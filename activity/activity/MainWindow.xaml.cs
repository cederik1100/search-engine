using System.Data;
using System.Windows;
using Microsoft.Win32;
using ClosedXML.Excel;


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

                    foreach (var headCell in worksheet.Row(1).Cells())
                    {
                        uploadedData.Columns.Add(headCell.Value.ToString());
                    }

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

                    MyDataGrid.ItemsSource = uploadedData.DefaultView;
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

            string search = TBSearch.Text;
            string category = CBDropDown.SelectedIndex.ToString();
            bool dataFound = false;


            if (uploadedData.Rows.Count == 0)
            {
                MessageBox.Show("Please upload a file first.");

            }
            else
            {
                
                if (category == "0")
                {
                    if (search == "")
                    {
                        MessageBox.Show("Please enter a search term.");
                    }
                    else
                    {
                        foreach (DataRow row in uploadedData.Rows)
                        {
                            if (row[0].ToString().ToLower() == search.ToLower() || row[0].ToString().ToUpper() == search.ToUpper())
                            {
                                TxtCompanyName.Text = row[0].ToString();
                                TxtSecurityNo.Text = row[1].ToString();
                                TxtDateRegistered.Text = row[3].ToString();
                                TxtLicenseNo.Text = row[2].ToString();
                                TxtViolation.Text = row[5].ToString();
                                TxtPayersName.Text = row[4].ToString();
                                dataFound = true;
                                break;
                            }
                        }

                        if (dataFound == false)
                        {
                            MessageBox.Show("Data not found");
                        }
                        
                    }
                }
                else if (category == "1")
                {
                    if (search == "")
                    {
                        MessageBox.Show("Please enter a search term.");
                    }
                    else
                    {
                        foreach (DataRow row in uploadedData.Rows)
                        {
                            if (row[1].ToString() == search)
                            {
                                TxtCompanyName.Text = row[0].ToString();
                                TxtSecurityNo.Text = row[1].ToString();
                                TxtDateRegistered.Text = row[3].ToString();
                                TxtLicenseNo.Text = row[2].ToString();
                                TxtViolation.Text = row[5].ToString();
                            }
                        }
                    }
                }
            }


        }
        
    }
}