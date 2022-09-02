using System.Collections.ObjectModel;

namespace ToDoList.Redux.States
{
    public class TodoState
    {
        public bool IsLoading { get; set; }
        public ObservableCollection<TodoTasks> TasksList { get; set; } = new ObservableCollection<TodoTasks>();

        public static TodoState InitialState = new TodoState()
        {
            IsLoading = false,
            TasksList = new ObservableCollection<TodoTasks>()
        };
    }
}
