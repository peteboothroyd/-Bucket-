using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewCompletedTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public ViewCompletedTaskCommand(MainWindow MainWindow)
            : base("View Completed")
        {
            mMainWindow = MainWindow;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            mMainWindow.ViewFilter = task => task.Marked;

            foreach (Task task in todos)
            {
                task.IsViewable = false;
            }

            var tempTodos = todos.Where(mMainWindow.ViewFilter);
            foreach (Task task in tempTodos)
            {
                task.IsViewable = true;

            }
        }
    }
}
