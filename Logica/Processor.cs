using Scheduler_Simulator.Presentacion;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Scheduler_Simulator.Logica
{
    public class Processor
    {
        #region Atributos
        // Registro de procesos que maneja el procesador 
        private List<RegProcess> procesos = new List<RegProcess>();

        // Variables que controlan los paneles de la experiencia de usuario
        private List<ListProcess> listasProcesos = new List<ListProcess>();
        private System.Windows.Forms.Timer temporizadorActualizacionProgressBar;
        private int indiceActualListaProceso = 0;

        // Variables para medir el tiempo en ejecución
        private float tiempoInicial = 0;
        private float tiempoActual = 0;
        private Stopwatch cronometro = new Stopwatch();
        #endregion

        public Processor()
        {
            // Instancia el temporizador
            temporizadorActualizacionProgressBar = new System.Windows.Forms.Timer();
            temporizadorActualizacionProgressBar.Interval = 500; // Tiempo en milisegundos
            temporizadorActualizacionProgressBar.Tick += TemporizadorActualizacionProgressBar_Tick;
        }

        public void ProcesarProcesos()
        {
            // Cuando llega al procesador, el proceso se cambia de estado a "Ejecutando"
            foreach (var proceso in procesos)
            {
                proceso.Estado = RegProcess.EstadoProceso.Ejecutando;
            }

            // Comienza la cuenta del tiempo y el temporizador
            temporizadorActualizacionProgressBar.Start();
            cronometro.Start();
        }

        private void TemporizadorActualizacionProgressBar_Tick(object sender, EventArgs e)
        {
            ListProcess listaProcesoActual = listasProcesos[indiceActualListaProceso];

            // Si la barra de progreso actual no ha alcanzado el 100%
            if (listaProcesoActual.ProgressBarValue < 100)
            {
                // Incrementa la barra de progreso en 10
                listaProcesoActual.ProgressBarValue += 10;

                tiempoActual += (float)cronometro.Elapsed.TotalSeconds;
            }
            else
            {
                // Actualizar los datos de los procesos y de los paneles de procesos
                procesos[indiceActualListaProceso].Estado = RegProcess.EstadoProceso.Terminado;
                listasProcesos[indiceActualListaProceso].State = RegProcess.EstadoProceso.Terminado.ToString();

                procesos[indiceActualListaProceso].TiempoEspera = tiempoInicial % 2;
                listasProcesos[indiceActualListaProceso].WaitTime = tiempoInicial.ToString();

                procesos[indiceActualListaProceso].TiempoRafaga = tiempoActual % 2;
                listasProcesos[indiceActualListaProceso].BurstTime = tiempoActual.ToString();

                // Cuando termina todo el ciclo de una barra de carga, se iguala el tiempo inicial con tiempo total
                // Ahora el tiempo inicial es el tiempo total anterior
                tiempoInicial = tiempoActual;

                // Si la barra de progreso ha alcanzado el 100%, pasa a la siguiente
                indiceActualListaProceso++;

                // Cuando se llega al final de la lista, se detiene el temporizador y el cronómetro
                if (indiceActualListaProceso >= listasProcesos.Count)
                {
                    temporizadorActualizacionProgressBar.Stop();
                    cronometro.Stop();
                }
            }
        }

        #region Propiedades
        public List<RegProcess> Procesos { get => procesos; set => procesos = value; }
        public List<ListProcess> ListasProcesos { get => listasProcesos; set => listasProcesos = value; }
        #endregion
    }
}
