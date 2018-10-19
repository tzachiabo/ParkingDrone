﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DroneServer.BL
{
    public class Configuration
    {
        private static Configuration m_instance;
        private XmlDocument m_doc;

        private Configuration()
        {
            m_doc = new XmlDocument();
            m_doc.Load("BaseConfig.txt");//TODO 

        }

        public static Configuration getInstance()
        {
            if (m_instance == null)
            {
                m_instance = new Configuration();
            }
            return m_instance;
        }

        public String get(String name)
        {
            XmlNode node = m_doc.DocumentElement.SelectSingleNode(name);
            return node.InnerText;
        }
    }
}
