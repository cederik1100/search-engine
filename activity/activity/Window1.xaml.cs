using System.Windows;
using System.IO;
using Microsoft.Win32;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;


namespace activity
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(object selectedItem)
        {
            InitializeComponent();
            TxtBlock.Text = selectedItem.ToString();
        }

        private void BTPrint_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save PDF",
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = "SelectedItem.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Delete existing file if necessary
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Create PDF with design
                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    // Set Fonts
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // Title
                    Paragraph title = new Paragraph("Searched")
                        .SetFont(boldFont)
                        .SetFontSize(20)
                        .SetFontColor(new DeviceRgb(3, 52, 110))
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    document.Add(title);

                    // Space
                    document.Add(new Paragraph("\n"));

                    // Background Rectangle
                    PdfPage currentPage = pdf.GetPage(pdf.GetNumberOfPages());
                    PdfCanvas canvas = new PdfCanvas(currentPage);
                    canvas.SetFillColor(new DeviceRgb(230, 230, 250))
                          .Rectangle(100, 550, 400, 200)
                          .Fill();

                   
                    Paragraph valueText = new Paragraph(TxtBlock.Text)
                        .SetFont(normalFont)
                        .SetFontSize(12)
                        .SetFontColor(ColorConstants.DARK_GRAY)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                    document.Add(valueText);

                    // Footer
                    document.Add(new Paragraph("Generated on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        .SetFont(normalFont)
                        .SetFontSize(10)
                        .SetFontColor(ColorConstants.GRAY)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                }

                MessageBox.Show("PDF saved successfully at:\n" + filePath, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BTClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
