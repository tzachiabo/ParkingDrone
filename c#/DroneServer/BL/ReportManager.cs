using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace DroneServer.BL
{
    public class ReportManager
    {
        private static ReportManager instance = null;
        private HashSet<KeyValuePair<string, string>> UnauthorizedCars;

        private ReportManager()
        {
            UnauthorizedCars = new HashSet<KeyValuePair<string, string>>();
        }

        public static ReportManager getInstance()
        {
            if (instance == null)
            {
                instance = new ReportManager();
            }

            return instance;
        }

        public void addCarPlate(string car_plate, string path_to_car_image)
        {
            if (isUnauthorizedCarPlate(car_plate))
            {
                UnauthorizedCars.Add(new KeyValuePair<string, string>(car_plate, path_to_car_image));
            }
        }

        public void make_report(string path)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Drone Report";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont title_font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            XFont normal_font = new XFont("Verdana", 10);

            gfx.DrawString("Drone Report", title_font, XBrushes.Black, new XRect(0, 0, page.Width, 70), XStringFormats.Center);

            int line = 180;
            int image_line = 110;

            if (UnauthorizedCars.Count == 0)
            {
                gfx.DrawString("No cars where parking illigaly", normal_font, XBrushes.Black, new XRect(40, 0, page.Width, line), XStringFormats.CenterLeft);
            }
            else
            {
                foreach (KeyValuePair<string, string> car in UnauthorizedCars)
                {
                    if (line > 1400)
                    {
                        page = document.AddPage();

                        gfx = XGraphics.FromPdfPage(page);

                        line = 70;
                        image_line = 50;
                    }
                    gfx.DrawString("car plate: " + car.Key, normal_font, XBrushes.Black, new XRect(40, 0, page.Width, line), XStringFormats.CenterLeft);
                    XImage image = XImage.FromFile(car.Value);
                    double x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;
                    gfx.DrawImage(image, 80, image_line, 250, 140);
                    line += 350;
                    image_line += 175;
                }
            }

            document.Save(path);
        }

        private Boolean isUnauthorizedCarPlate(string car_plate)
        {
            return true;
        }

        //private void drawImageToPDF(XGraphics gfx, int number, String jpegSamplePath)
        //{
        //    BeginBox(gfx, number, "DrawImage (rotated)");
        //    XImage image = XImage.FromFile(jpegSamplePath);
        //    const double dx = 250, dy = 140;

        //    gfx.TranslateTransform(dx / 2, dy / 2);
        //    gfx.ScaleTransform(0.7);
        //    gfx.RotateTransform(-25);
        //    gfx.TranslateTransform(-dx / 2, -dy / 2);

        //    double width = image.PixelWidth * 72 / image.HorizontalResolution;
        //    double height = image.PixelHeight * 72 / image.HorizontalResolution;

        //    gfx.DrawImage(image, (dx - width) / 2, 0, width, height);

        //    EndBox(gfx);
        //}

        //public void EndBox(XGraphics gfx)
        //{
        //    gfx.Restore(this.state);
        //}

        //public void BeginBox(XGraphics gfx, int number, string title)
        //{
        //    const int dEllipse = 15;
        //    XRect rect = new XRect(0, 20, 300, 200);
        //    if (number % 2 == 0)
        //        rect.X = 300 - 5;
        //    rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
        //    rect.Inflate(-10, -10);
        //    XRect rect2 = rect;
        //    rect2.Offset(this.borderWidth, this.borderWidth);
        //    gfx.DrawRoundedRectangle(new XSolidBrush(this.shadowColor), rect2, new XSize(dEllipse + 8, dEllipse + 8));
        //    XLinearGradientBrush brush = new XLinearGradientBrush(rect, this.backColor, this.backColor2, XLinearGradientMode.Vertical);
        //    gfx.DrawRoundedRectangle(this.borderPen, brush, rect, new XSize(dEllipse, dEllipse));
        //    rect.Inflate(-5, -5);
        //    XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
        //    gfx.DrawString(title, font, XBrushes.Navy, rect, XStringFormats.TopCenter);
        //    rect.Inflate(-10, -5);
        //    rect.Y += 20;
        //    rect.Height -= 20;

        //    this.state = gfx.Save();
        //    gfx.TranslateTransform(rect.X, rect.Y);

        //}
    }
}
