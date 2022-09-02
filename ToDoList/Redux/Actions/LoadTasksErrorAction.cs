using Redux;

namespace ToDoList.Redux.Actions
{
    public class LoadTasksErrorAction : IAction
    {
        public string Error { get; }

        public LoadTasksErrorAction(string error)
        {
            Error = error;
        }
    }
}
