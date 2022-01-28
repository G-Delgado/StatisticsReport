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

namespace StatisticsReport
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String[] rows = null;
        DataTable dt = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
