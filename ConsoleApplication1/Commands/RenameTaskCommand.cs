using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class RenameTaskCommand : CommandBase
    {
        private ToDoApplication mApplication;

        public RenameTaskCommand(ToDoApplication toDoApplication)
            : base("Rename")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mApplication))
                return;

            var taskName = Check.AskQuestionServer(mApplication, "Please enter name:", "ClientPresenceCheckRequest");
            if (Check.ExitCheck(taskName))
                return;

            var newTaskName = Check.AskQuestion("Please enter new name:", Check.EmptyStringCheck);
            if (Check.ExitCheck(newTaskName))
                return;

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();
                
                Task taskToChange = mApplication.Todos.FirstOrDefault<Task>(task => task.Name == taskName);

                mApplication.Hub.Invoke("ClientRenameTaskRequest", taskToChange.ID, newTaskName).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
