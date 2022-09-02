using Redux;
using ToDoList.Redux.Actions;
using ToDoList.Redux.States;

namespace ToDoList.Redux.Reducers
{
    public static class Reducer
    {
        public static TodoState Execute(TodoState previousState, IAction action)
        {

            if (action is LoadTasksAction)
            {
                previousState.IsLoading = true;
            }

            if (action is LoadTasksSuccessAction success)
            {
                previousState.TasksList.Clear();
                foreach (var item in success.ResponseTasks.Message.Tasks)
                {
                    switch (item.Status)
                    {
                        case "0":
                            item.Status = "Задача не выполнена";
                            break;
                        case "1":
                            item.Status = "Задача не выполнена, отредактирована админом";
                            break;
                        case "10":
                            item.Status = "Задача выполнена";
                            break;
                        case "11":
                            item.Status = "Задача отредактирована админом и выполнена";
                            break;
                    }
                    previousState.TasksList.Add(item);
                }
                previousState.IsLoading = false;
            }

            if (action is LoadTasksErrorAction)
            {
                previousState.IsLoading = false;
            }

            return previousState;
        }
    }
}
