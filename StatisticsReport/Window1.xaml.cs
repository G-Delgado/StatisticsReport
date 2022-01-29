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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace StatisticsReport
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(string[] rows)
        {
            InitializeComponent();

            IDictionary<String, int> dict = new Dictionary<String, int>();
            for (int i = 1; i < rows.Length; i++)
            {
                String type = rows[i].Split(',')[4];
                if (dict.ContainsKey(type))
                {
                    dict[type] += 1;
                } else
                {
                    dict[type] = 1;
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Municipios por Tipo",
                    Values = new ChartValues<double> { dict["Municipio"], dict["Isla"], dict["Área no municipalizada"] }
                }
            };

            Console.WriteLine(dict["Municipio"]);
            Console.WriteLine(dict["Isla"]);
            Console.WriteLine(dict["Área no municipalizada"]);

            //adding series will update and animate the chart automatically
            /*SeriesCollection.Add(new RowSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });*/

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Municipio", "Isla", "Área no municipalizada" };
            Formatter = value => value.ToString("N");

            DataContext = this;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
