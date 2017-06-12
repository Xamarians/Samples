using System;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Xamarin.Forms;
namespace PdfDemo
{
    public partial class MainPage : ContentPage
	{
        ViewModel.PdfViewModel viewModel;
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModel.PdfViewModel();           
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {        
            var path = Path.Combine(Helpers.FileHelpers.DirectoryPath, "myTestFile"+count+".pdf");
            count++;
            PdfPTable tableLayout = new PdfPTable(4);
            var fs = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
             document.Add(Add_Content_To_PDF(tableLayout));
         
            document.Close();
            writer.Close();
            fs.Close();
            await DisplayAlert("PDF generated", "", "ok");
        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {
            float[] headers = { 20, 20, 30, 30 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 90;       //Set the PDF File width percentage

            //Add Title to the PDF file at the top
			tableLayout.AddCell(new PdfPCell(new Phrase("Data Sheet", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 20, 1, new iTextSharp.text.Color(153, 51, 0))))
            {
                Colspan = 4,
                Border = 0,
                PaddingBottom = 20,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
				BackgroundColor = new iTextSharp.text.Color(0, 153, 140)
            });
			tableLayout.AddCell(new PdfPCell(new Phrase("Sheet No. :", new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 13, 1, iTextSharp.text.Color.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, PaddingBottom = 15, BackgroundColor = iTextSharp.text.Color.WHITE });
			tableLayout.AddCell(new PdfPCell(new Phrase(viewModel.CompanyInfo, new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 15, 1, iTextSharp.text.Color.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, PaddingBottom = 15, BackgroundColor = iTextSharp.text.Color.WHITE });
			tableLayout.AddCell(new PdfPCell(new Phrase("Date :", new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 15, 1, iTextSharp.text.Color.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, PaddingBottom = 15, BackgroundColor = iTextSharp.text.Color.WHITE });
			tableLayout.AddCell(new PdfPCell(new Phrase(viewModel.ClientInfo, new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 15, 1, iTextSharp.text.Color.BLACK))) { Colspan = 1, Border = 0, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, PaddingBottom = 15, BackgroundColor = iTextSharp.text.Color.WHITE });

            //Add header
            AddCellToHeader(tableLayout, "Cricketer Name");
            AddCellToHeader(tableLayout, "Height");
            AddCellToHeader(tableLayout, "Born On");
            AddCellToHeader(tableLayout, "Parents");

            //Add body
            AddCellToBody(tableLayout, "Sachin Tendulkar");
            AddCellToBody(tableLayout, "1.65 m");
            AddCellToBody(tableLayout, "April 24, 1973");
            AddCellToBody(tableLayout, "Ramesh Tendulkar, Rajni Tendulkar");

            AddCellToBody(tableLayout, "Mahendra Singh Dhoni");
            AddCellToBody(tableLayout, "1.75 m");
			AddCellToBody(tableLayout, "July 7, 1981");
			AddCellToBody(tableLayout, "Devki Devi, Pan Singh");

			AddCellToBody(tableLayout, "Virender Sehwag");
			AddCellToBody(tableLayout, "1.70 m");
			AddCellToBody(tableLayout, "October 20, 1978");
			AddCellToBody(tableLayout, "Aryavir Sehwag, Vedant Sehwag");

			AddCellToBody(tableLayout, "Virat Kohli");
			AddCellToBody(tableLayout, "1.75 m");
			AddCellToBody(tableLayout, "November 5, 1988");
			AddCellToBody(tableLayout, "Saroj Kohli, Prem Kohli");
			tableLayout.AddCell(new PdfPCell(new Phrase("Total ", new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 15, 1, iTextSharp.text.Color.BLACK))) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, PaddingBottom = 10, BackgroundColor = iTextSharp.text.Color.WHITE });
			tableLayout.AddCell(new PdfPCell(new Phrase("4", new iTextSharp.text.Font(iTextSharp.text.Font.COURIER, 15, 1, iTextSharp.text.Color.BLACK))) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, PaddingBottom = 10, BackgroundColor = iTextSharp.text.Color.WHITE });
			return tableLayout;
		}
        // Method to add single cell to the header
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, 1, iTextSharp.text.Color.WHITE))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, Padding = 5, BackgroundColor = iTextSharp.text.Color.RED });
        }

        // Method to add single cell to the body
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, 1, iTextSharp.text.Color.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, Padding = 5, BackgroundColor = iTextSharp.text.Color.WHITE });
        }


    }
}
    
