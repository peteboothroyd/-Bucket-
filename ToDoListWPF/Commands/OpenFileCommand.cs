using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class OpenFileCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mPath;

        public OpenFileCommand(MainWindow mainwindow, string path)
            : base("Open")
        {
            mMainWindow = mainwindow;
            mPath = path;
        }

        public override void Execute()
        { 
            try
            {
                XDocument mImportedDoc = XDocument.Load(mPath);

                Parallel.ForEach(mImportedDoc.Root.Elements(), node =>
                {
                    if (Check.XMLNodeValidCheck(node))
                    {
                        string[] mAttributeArray = new string[6];
                        mAttributeArray[0] = node.Attribute((String)Application.Current.Resources["NAME_ATTRIBUTION"]).Value.Trim();
                        mAttributeArray[1] = node.Attribute((String)Application.Current.Resources["DESCRIPTION_ATTRIBUTION"]).Value.Trim();
                        mAttributeArray[2] = node.Attribute((String)Application.Current.Resources["MARKED_ATTRIBUTE"]).Value.Trim();
                        mAttributeArray[3] = node.Attribute((String)Application.Current.Resources["SORTORDER_ATTRIBUTE"]).Value.Trim();
                        mAttributeArray[4] = node.Attribute((String)Application.Current.Resources["TAGS_ATTRIBUTE"]).Value.Trim();
                        mAttributeArray[5] = node.Attribute((String)Application.Current.Resources["ISVIEWABLE_ATTRIBUTE"]).Value.Trim();

                        if (!Check.BoolCheck(mAttributeArray[2]))
                            return;

                        for (int i = 0, n = mAttributeArray.Length; i < n; i++)
                        {
                            if (String.IsNullOrEmpty(mAttributeArray[i]))
                                mAttributeArray[i] = "";
                        }

                        mMainWindow.Hub.Invoke("ClientImportTaskRequest", mAttributeArray[0], mAttributeArray[1], bool.Parse(mAttributeArray[2]), int.Parse(mAttributeArray[3]), mAttributeArray[4], bool.Parse(mAttributeArray[5])).Wait();
                    }
                });
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
