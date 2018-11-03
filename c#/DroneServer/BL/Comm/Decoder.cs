﻿using DroneServer.SharedClasses;
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

                    break;
                case "getStatus":

                    break;
                case "isAlive":

                    break;
                case "getLocation":

                    break;
                case "":
                    return new Response(0, Status.Ok, MissionType.EndOfSocket, null);
            }
            Assertions.verify(false, "decoder faild to decode the recived message : " + data);
            return null;
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
