using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ListTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ListTaskCommand(ToDoApplication toDoApplication)
            : base("List")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mApplication))
                return;

            mApplication.Hub.Invoke("ClientRequestTaskList").Wait();
            var listTodos = mApplication.Todos.Where(task => task.IsViewable).OrderBy(task => task.SortOrder);
           
            foreach (Task task in listTodos)
            {
                Console.WriteLine("\t Name: {0} \n\t Description: {1} \n\t Complete = {2} \n\t Tags = {3}" +
                    "\n------------------------------------------------",
                    task.Name, task.Description, task.Marked.ToString(), task.Tags);
            }
        }
    }
}
