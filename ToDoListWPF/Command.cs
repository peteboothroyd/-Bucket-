using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoTrainingGit
{
    public static class Command
    {
        public static RoutedUICommand ClearList = new RoutedUICommand("Clear List", "ClearList", typeof(Command), new InputGestureCollection()
            {
                new KeyGesture(Key.C, ModifierKeys.Alt, "Alt-C")
            });

        public static RoutedUICommand ViewAll = new RoutedUICommand("View All", "ViewAll", typeof(Command), new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Alt, "Alt-A")
            });

        public static RoutedUICommand ViewCompleted = new RoutedUICommand("View Finished", "ViewCompleted", typeof(Command), new InputGestureCollection()
            {
                new KeyGesture(Key.F, ModifierKeys.Alt, "Alt-F")
            });

        public static RoutedUICommand ViewUnfinished = new RoutedUICommand("View Unfinished", "ViewUnfinished", typeof(Command), new InputGestureCollection()
            {
                new KeyGesture(Key.U, ModifierKeys.Alt, "Alt-U")
            });

        public static RoutedUICommand ViewFiltered = new RoutedUICommand("View Filtered", "ViewFiltered", typeof(Command));

        public static RoutedUICommand Connect = new RoutedUICommand("Connect", "Connect", typeof(Command));
    }
}
