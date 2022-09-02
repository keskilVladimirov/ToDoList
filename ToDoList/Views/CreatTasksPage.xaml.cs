using System;
using System.Text.RegularExpressions;
using ToDoList.Redux;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatTasksPage : ContentPage
    {
        public const string EMAIL_PATTERN = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

        public CreatTasksPage()
        {
            InitializeComponent();
        }

        private async void ButtonSet_Clicked_Creat(object sender, EventArgs e)
        {

            if (UserNameEntry.Text == null || EmailEntry.Text == null || TextEntry.Text == null ||
                UserNameEntry.Text == "" || EmailEntry.Text == "" || TextEntry.Text == "")
            {
                await DisplayAlert("Внимание", "Нужно заполнить все поля!", "Понятно");
                return;
            }

            if (!Regex.IsMatch(EmailEntry.Text, EMAIL_PATTERN))
            {
                await DisplayAlert("Внимание", "Email не валиден!", "Понятно");
                return;
            }

            var status = await ActionCreator.CreateTodoTasksAsync(UserNameEntry.Text, EmailEntry.Text, TextEntry.Text);

            if (status == "ok")
            {
                await DisplayAlert("Внимание", "Задача успешно создана.", "Ок");
                App.Current.MainPage = new AppShell();
            }
            else
                await DisplayAlert("Внимание", "Что то пошло не так...", "Ок");
        }
    }
}