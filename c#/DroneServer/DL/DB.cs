using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.IO;

using DroneServer.SharedClasses;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace DroneServer
{
    class DB
    {
        static string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\DL\DroneDB.mdf;";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void addParking(Parking parking)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = new SqlConnection(cs);


            string qry = "INSERT INTO Parking (Name, MinZoom,MaxZoom,Zoom,LocationLat,LocationLng)" +
                         "VALUES(N'" + parking.name + "', " + parking.minZoom + ", " + parking.maxZoom + ", " + parking.zoom + ", " + parking.lat + ", " + parking.lng + ");";

            con.Open();
            cmd = new SqlCommand(qry, con);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("err");
                cmd.Dispose();
                con.Close();
                return;
            }
            cmd.Dispose();
            con.Close();


            if (parking.border == null)
                return;
            for (int i = 0; i < parking.border.Count; i++)
            {
                SqlConnection con2;
                SqlCommand cmd2;
                con2 = new SqlConnection(cs);
                con2.Open();
                qry = "INSERT INTO BorderPoint (ParkingName,PointID,Lat,Lng)" +
                         "VALUES(N'" + parking.name + "', " + i + ", " + parking.border[i].y + ", " + parking.border[i].x + ");";
                cmd2 = new SqlCommand(qry, con2);
                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("err");
                    cmd.Dispose();
                    con.Close();
                    return;
                }
                cmd2.Dispose();
                con2.Close();
            }



            cmd.Dispose();
            con.Close();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<Parking> selectAllParkings()
        {
            SqlConnection con;
            SqlCommand cmd;
            con = new SqlConnection(cs);

            string qry = "select * from Parking";

            con.Open();
            cmd = new SqlCommand(qry, con);

            List<Parking> s = new List<Parking>();
            try
            {
                SqlDataReader reader= cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    
                    Parking p = new Parking(reader.GetValue(0)+"",
                                            Convert.ToDouble(reader.GetValue(4)),
                                            Convert.ToDouble(reader.GetValue(5)),
                                            Convert.ToDouble(reader.GetValue(3)),
                                            Convert.ToDouble(reader.GetValue(2)),
                                            Convert.ToDouble(reader.GetValue(1)),
                                            new List<Point>());
                    s.Add(p);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("err");
                cmd.Dispose();
                con.Close();
                return null;
            }
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < s.Count; i++)
            {
                qry = "select Lat,Lng from BorderPoint where ParkingName='"+s[i].name+"' order by PointID";
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand(qry, con);

                List<Point> l = new List<Point>();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Point p = new Point((double)reader.GetValue(0), (double)reader.GetValue(1));
                            l.Add(p);
                        }
                    }
                    s[i].border = l;
                }
                catch (Exception) {
                    Console.WriteLine("err2");
                    cmd.Dispose();
                    con.Close();
                    return null ;
                }

                cmd.Dispose();
                con.Close();
            }



            return s;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void deleteParking(string name)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = new SqlConnection(cs);


            string qry = "DELETE from Parking where Name='"+ name + "';"+
                         "DELETE from BorderPoint where ParkingName='" + name + "';";

            con.Open();
            cmd = new SqlCommand(qry, con);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("err");
                cmd.Dispose();
                con.Close();
                return;
            }
            cmd.Dispose();
            con.Close();
        }

    }
}
