using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class OpenFileCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mPath;

        public OpenFileCommand(MainWindow MainWindow, string Path)
            : base("Open")
        {
            mMainWindow = MainWindow;
            mPath = Path;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            try
            {
                XDocument mImportedDoc = XDocument.Load(mPath);

                foreach (XElement node in mImportedDoc.Root.Elements())
                {
                    if (Check.XMLNodeValidCheck(node))
                    {
                        string name = node.Attribute((String)Application.Current.Resources["NAME_ATTRIBUTION"]).Value.Trim();
                        string description = node.Attribute((String)Application.Current.Resources["DESCRIPTION_ATTRIBUTION"]).Value.Trim();
                        string marked = node.Attribute((String)Application.Current.Resources["MARKED_ATTRIBUTE"]).Value.Trim();
                        string tags = node.Attribute((String)Application.Current.Resources["TAGS_ATTRIBUTE"]).Value.Trim();
                        if (String.IsNullOrEmpty(name))
                            name = "";
                        if (String.IsNullOrEmpty(description))
                            description = "";
                        if (String.IsNullOrEmpty(tags))
                            tags = "";
                        Task newTask = new Task(name, description, tags, bool.Parse(marked));
                        todos.Add(newTask);
                    }
                }
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
