using ToDoList.Redux.States;

namespace ToDoList.Models
{
    public class MessageTasks
    {
        public TodoTasks[] Tasks { get; set; }
        public int TotalTaskCount { get; set; }
    }
}
