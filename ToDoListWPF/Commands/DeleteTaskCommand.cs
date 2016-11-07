using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class DeleteTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public DeleteTaskCommand(MainWindow mainwindow)
            : base("Delete")
        {
            mMainWindow = mainwindow;
        }

        public override void Execute()
        {
            try
            {
                Task deletedTask = (Task)mMainWindow.TaskListBox.SelectedItem;
                mMainWindow.Hub.Invoke("ClientDeleteTaskRequest", deletedTask.ID).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
