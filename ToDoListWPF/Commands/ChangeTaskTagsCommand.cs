using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class ChangeTaskTagsCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private Task mTask;

        public ChangeTaskTagsCommand(MainWindow mainwindow, Task task)
            : base("Change tags")
        {
            mMainWindow = mainwindow;
            mTask = task;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientChangeTaskTagsRequest", mTask.ID, mTask.Tags).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
