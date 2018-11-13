using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Comm
{
    class Decoder
    {
        public static Response decode(String data)
        {
            string[] words = data.Split(' ');
            switch (words[0])
            {
                case "move":
                    return parseMove(words);
                    
                case "takeOff":
                    return parseTakeOff(words);

                case "startLanding":
                    return parseStartLanding(words);

                case "confirmLanding":
                    return parseConfirmLanding(words);

                case "goHome":

                    break;
                case "moveGimbal":
                    return parseMoveGimbal(words);
                    
                case "goToGPS":
                    return parseGoToGPS(words);
                    
                case "takePhoto":

                    break;
                case "stop":
                    return parseStop(words);
                    
                case "getStatus":

                    break;
                case "isAlive":

                    break;
                case "getLocation":
                    return parseLocation(words);

                case "":
                    return new Response(0, Status.Ok, MissionType.EndOfSocket, null);
            }
            Assertions.verify(false, "decoder faild to decode the recived message : " + data);
            return null;
        }

        private static Response parseLocation(string[] sentance)
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            double lat = Double.Parse(sentance[4]);
            double lng = Double.Parse(sentance[5]);
            double alt = Double.Parse(sentance[3]);

            Logger.getInstance().debug("decode location update lat: " + lat + " lng: " + lng + " alt: " + alt);

            Point p = new Point(lng, lat, alt);
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, p);
            return res;
        }


        private static Response parseStop(string[] sentance)
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseMoveGimbal(string[] sentance)
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseGoToGPS(string[] sentance)
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseTakeOff(string[] sentance) 
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseStartLanding(string[] sentance) 
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseConfirmLanding(string[] sentance) 
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseMove(string[] sentance) 
        {
            Assertions.verify(sentance[2] == "Done", "message recive is not according to protocol");

            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

    }


}
