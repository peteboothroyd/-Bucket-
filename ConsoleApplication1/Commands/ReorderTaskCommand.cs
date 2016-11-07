using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ReorderTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ReorderTaskCommand(ToDoApplication toDoApplication)
            : base("Reorder")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            var mHub = mApplication.Hub;
            mApplication.CommandStack.Push(new List<CommandBase>() 
            { 
                new MoveTaskCommand(mApplication),
                new ViewAllTaskCommand(mApplication),
                new ViewCompletedTaskCommand(mApplication),
                new ViewUnfinishedTaskCommand(mApplication),
                new ViewTagsCommand(mApplication),
                new BackCommand(mApplication)
            });
        }
    }
}
