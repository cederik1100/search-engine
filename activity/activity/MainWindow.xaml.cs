using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to upload";
            openFileDialog.Filter = "Excel Files|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("File selected: " + openFileDialog.FileName);
            }

        }
    }
}