using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToDoTrainingGit
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var application = new ToDoApplication();
            application.Run();
        }
    }
}