using System;
using ToDoList.Redux;
using ToDoList.Redux.States;
using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            App.TodoStore.Subscribe(state =>
            {
                TodoList.IsRefreshing = state.IsLoading;
                TodoList.ItemsSource = state.TasksList;
            });
        }

        protected override async void OnAppearing()
        {
            await App.TodoStore.DispatchAsync(ActionCreator.LoadTodoTasksAsync(null, null, null));
        }

        private async void TodoList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tasks = TodoList.SelectedItem as TodoTasks;
            TodoList.SelectedItem = null;

            if (ActionCreator.Token == "")
            {
                await DisplayAlert("Внимание", "Вы не авторизованы, авторизуйтесь!", "Ок");
                return;
            }

            if (tasks != null)
                await Navigation.PushAsync(new EditTasksPage(tasks));
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var field = Picker.Items[Picker.SelectedIndex];
            await App.TodoStore.DispatchAsync(ActionCreator.LoadTodoTasksAsync(field, null, null));
        }

        private async void Button_Clicked_Set(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Направление сортировки", "Отмена", null, "asc", "desc");

            if (action == "Отмена" || action == null)
            {
                ButtonSet.Text = "Set";
                action = "asc";
            }
            else
                ButtonSet.Text = action;

            await App.TodoStore.DispatchAsync(ActionCreator.LoadTodoTasksAsync(null, null, action));
        }

        private async void Button_Clicked_Forth(object sender, EventArgs e)
        {
            await App.TodoStore.DispatchAsync(ActionCreator.LoadTodoTasksAsync(null, 1, null));
        }

        private async void Button_Clicked_Back(object sender, EventArgs e)
        {
            await App.TodoStore.DispatchAsync(ActionCreator.LoadTodoTasksAsync(null, -1, null));
        }
    }
}
