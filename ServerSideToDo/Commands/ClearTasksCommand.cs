using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class ClearTasksCommand : CommandBase
    {
        private User mUser;

        public ClearTasksCommand(User user)
            : base("Clear")
        {
            mUser = user;
        }

        public override void Execute()
        {
            mUser.Tasks.Clear();
        }
    }
}
