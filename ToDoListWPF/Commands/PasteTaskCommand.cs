using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class PasteTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private Task mTask;

        public PasteTaskCommand(MainWindow mainwindow, Task task)
            : base("Paste")
        {
            mMainWindow = mainwindow;
            mTask = task;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientPasteTaskRequest", mTask.Name, mTask.Description, mTask.Marked, mTask.SortOrder, mTask.Tags, mTask.IsViewable).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
