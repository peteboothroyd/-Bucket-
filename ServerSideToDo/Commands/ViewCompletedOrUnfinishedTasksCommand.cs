using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewCompletedOrUnfinishedTasksCommand : CommandBase
    {
        private User mUser;
        private bool mCompletedOrUnfinished;

        public ViewCompletedOrUnfinishedTasksCommand(User user, bool CompletedOrUnfinished)
            : base("View Completed")
        {
            mUser = user;
            mCompletedOrUnfinished = CompletedOrUnfinished;
        }

        public override void Execute()
        {
            foreach (Task task in mUser.Tasks)
            {
                task.IsViewable = false;
            }

            var filteredToDos = mUser.Tasks.Where(task => task.Marked == mCompletedOrUnfinished);

            foreach (Task task in filteredToDos)
            {
                task.IsViewable = true;

            }
        }
    }
}
