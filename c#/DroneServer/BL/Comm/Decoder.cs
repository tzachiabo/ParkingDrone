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

                    break;
                case "takeOff":
                    return parseTakeOff(words);

                case "landing":
                    return parseLanding(words);

                case "goHome":

                    break;
                case "moveGimbal":

                    break;
                case "goToGPS":

                    break;
                case "takePhoto":

                    break;
                case "stop":

                    break;
                case "getStatus":

                    break;
                case "isAlive":

                    break;
                case "getLocation":

                    break;
            }
            return null;
        }

        private static Response parseTakeOff(string[] sentance) // TODO assert sentance[2] == "Done"
        {
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

        private static Response parseLanding(string[] sentance) // TODO assert sentance[2] == "Done"
        {
            Response res = new Response(Int32.Parse(sentance[1]), Status.Ok, MissionType.MainMission, null);
            return res;
        }

    }

   
}
