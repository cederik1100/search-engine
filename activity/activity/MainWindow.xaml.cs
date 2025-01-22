using System;
using System.Data;
using System.Windows;
using Microsoft.Win32;
using ClosedXML.Excel;

namespace activity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //DropDown list
            List<string> category = new List<string> { "Company name", "Security No." };
            CBDropDown.ItemsSource = category;


        }


        private void BTSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text;

           List<string> companies = new List<string>{"Microsoft", "Google", "Amazon"};

            // Check if the searched text exists in the company list
            if (companies.Contains(search))
            {
                TxtCompanyName.Text = search;
            }
            else
            {
                TxtCompanyName.Text = "Company not found";
            }

        }

        private void BTUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to upload";
            openFileDialog.Filter = "Excel Files|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var workbook = new XLWorkbook(openFileDialog.FileName);
                    var worksheet = workbook.Worksheet(1);
                    var dataTable = new DataTable();

                    foreach (var headCell in worksheet.Row(1).Cells())
                    {
                        dataTable.Columns.Add(headCell.Value.ToString());
                    }

                    foreach( var row in worksheet.RowsUsed().Skip(1))
                    {
                        var dataRow = dataTable.NewRow();
                        int columnIndex = 0;

                        foreach (var cell in row.Cells())
                        {
                            dataRow[columnIndex++] = cell.Value.ToString();
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                    MyDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading the Excel file: {ex.Message}");
                }
            }
        }

        
    }
}