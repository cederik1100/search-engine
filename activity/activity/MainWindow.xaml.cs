using System.Data;
using System.Windows;
using Microsoft.Win32;
using ClosedXML.Excel;

using System.IO;




namespace activity
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            uploadedData = new DataTable();

            //DropDown list
            List<string> category = new List<string> { "Company name", "Tax Payer's Name" };
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
                    var worksheet = workbook.Worksheet("Summary");
                    uploadedData = new DataTable();

                    for(int i = 1; i <= worksheet.LastColumnUsed().ColumnNumber(); i++)
                    {
                        uploadedData.Columns.Add(worksheet.Cell(1, i).Value.ToString());
                    }
                    for(int i = 2; i <= worksheet.LastRowUsed().RowNumber(); i++)
                    {
                        var dataRow = uploadedData.NewRow();
                        for(int j = 1; j <= worksheet.LastColumnUsed().ColumnNumber(); j++)
                        {
                            if (worksheet.Cell(i, j).Value.ToString() == "")
                            {
                                dataRow[j - 1] = "None";
                            }
                            else
                            {
                                dataRow[j - 1] = worksheet.Cell(i, j).Value.ToString();
                            }
                        }
                        uploadedData.Rows.Add(dataRow);
                    }                   


                    DataTable limitedData = new DataTable();


                    if (uploadedData.Columns.Count > 0) limitedData.Columns.Add(uploadedData.Columns[0].ColumnName);
                    if (uploadedData.Columns.Count > 4) limitedData.Columns.Add(uploadedData.Columns[4].ColumnName);


                    foreach (DataRow row in uploadedData.Rows)
                    {
                        DataRow newRow = limitedData.NewRow();
                        if (uploadedData.Columns.Count > 0) newRow[0] = row[0];
                        if (uploadedData.Columns.Count > 4) newRow[1] = row[4];
                        limitedData.Rows.Add(newRow);
                    }


                    //ListBox.ItemsSource = limitedData.DefaultView;
                    
                    List<string> displayList = new List<string>();

                    foreach (DataRow row in limitedData.Rows)
                    {
                        displayList.Add($"Company Name: {row[0]}" +
                                        $"\nTax Payer's Name: {row[1]}");
                    }

                    ListBox.ItemsSource = displayList;
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
            else if (category == "Tax Payer's Name") columnIndex = 4;

         
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
            //ListBox2.ItemsSource = dataTable.DefaultView;

            List<string> displayList = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                displayList.Add($"Company Name: {row[0]} " +
                                $"\nSec no: {row[1]}  " +
                                $"\nLicense no: {row[2]} " +
                                $"\nDate Registered: {row[3]} " +
                                $"\nTax Payer's Name: {row[4]} " +
                                $"\nViolation: {row[5]}"); 
            }

            ListBox2.ItemsSource = displayList;
 

            // Show message if no data matches the search
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No matching records found.");
            }
        }

        private void BTPrint_Click(object sender, RoutedEventArgs e)
        {
            //if (ListBox2.ItemsSource == null)
            //{
            //    MessageBox.Show("No data to print. Please upload a file and search first.");
            //    return;
            //}

            //SaveFileDialog saveFileDialog = new SaveFileDialog
            //{
            //    Title = "Save PDF File",
            //    Filter = "PDF Files|*.pdf",
            //    FileName = "Data.pdf"
            //};

            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    try
            //    {
            //        DataTable dataTable = ((DataView)ListBox2.ItemsSource).ToTable();

            //        Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            //        PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
            //        document.Open();

            //        PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            //        table.WidthPercentage = 100;

            //        // Add headers
            //        foreach (DataColumn column in dataTable.Columns)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName))
            //            {
            //                BackgroundColor = BaseColor.LIGHT_GRAY,
            //                HorizontalAlignment = Element.ALIGN_CENTER
            //            };
            //            table.AddCell(cell);
            //        }

            //        // Add data rows
            //        foreach (DataRow row in dataTable.Rows)
            //        {
            //            foreach (var cellData in row.ItemArray)
            //            {
            //                table.AddCell(cellData.ToString());
            //            }
            //        }

            //        document.Add(table);
            //        document.Close();

            //        MessageBox.Show("PDF saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            }

        private void BTClear_Click(object sender, RoutedEventArgs e)
        {
            ListBox2.ItemsSource = null;
            MessageBox.Show("Data cleared successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BTClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListBox2.SelectedItem != null)
            {
                Window1 window1 = new Window1(ListBox2.SelectedItem);
                window1.Show();
            }
        }
    }

}