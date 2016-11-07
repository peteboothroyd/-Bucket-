using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ToDoTrainingGit.Commands;

namespace ToDoTrainingGit
{
    public class ToDoApplication
    {
        internal const string FILE_EXTENSION = ".xml";
        internal const string ROOT_NAME = "root";
        internal const string TASK_XML_NAME = "Task";
        internal const int DESCRIPTION_LENGTH = 100;
        internal const int NAME_LENGTH = 10;
        internal const int TAG_LENGTH = 100;
        internal const string MARK_AFFIRMATIVE = "completed";
        internal const string MARK_NEGATIVE = "unfinished";
        internal const string CONFIRMATION_AFFIRMATION = "yes";
        internal const string CONFIRMATION_NEGATIVE = "no";
        internal const string UP_DIRECTION = "up";
        internal const string DOWN_DIRECTION = "down";
        internal const string EXIT_STRING = "exit";
        internal const string FOLDER_LOCATION = "\\\\DriveWorks\\Thelwall\\Profiles\\peterboothroyd" +
            "\\My Documents\\Visual Studio 2013\\Projects\\ConsoleApplication1\\ConsoleApplication1\\";
        internal const string NAME_ATTRIBUTION = "name";
        internal const string DESCRIPTION_ATTRIBUTION = "description";
        internal const string MARKED_ATTRIBUTE = "marked";
        internal const string TAGS_ATTRIBUTE = "tags";
        internal const string SORTORDER_ATTRIBUTE = "sortorder";
        internal const string ISVIEWABLE_ATTRIBUTE = "isviewable";

        private List<Task> mToDoList = new List<Task>();
        private Stack<List<CommandBase>> mCommandStack;
        private int mToDoCount;
        public IHubProxy Hub{ get; set; }
        public HubConnection HubConnection { get; set; }

        public List<Task> Todos
        {
            get
            {
                return mToDoList;
            }
            set
            {
                mToDoList = value;
            }
        }

        public Stack<List<CommandBase>> CommandStack
        {
            get
            {
                return mCommandStack;
            }
        }

        public int ToDoCount
        {
            get
            {
                mToDoCount = mToDoList.Count;
                return mToDoCount;
            }
        }

        internal void Run()
        {
            Console.WriteLine("Type exit at any point to return to main menu.");

            mCommandStack = new Stack<List<CommandBase>>();
            mCommandStack.Push(new List<CommandBase>() 
            { 
                new CreateTaskCommand(this),
                new RenameTaskCommand(this),
                new UpdateDescriptionTaskCommand(this),
                new DeleteTaskCommand(this),
                new ChangeTaskTagsCommand(this),
                new ListTaskCommand(this),
                new MarkTaskCommand(this),
                new ClearTaskCommand(this),
                new ImportTaskCommand(this),
                new ExportTaskCommand(this),
                new ReorderTaskCommand(this),
                new RefreshTasksCommand(this),
                new BackCommand(this)
            });
            
            string url = @"http://localhost:8080/";
            StartConnection startConnection = new StartConnection(url, "ToDoHub", this);
            startConnection.Execute();

            while (mCommandStack.Count > 0)
            {
                var currentCommands = mCommandStack.Peek();

                Console.WriteLine("\n{0}:", string.Join(", ", currentCommands.Select(c => c.Name)));

                string userInput = Console.ReadLine().Trim();

                var matchingCommand = currentCommands.FirstOrDefault(command => string.Equals(command.Name, userInput, StringComparison.OrdinalIgnoreCase));
                if (Check.ExitCheck(userInput))
                    break;

                if (matchingCommand == null)
                {
                    Console.WriteLine("Command not recognised. Try again.");
                    continue;
                }
                
                matchingCommand.Execute();
            }
        }
    }
}
