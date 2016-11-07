using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewAllTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public ViewAllTaskCommand(MainWindow MainWindow)
            : base("View All")
        {
            mMainWindow = MainWindow;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            foreach (Task task in todos)
            {
                task.IsViewable = true;
            }
        }
    }
}
