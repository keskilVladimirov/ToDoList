using System;
using ToDoList.Redux;
using ToDoList.Redux.States;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTasksPage : ContentPage
    {
        private TodoTasks todoTasksTemp;
        private string statusTemp;

        public EditTasksPage(TodoTasks todoTasks)
        {
            InitializeComponent();

            todoTasksTemp = todoTasks;
            IdLabel.Text = todoTasks.Id.ToString();
            UserNameLabel.Text = todoTasks.Username;
            EmailLabel.Text = todoTasks.Email;
            TextEntry.Text = todoTasks.Text;
            ButtonStatus.Text = todoTasks.Status;
            statusTemp = todoTasks.Status;
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (TextEntry.Text == null || TextEntry.Text == "")
            {
                await DisplayAlert("Внимание", "Нужно заполнить поле 'Краткое описание'!", "Понятно");
                return;
            }

            todoTasksTemp.Text = TextEntry.Text;
            todoTasksTemp.Status = ButtonStatus.Text;

            switch (todoTasksTemp.Status)
            {
                case "Задача не выполнена":
                    todoTasksTemp.Status = "0";
                    break;
                case "Задача не выполнена, отредактирована админом":
                    todoTasksTemp.Status = "1";
                    break;
                case "Задача выполнена":
                    todoTasksTemp.Status = "10";
                    break;
                case "Задача отредактирована админом и выполнена":
                    todoTasksTemp.Status = "11";
                    break;
            }

            var status = await ActionCreator.EditTodoTasksAsync(todoTasksTemp);

            if (status == "ok")
            {
                await DisplayAlert("Внимание", "Задача успешно сохранена.", "Ок");
                App.Current.MainPage = new AppShell();
            }
            else
                await DisplayAlert("Внимание", "Что то пошло не так...", "Ок");
        }

        private async void ButtonStatus_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Статус", "Отмена", null, "Задача не выполнена", "Задача не выполнена, отредактирована админом", "Задача выполнена", " Задача отредактирована админом и выполнена");
            if (action == "Отмена" || action == null)
                action = statusTemp;

            ButtonStatus.Text = action;
        }
    }
}