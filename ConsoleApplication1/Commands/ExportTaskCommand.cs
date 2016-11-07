using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDoTrainingGit.Commands
{
    class ExportTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ExportTaskCommand(ToDoApplication toDoApplication)
            : base("Export")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mApplication))
            {
                Console.WriteLine("Cannot export when todo list is empty.");
                return;
            }

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var taskName = Check.AskQuestion("Type in name of file to export to:", Check.XMLFileCheck);
            if (Check.ExitCheck(taskName))
                return;

            XDocument mExportedDoc = new XDocument();

            XElement xmlTree = new XElement(ToDoApplication.ROOT_NAME);

            var mOrderedtodos = mApplication.Todos.OrderBy(p => p.SortOrder);
            Parallel.ForEach(mOrderedtodos, task =>
           {
               XElement taskXName = new XElement(ToDoApplication.TASK_XML_NAME,
               new XAttribute(ToDoApplication.NAME_ATTRIBUTION, task.Name),
               new XAttribute(ToDoApplication.DESCRIPTION_ATTRIBUTION, task.Description),
               new XAttribute(ToDoApplication.MARKED_ATTRIBUTE, task.Marked.ToString()),
               new XAttribute(ToDoApplication.TAGS_ATTRIBUTE, task.Tags),
               new XAttribute(ToDoApplication.SORTORDER_ATTRIBUTE, task.SortOrder),
               new XAttribute(ToDoApplication.ISVIEWABLE_ATTRIBUTE, task.IsViewable)
               );
               xmlTree.Add(taskXName);
           });

            string fileNamePath = ToDoApplication.FOLDER_LOCATION + taskName + ToDoApplication.FILE_EXTENSION;
            mExportedDoc.Save(fileNamePath);
        }
    }
}
