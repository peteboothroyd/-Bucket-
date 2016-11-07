using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDoTrainingGit
{
    class Check
    {
        public static bool XMLNodeValidCheck(XElement node)
        {
            if (node.Attribute("name") != null && node.Attribute("description")
                != null && node.Attribute("marked") != null && node.Attribute("tags") != null)
            {
                return true;
            }
            return false;
        }

        public static bool ListCountGreaterZero(MainWindow MainWindow)
        {
            if (MainWindow.Tasks.Count > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
