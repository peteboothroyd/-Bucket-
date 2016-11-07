using Microsoft.Owin.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTrainingGit.Commands;

namespace ToDoTrainingGit
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://localhost:8080/";
            using (WebApp.Start<Startup>(url))
            {
                ToDoHub.UserDictionary = new ConcurrentDictionary<string, User>();
                Console.WriteLine(string.Format("Server running at {0}", url));
                Console.ReadLine();
            }
        }
    }
}
