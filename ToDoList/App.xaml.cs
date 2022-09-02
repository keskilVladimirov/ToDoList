using Redux;
using ToDoList.Redux.Reducers;
using ToDoList.Redux.States;
using Xamarin.Forms;

namespace ToDoList
{
    public partial class App : Application
    {
        public static Store<TodoState> TodoStore { get; set; }

        public App()
        {
            InitializeComponent();
            TodoStore = new Store<TodoState>(Reducer.Execute, TodoState.InitialState);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
