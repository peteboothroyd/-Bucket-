using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class MoveTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public MoveTaskCommand(ToDoApplication toDoApplication)
            : base("Move")
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

            var direction = Check.AskQuestion("Please enter direction:",
                possibleName => Check.EitherOrQuestionCheck(possibleName, ToDoApplication.UP_DIRECTION, ToDoApplication.DOWN_DIRECTION));
            if (Check.ExitCheck(taskName))
                return;

            bool mDirection;
            if (direction == ToDoApplication.UP_DIRECTION)
                mDirection = true;
            else mDirection = false;

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();

                Task taskToChange = mApplication.Todos.FirstOrDefault<Task>(task => task.Name == taskName);

                mApplication.Hub.Invoke("ClientMoveTaskRequest", taskToChange.ID, mDirection).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
