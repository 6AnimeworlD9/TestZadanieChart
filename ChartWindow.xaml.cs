using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestZadanieChart
{
    //класс-логика для постройки трендов на графике
    public partial class ChartWindow : Window
    {
        public int countX { get; set; }
        public int countY { get; set; }
        public int countT { get; set; }
        public double[] chart1;
        public double[] chart2;
        public double[] xSimulation1;
        public double[] xSimulation2;
        public int iter = 1;
        public ScottPlot.Plottable.SignalPlot sig;
        public System.Windows.Threading.DispatcherTimer dT;

        //Инициализация окна и создание таймера, у которого событие срабатывает каждую 1 секунду.
        public ChartWindow()
        {
            InitializeComponent();
            dT = new System.Windows.Threading.DispatcherTimer();
            dT.Tick += new EventHandler(timer1_Tick);
            dT.Interval = new TimeSpan(0, 0, 1);
        }
        //Событие таймера, который двигает тренды за счет итератора(для массивов).
        private void timer1_Tick(object sender, EventArgs e)
        {
            simulationDynamicChart();
            Chart.Render();
            iter++;
        }
        //Создание массивов пути и массивов для движения тренда по этому пути, запуск таймера.
        public void firstFormChart()
        {
            switch (countT)
            {
                case 1:
                    chart1 = settingsPlane(false, countX, countY);
                    xSimulation1 = new double[countX + 1];
                    break;
                case 2:
                    Random r = new Random();
                    chart1 = settingsPlane(false, countX, countY);
                    /*DataGen.RandomWalk возвращает не совсем рандомный путь для тренда, 
                     поэтому для второго тренда меняем его благодаря классу Random*/
                    chart2 = new double[1_000_000];
                    for (int i = 0; i < 1_000_000; i++)
                        chart2[i] = chart1[i] + r.Next(-2, 2);
                    xSimulation1 = new double[countX + 1];
                    xSimulation2 = new double[countX + 1];
                    break;
            }
            dT.Start();
        }
        //Первоначальная настройка графика в целом и возврат пути для тренда из 1кк точек.
        public double[] settingsPlane(bool b, int cX, int cY)
        {
            Chart.Configuration.WarnIfRenderNotCalledManually = b;
            Chart.Plot.SetAxisLimitsX(0, cX);
            Chart.Plot.SetAxisLimitsY(0, cY);
            Chart.Plot.SetOuterViewLimits(0, cX, 0, cY);
            Random r = new Random(0);
            return ScottPlot.DataGen.RandomWalk(r, 1_000_000, 2);
        }
        /*Симуляция движения графика за счет дополнительных массивов размером длины оси X, в которые загоняется
        часть значений из массива пути. Основная логика благодаря итератору iter.*/
        private void simulationDynamicChart()
        {
            for (int i = 0; i < countX + 1; i++)
            {
                xSimulation1[i] = chart1[i + iter];
                if (countT == 2)
                    xSimulation2[i] = chart2[i + iter];
            }

            sig = Chart.Plot.AddSignal(xSimulation1, 1, System.Drawing.Color.Blue);
            if (countT == 2)
                sig = Chart.Plot.AddSignal(xSimulation2, 1, System.Drawing.Color.Red);
            sig.OffsetX = 0;
            sig.OffsetY = 0;
        }
    }
}


