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
using Microsoft.Win32;
using System.IO;
using System.Data;
using LiveCharts;
using LiveCharts.Wpf;

namespace StatisticsReport
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private Window1 wn = null;

        private String[] rows = null;

        private DataTable dt = new DataTable();

        private String[] departments = null;
        private SeriesCollection SeriesCollection { get; set; }
        private string[] Labels { get; set; }
        private Func<double, string> Formatter { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            departments = new string []{"Amazonas",
                "Antioquia",
                "Arauca",
                "Atlántico",                
                "Bolívar",
                "Boyacá",
                "Caldas",
                "Caqueta",
                "Casanare",
                "Cauca",
                "Cesar",
                "Chocó",
                "Córdoba",
                "Cundinamarca",
                "Guainiía",
                "Guaviare",
                "Huila",
                "La Guajira",
                "Magdalena",
                "Meta",
                "Nariño",
                "Norte de Santander",
                "Putumayo",
                "Quindío",
                "Risaralda",
                "San Andrés y Providencia",
                "Santander",
                "Sucre",
                "Tolima",
                "Valle del Cauca",
                "Vaupés",
                "Vichada"};

            cbDepartamentos.ItemsSource = departments;

            //mainDataGrid.ItemsSource = dt.AsDataView();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();            
            opf.Title = "Import Data";

            if (opf.ShowDialog() == true){
                labelToChange.Content = opf.FileName;
                mainDataGrid.ItemsSource =loadData(opf.FileName).AsDataView();
                labelToChange.Content = " Se han importado los datos";
            }
        }

        private DataTable loadData(string filePath)
        {
            //rows = File.ReadAllText(filePath).Split('\n');
            rows = File.ReadAllLines(filePath);//Lee todas las lineas del archivo
            string[] columns = rows.First().Split(',');
            columns[4] = "Tipo";
            foreach(string column in columns)
            {
                dt.Columns.Add(column);
            }

            for(var i = 1; i < rows.Length; i++)
            {
                // The last column is't working
                dt.Rows.Add(rows[i].Split(','));
            }

            //Console.WriteLine(rows[2].Split(',')[4]);

            return dt;
        }

        private void Button_Filtar(object sender, RoutedEventArgs e)
        {
            if(rows == null)
            {
                labelToChange.Content = "No se ha importado el archivo!";
            } else
            {
                dt.Rows.Clear();
                for (var i = 1; i < rows.Length; i++)
                {                
                    // The last column is't working
                    String[] parts = rows[i].Split(',');                
                    if (parts[2].Equals(cbDepartamentos.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        dt.Rows.Add(parts);
                    }                
                }
                mainDataGrid.ItemsSource = dt.AsDataView();
            }

        }
    
        private  void Show_New_Window(object sender, RoutedEventArgs e)
        {
            if (rows == null)
            {
                labelToChange.Content = "No se ha importado el archivo para el gráfico!";
            } else
            {
            wn = new Window1(rows);
            wn.Show();
            }
        }
    }
}
