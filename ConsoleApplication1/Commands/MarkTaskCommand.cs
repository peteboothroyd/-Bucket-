using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class MarkTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public MarkTaskCommand(ToDoApplication toDoApplication)
            : base("Mark")
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

            var status = Check.AskQuestion("Enter new status of task, completed or unfinished:",
                possibleName => Check.EitherOrQuestionCheck(possibleName, ToDoApplication.MARK_AFFIRMATIVE, ToDoApplication.MARK_NEGATIVE));
            if (Check.ExitCheck(status))
                return;

            bool mIsChecked;
            if (status == ToDoApplication.MARK_AFFIRMATIVE)
                mIsChecked = true;
            else mIsChecked = false;

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();

                Task taskToChange = mApplication.Todos.FirstOrDefault<Task>(task => task.Name == taskName);

                mApplication.Hub.Invoke("ClientMarkTaskRequest", taskToChange.ID, mIsChecked).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
