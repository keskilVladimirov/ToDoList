using System;
using ToDoList.Redux;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            CheckLogin();
        }

        private async void CheckLogin()
        {
            if (ActionCreator.Token != "")
            {
                bool result = await DisplayAlert("Внимание", "Вы авторизованы!", "Ок", "Выход");
                if (result)
                {
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Внимание", "Вы успешно вышли из аккаунта.", "Ок");
                    ActionCreator.Token = "";
                }
            }
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            if (LoginEntry.Text == null || PasswordEntry.Text == null ||
                LoginEntry.Text == "" || PasswordEntry.Text == "")
            {
                await DisplayAlert("Внимание", "Нужно заполнить все поля!", "Понятно");
                return;
            }

            var status = await ActionCreator.AuthorizationTodoTasksAsync(LoginEntry.Text, PasswordEntry.Text);

            if (status == "ok")
            {
                await DisplayAlert("Внимание", "Вы успешно авторизовались.", "Ок");
                App.Current.MainPage = new AppShell();
            }
            else
                await DisplayAlert("Внимание", "Login или password не верны!", "Ок");
        }
    }
}