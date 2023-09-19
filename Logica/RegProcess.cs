using System;
using System.Collections.Generic;

namespace Scheduler_Simulator.Logica
{
    public class RegProcess
    {
        #region Atributos

        // Estados posibles del proceso
        public enum EstadoProceso
        {
            Esperando,  // En espera
            Ejecutando, // En ejecución
            Bloqueado,  // Bloqueado
            Terminado   // Terminado
        }

        private string nombre = "";
        private int prioridad = 1;
        private float tiempoEspera = 0; // Tiempo de espera
        private float tiempoRafaga = 0; // Tiempo de ráfaga
        private EstadoProceso estado = EstadoProceso.Esperando;
        #endregion

        #region Constructores
        public RegProcess() { }

        public RegProcess(string nombre, int prioridad)
        {
            this.nombre = nombre;
            this.prioridad = prioridad;
        }
        #endregion

        #region Propiedades
        public string Nombre { get => nombre; set => nombre = value; }
        public int Prioridad { get => prioridad; set => prioridad = value; }
        public float TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }
        public float TiempoRafaga { get => tiempoRafaga; set => tiempoRafaga = value; }
        public EstadoProceso Estado { get => estado; set => estado = value; }
        #endregion
    }
}
