using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class CreateTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public CreateTaskCommand(ToDoApplication toDoApplication)
            : base("Create")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            var taskName = Check.AskQuestion("Please enter name:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskName))
                return;

            var taskDescription = Check.AskQuestion("Enter description:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskDescription))
                return;

            string taskTags = Check.AskQuestion("Enter tags:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskTags))
                return;
            
            try
            {
                mApplication.Hub.Invoke("ClientCreateTaskRequest", taskName, taskDescription, taskTags).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
