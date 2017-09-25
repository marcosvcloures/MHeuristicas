﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for GeneticAlgorithm.xaml
    /// </summary>
    public partial class SimulatedAnnealing : UserControl, INotifyPropertyChanged
    {
        public Heuristics.HeuristicsBase Heuristics;
        public MainWindow mainWindow;

        public double t { get; set; }
        public double tf { get; set; }
        public double taxaResf { get; set; }
        public int contIteracao { get; set; }
        public bool opt1 { get { return opt == 1; } set { opt = 1; } }
        public bool opt2 { get { return opt == 2; } set { opt = 2; } }

        private int opt;

        public event PropertyChangedEventHandler PropertyChanged;

        public SimulatedAnnealing()
        {
            DataContext = this;

            t = 100000;
            tf = 1000;
            taxaResf = 0.95;
            contIteracao = 100;
            opt = 2;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bkw = new BackgroundWorker();

            mainWindow.StartHeuristic();

            mainWindow.Heuristic = new Heuristics.SimulatedAnnealing(t, tf, taxaResf, contIteracao, opt);

            bkw.DoWork += (_, __) =>
            {
                mainWindow.Heuristic.Run();
            };

            bkw.RunWorkerCompleted += (_, __) =>
            {
                mainWindow.EndHeuristic();
            };

            bkw.RunWorkerAsync();
        }
    }
}