using System.Diagnostics;
using System.Collections.Generic;

namespace Scheduler_Simulator.Logica
{
    public class Scheduler
    {
        #region Atributos
        List<RegProcess> procesos = new List<RegProcess>(); // Lista de procesos que se van a manejar
        List<Processor> procesadores = new List<Processor>(); // Lista de procesadores con los que se van a trabajar

        List<int> procesosIndice = new List<int>(); // Lista de índices que relacionan los procesadores con los procesos 
        #endregion

        #region Constructores

        // Constructor para asignar procesos automáticamente
        public Scheduler(List<Processor> procesadores, List<RegProcess> procesos)
        {
            this.procesadores = procesadores;
            this.procesos = procesos;

            AsignarProcesosAutomaticamente();
        }

        // Constructor para asignar procesos manualmente
        public Scheduler(List<Processor> procesadores, List<RegProcess> procesos, List<int> procesosIndice)
        {
            this.procesadores = procesadores;
            this.procesos = procesos;
            this.procesosIndice = procesosIndice;

            AsignarProcesosManualmente();
        } 
        #endregion

        // Método para ordenar los procesos por prioridad
        public List<RegProcess> OrdenarPorPrioridad()
        {
            List<RegProcess> procesosOrdenados = procesos.OrderByDescending(p => p.Prioridad).ToList();
            return procesosOrdenados;
        }

        // Distribuir para cada procesador un número igual de procesos
        public void AsignarProcesosAutomaticamente()
        {
            Debug.WriteLine("Total de procesadores: " + procesadores.Count);
            Debug.WriteLine("Total de procesos: " + procesos.Count);

            int totalProcesadores = procesadores.Count;                         // Total de procesadores
            int procesosPorProcesador = procesos.Count / totalProcesadores;     // Procesos por cada procesador
            int procesosRestantes = procesos.Count % totalProcesadores;         // Residuo, si lo hay

            int indiceInicio = 0;

            foreach (var procesador in procesadores) // Para cada procesador dentro de la lista
            {
                int numProcesos = procesosPorProcesador;

                if (procesosRestantes > 0)
                {
                    numProcesos++;
                    procesosRestantes--;   // Se deshace del residuo cuando el número de procesos es impar
                }

                List<RegProcess> procesosParaProcesador = procesos.GetRange(indiceInicio, numProcesos);
                procesador.Procesos = procesosParaProcesador;
                indiceInicio += numProcesos;

                Debug.WriteLine("Procesos del procesador: " + procesador.Procesos.Count);
            }
        }

        // El usuario puede escoger a qué procesador va cada proceso
        public void AsignarProcesosManualmente()
        {
            for (int i = 0; i < procesos.Count; i++)
            {
                int indice = procesosIndice[i];

                if (indice >= 0 && indice < procesadores.Count)
                {
                    if (procesadores[indice].Procesos == null)
                    {
                        procesadores[indice].Procesos = new List<RegProcess>();
                    }

                    procesadores[indice].Procesos.Add(procesos[i]);
                }
                else
                {
                    Console.WriteLine("Error: El índice de procesador está fuera de rango para el proceso en la posición " + i);
                }
            }
        }

        // Imprime todos los procesos dentro de un procesador
        public void MostrarProcesos(int procesador) 
        {
            if (procesador < procesadores.Count)
            {
                Debug.WriteLine("Procesos dentro del procesador " + procesador + ": ");

                for (int i = 0; i < procesadores[procesador].Procesos.Count; i++)
                {
                    Debug.WriteLine(procesadores[procesador].Procesos[i].Nombre + ", " + procesadores[procesador].Procesos[i].Prioridad);
                }
            }
            else Debug.WriteLine("No existe tal procesador");
        }

        #region Propiedades
        public List<RegProcess> Procesos { get => procesos; set => procesos = value; }
        public List<Processor> Procesadores { get => procesadores; set => procesadores = value; } 
        #endregion
    }
}
