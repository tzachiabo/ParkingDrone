using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class Response
    {
        private String m_key;
        private Status m_status;
        private MissionType m_type;
        private Object m_data;

        public string Key { get => m_key; set => m_key = value; }
        public Status Status { get => m_status; set => m_status = value; }
        public object Data { get => m_data; set => m_data = value; }

        public Response(String key, Status status,MissionType type, Object data)
        {
            Key = key;
            m_status = status;
            m_data = data;
            m_type = type;
        }
    }
}
