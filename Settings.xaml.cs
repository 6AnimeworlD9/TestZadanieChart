using System;
using System.Collections.Generic;
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

namespace TestZadanieChart
{
    //класс для настроек графика
    public partial class Settings : Window
    {
        //Инициализация окна.
        public Settings()
        {
            InitializeComponent();
        }
        //Событие при вводе в TextBox, для того чтобы можно было вводить только цифры.
        private void onlyNumber(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        //Запуск окна с графиком и передача настроек от пользователя в переменные окна с графиком.
        //ВАЖНО: для примера X нужен от 50 до 800, Y от 50 до 1000.
        private void run_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow cw = new ChartWindow();
            cw.Owner = this;
            try
            {
                if (Convert.ToInt32(countX.Text) > 0 && Convert.ToInt32(countY.Text) > 0)
                {
                    cw.countX = Convert.ToInt32(countX.Text);
                    cw.countY = Convert.ToInt32(countY.Text);
                    cw.countT = Convert.ToInt32(countT.Text);
                    cw.Show();
                    cw.firstFormChart();
                }
                else MessageBox.Show("Числа должны быть больше нуля.");
            }
            catch { MessageBox.Show("Произошла ошибка."); return; }
        }
    }
}
