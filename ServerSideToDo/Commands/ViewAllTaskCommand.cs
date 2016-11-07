using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewAllTaskCommand : CommandBase
    {
        private User mUser;

        public ViewAllTaskCommand(User user)
            : base("View All")
        {
            mUser = user;
        }

        public override void Execute()
        {
            foreach (Task task in mUser.Tasks)
            {
                task.IsViewable=true;
            }
        }
    }
}
