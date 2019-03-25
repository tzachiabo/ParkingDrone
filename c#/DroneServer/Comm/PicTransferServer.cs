using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneServer.BL.Comm
{
    public class PicTransferServer
    {
        private Boolean running;

        Thread thread;
        private TcpListener m_server;
        public static int m_index;

        public PicTransferServer()
        {
            m_index = 0;

            running = true;
            thread = new Thread(() =>
            {
                Configuration conf = Configuration.getInstance();
                int port = Int32.Parse(conf.get("pic_server_port"));

                m_server = new TcpListener(IPAddress.Any, port);
                m_server.Start();
                Logger.getInstance().debug("Pic Transfer Server has started");

                while (running)
                {
                    TcpClient client;
                    try
                    {
                        Logger.getInstance().debug("Pic Transfer Server wait for client");

                        client = m_server.AcceptTcpClient();
                        Logger.getInstance().debug("Pic Transfer Server recive a client");

                        m_index++;
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        Assertions.verify(running == false, "socket got unexpected exception");
                        return;
                    }

                    NetworkStream ns = client.GetStream();

                    byte[] bytes = new byte[1024];

                    String location = m_index + ".JPG";
                    FileStream pic_file = File.Create(location);
                    int write_amount = 0;
                    int offset = 0;

                    while ((write_amount = ns.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        pic_file.Write(bytes, 0, write_amount);
                        pic_file .Flush();
                        offset += write_amount;
                    }

                    pic_file.Close();

                    client.Close();
                }
            });
            thread.Start();
        }

        public static String getLastPicPath()
        {
            return m_index + ".JPG";
        }


        public void shutDown()
        {
            running = false;
        }
    }
}
