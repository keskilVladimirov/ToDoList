using Redux;
using ToDoList.Models;

namespace ToDoList.Redux.Actions
{
    public class LoadTasksSuccessAction : IAction
    {
        public ResponseTasks ResponseTasks { get; }

        public LoadTasksSuccessAction(ResponseTasks responseTasks)
        {
            ResponseTasks = responseTasks;
        }
    }
}
