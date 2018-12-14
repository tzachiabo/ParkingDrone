using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class Response
    {
        private int m_key;
        private Status m_status;
        private MissionType m_type;
        private Object m_data;

        public int Key { get => m_key; set => m_key = value; }
        public Status Status { get => m_status; set => m_status = value; }
        public object Data { get => m_data; set => m_data = value; }
        public MissionType Type { get => m_type; set => m_type = value; }

        public Response(int key, Status status,MissionType type, Object data=null)
        {
            Key = key;
            m_status = status;
            m_data = data;
            Type = type;
        }
    }
}
