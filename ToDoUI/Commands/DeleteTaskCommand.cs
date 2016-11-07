using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class DeleteTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public DeleteTaskCommand(MainWindow MainWindow)
            : base("Delete")
        {
            mMainWindow = MainWindow;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            Task deletedTask = (Task)mMainWindow.TaskListBox.SelectedItem;
            todos.Remove(deletedTask);
        }
    }
}
