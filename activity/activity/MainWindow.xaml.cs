using System.Data;
using System.Windows;
using Microsoft.Win32;
using ClosedXML.Excel;
using System.Windows.Controls;


namespace activity
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            uploadedData = new DataTable();

            //DropDown list
            List<string> category = new List<string> { "Company name", "Security No." };
            CBDropDown.ItemsSource = category;


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
                    uploadedData = new DataTable();

                    // Add columns 
                    foreach (var headCell in worksheet.Row(1).Cells())
                    {
                        uploadedData.Columns.Add(headCell.Value.ToString());
                    }

                    // Add rows 
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        var dataRow = uploadedData.NewRow();
                        int columnIndex = 0;

                        foreach (var cell in row.Cells())
                        {
                            dataRow[columnIndex++] = cell.Value.ToString();
                        }

                        uploadedData.Rows.Add(dataRow);
                    }


                    DataTable limitedData = new DataTable();


                    if (uploadedData.Columns.Count > 0) limitedData.Columns.Add(uploadedData.Columns[0].ColumnName);
                    if (uploadedData.Columns.Count > 1) limitedData.Columns.Add(uploadedData.Columns[1].ColumnName);


                    foreach (DataRow row in uploadedData.Rows)
                    {
                        DataRow newRow = limitedData.NewRow();
                        if (uploadedData.Columns.Count > 0) newRow[0] = row[0];
                        if (uploadedData.Columns.Count > 1) newRow[1] = row[1];
                        limitedData.Rows.Add(newRow);
                    }


                    Suggestion.ItemsSource = limitedData.DefaultView;
                    MessageBox.Show("File uploaded successfully!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading the Excel file: {ex.Message}");
                }
            }
        }

        private DataTable uploadedData;

        private void BTSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text.ToLower(); 
            string category = CBDropDown.SelectedItem.ToString();
            DataTable dataTable = new DataTable();

            // Add columns 
            foreach (DataColumn column in uploadedData.Columns)
            {
                dataTable.Columns.Add(column.ColumnName);
            }

            // Check if data is uploaded and if there is a search 
            if (uploadedData.Rows.Count == 0)
            {
                MessageBox.Show("Please upload a file first.");
                return;
            }

            if(search == "")
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            // Check column index based on selected category
            int columnIndex = -1;
            if (category == "Company name") columnIndex = 0;
            else if (category == "Security No.") columnIndex = 1;

         
            if (columnIndex == -1)
            {
                MessageBox.Show("Invalid category selected.");
                return;
            }

            foreach (DataRow row in uploadedData.Rows)
            {
                if (row[columnIndex].ToString().ToLower().Contains(search))
                {
                    DataRow newRow = dataTable.NewRow();
                    for (int i = 0; i < uploadedData.Columns.Count; i++)
                    {
                        newRow[i] = row[i];
                    }
                    dataTable.Rows.Add(newRow);
                }
            }

            // Display results
            Data.ItemsSource = dataTable.DefaultView;

            // Show message if no data matches the search
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No matching records found.");
            }
        }

    }
}