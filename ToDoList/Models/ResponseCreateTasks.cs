using ToDoList.Redux.States;

namespace ToDoList.Models
{
    public class ResponseCreateTasks
    {
        public string Status { get; set; }
        public TodoTasks Message { get; set; }
    }
}
