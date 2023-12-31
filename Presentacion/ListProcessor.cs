﻿using Scheduler_Simulator.Logica;
using System.ComponentModel;

/*
 * 
 * HECHO POR JESUS GABRIEL PARRA
 * 
 */

namespace Scheduler_Simulator.Presentacion
{
    public partial class ListProcessor : UserControl
    {
        #region Attributes
        private List<RegProcess> processes;
        private string nucleusNumber;
        ListProcess[] listProcess;
        private Processor processor; 
        #endregion

        #region Constructors
        public ListProcessor(List<RegProcess> processes)
        {
            InitializeComponent();

            this.processes = processes;

            populateItems();
        }

        public ListProcessor(List<RegProcess> processes, Processor processor)
        {
            InitializeComponent();

            this.processes = processes;
            this.processor = processor;

            populateItems();
        } 
        #endregion

        private void populateItems()
        {
            listProcess = new ListProcess[processes.Count];

            for (int i = 0; i < listProcess.Length; i++)
            {
                listProcess[i] = new ListProcess();
                listProcess[i].ProcessName = processes[i].Nombre;
                listProcess[i].State = processes[i].Estado.ToString();
                listProcess[i].WaitTime = processes[i].TiempoEspera.ToString();
                listProcess[i].BurstTime = processes[i].TiempoRafaga.ToString();

                if (flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else flowLayoutPanel1.Controls.Add(listProcess[i]);

                processor.ListasProcesos = listProcess.ToList();

                
            }
        }

        #region CustomProps
        [Category("Custom Props")]
        public string NucleusNumber
        {
            get { return nucleusNumber; }
            set { nucleusNumber = value; lblNucleus.Text = value; }
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
