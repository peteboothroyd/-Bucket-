using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewTagsCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mTagName;

        public ViewTagsCommand(MainWindow mainwindow, string tagname)
            : base("View Tags")
        {
            mMainWindow = mainwindow;
            mTagName = tagname;
        }

        public override void Execute()
        {
            mMainWindow.Hub.Invoke("ClientViewTagsRequest", mTagName).Wait();
        }
    }
}
