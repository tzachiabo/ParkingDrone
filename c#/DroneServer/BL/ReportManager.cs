using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;
using Emgu.CV;
using Emgu.CV.Structure;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace DroneServer.BL
{
    public class ReportManager
    {
        private static ReportManager instance = null;
        private HashSet<KeyValuePair<string, string>> UnauthorizedCars;
        private List<Car> unauthorized_cars;
        private List<String> allowed_car_plates;
        private bool is_init;
        private string m_base_photo_path;

        private ReportManager()
        {
            UnauthorizedCars = new HashSet<KeyValuePair<string, string>>();
            unauthorized_cars = new List<Car>();
            is_init = false;
        }

        public static ReportManager getInstance()
        {
            if (instance == null)
            {
                instance = new ReportManager();
            }

            return instance;
        }

        public void init(String base_photo_path)
        {
            Assertions.verify(!is_init, "Report manager is allready initialized");

            is_init = true;
            m_base_photo_path = base_photo_path;
        }

        public void addCarPlate(string car_plate, string path_to_car_image, Car car)
        {
            Assertions.verify(is_init, "Report manager is not initialized");

            Logger.getInstance().info("adding car_plate: " + car_plate + " to report");
            if (isUnauthorizedCarPlate(car_plate))
            {
                unauthorized_cars.Add(car);
                UnauthorizedCars.Add(new KeyValuePair<string, string>(car_plate, path_to_car_image));
            }
        }

        public void make_report(string path)
        {
            Assertions.verify(is_init, "Report manager is not initialized");

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Drone Report";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont title_font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            XFont sub_title_font = new XFont("Verdana", 15, XFontStyle.BoldItalic);

            XFont normal_font = new XFont("Verdana", 10);

            gfx.DrawString("Drone Report", title_font, XBrushes.Black, new XRect(0, 0, page.Width, 70), XStringFormats.Center);

            int line = 180;
            int image_line = 110;

            gfx.DrawString("Parking Image:", sub_title_font, XBrushes.Black, new XRect(40, 0, page.Width, line), XStringFormats.CenterLeft);

            String parking_img_path = "parking.JPG";
            mark_illigal_cars(m_base_photo_path, parking_img_path);

            XImage image = XImage.FromFile(parking_img_path);
            double x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;
            gfx.DrawImage(image, 80, image_line, 500, 280);
            line += 650;
            image_line += 300;

            if (UnauthorizedCars.Count == 0)
            {
                gfx.DrawString("No cars where parking illigaly", normal_font, XBrushes.Black, new XRect(40, 0, page.Width, line), XStringFormats.CenterLeft);
            }
            else
            {
                gfx.DrawString("Unauthorized Cars:", sub_title_font, XBrushes.Black, new XRect(40, 0, page.Width, line), XStringFormats.CenterLeft);
                line += 50;
                image_line += 50;

                foreach (KeyValuePair<string, string> car in UnauthorizedCars)
                {
                    if (line > 1400)
                    {
                        page = document.AddPage();

                        gfx = XGraphics.FromPdfPage(page);

                        line = 70;
                        image_line = 50;
                    }
                    gfx.DrawString("car plate: " + car.Key, normal_font, XBrushes.Black, new XRect(60, 0, page.Width, line), XStringFormats.CenterLeft);
                    image = XImage.FromFile(car.Value);
                    x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;
                    gfx.DrawImage(image, 80, image_line, 250, 140);
                    line += 350;
                    image_line += 175;
                }
            }

            document.Save(path);
        }

        private Boolean isUnauthorizedCarPlate(string car_plate)
        {
            if (allowed_car_plates == null)
            {
                allowed_car_plates = DB.getAllAllowedCarPlates();
            }

            return !allowed_car_plates.Contains(car_plate);
        }

        private void mark_illigal_cars(string base_photo_path, string base_photo_output_path)
        {
            Mat img = CvInvoke.Imread(base_photo_path, Emgu.CV.CvEnum.ImreadModes.AnyColor);

            foreach (Car car in unauthorized_cars)
            {
                Rectangle rect = car.GetRectangle();
                MCvScalar color = new MCvScalar(0, 0, 255);
                CvInvoke.Rectangle(img, rect, color, 2);
            }

            CvInvoke.Imwrite(base_photo_output_path, img);
        }

    }
}
