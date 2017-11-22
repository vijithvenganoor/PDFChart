using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

namespace PDFChart
{
    class Program
    {
        static void Main(string[] args)
        {
            ChartCreat();
        }

        static void ChartCreat()
        {
            string[] x = new string[4] { "Mango", "Apple", "Orange", "Banana" };
            int[] y = new int[4] { 200, 112, 55, 96 };
            Chart Chart1 = new Chart();
            Chart1.Titles.Add(new Title("Items"));
            Chart1.Legends.Add(new Legend("Default"));
            Chart1.Series.Add(new Series("Default"));
            Chart1.ChartAreas.Add(new ChartArea("ChartArea1"));
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;

            FileStream fs = new FileStream(@"D:\Servers\Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
            pdfDoc.Open();
            using (MemoryStream stream = new MemoryStream())
            {
                Chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                pdfDoc.Add(chartImage);
                pdfDoc.Close();
            }
        }

    }
}
