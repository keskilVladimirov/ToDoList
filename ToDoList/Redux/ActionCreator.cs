using Newtonsoft.Json;
using Redux;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Redux.Actions;
using ToDoList.Redux.States;

namespace ToDoList.Redux
{
    public delegate Task AsyncActionsCreator<TState>(Dispatcher dispatcher, Func<TState> getState);

    public static class ActionCreator
    {
        public const string API_URL = "https://uxcandy.com/~shapoval/test-task-backend/v2/";
        public const string CREATE = "create";
        public const string LOGIN = "login";
        public const string EDIT = "edit";
        public const string DEVELOPER = "?developer=keskil";
        public const string PAGE = "&page=";
        public const string SORT_FIELD = "&sort_field=";
        public const string SORT_DIRECTION = "&sort_direction=";

        private static int pageCount = 1;
        private static string sortFieldValue = "id";
        private static string sortDirectionValue = "asc";
        private static string token = "";

        public static string Token
        {
            get { return token; }
            set { token = value; }
        }


        public static AsyncActionsCreator<TodoState> LoadTodoTasksAsync(string field, int? count, string sortDirecValue)
        {
            return async (dispatch, getState) =>
            {
                dispatch(new LoadTasksAction());

                try
                {
                    if (field != null) sortFieldValue = field;
                    if (count != null) pageCount += count.Value;
                    if (pageCount < 1) pageCount = 1;
                    if (sortDirecValue != null) sortDirectionValue = sortDirecValue;

                    var client = new HttpClient();
                    var response = await client.GetAsync(API_URL + DEVELOPER + SORT_FIELD + sortFieldValue + SORT_DIRECTION + sortDirectionValue + PAGE + pageCount);
                    string result = await response.Content.ReadAsStringAsync();
                    var responseTasks = JsonConvert.DeserializeObject<ResponseTasks>(result);

                    if (responseTasks.Message.Tasks.Length == 0)
                    {
                        pageCount -= 1;
                        dispatch(new LoadTasksErrorAction("Error 0"));
                    }
                    else
                        dispatch(new LoadTasksSuccessAction(responseTasks));
                }
                catch (Exception ex)
                {
                    dispatch(new LoadTasksErrorAction(ex.ToString()));
                }
            };
        }

        public static async Task<string> CreateTodoTasksAsync(string username, string email, string text)
        {
            var status = "";
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("text", text)
                });

                var client = new HttpClient();
                var response = await client.PostAsync(API_URL + CREATE + DEVELOPER, formContent);
                string result = await response.Content.ReadAsStringAsync();
                var responseCreateTasks = JsonConvert.DeserializeObject<ResponseCreateTasks>(result);

                if (responseCreateTasks.Status == "ok")
                    status = "ok";
                else if (responseCreateTasks.Status == "error")
                    status = "error";
            }
            catch
            {
                status = "error";
            }
            return status;
        }

        public static async Task<string> AuthorizationTodoTasksAsync(string login, string password)
        {
            var status = "";
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", login),
                    new KeyValuePair<string, string>("password", password)
                });

                var client = new HttpClient();
                var response = await client.PostAsync(API_URL + LOGIN + DEVELOPER, formContent);
                string result = await response.Content.ReadAsStringAsync();
                var responseToken = JsonConvert.DeserializeObject<ResponseToken>(result);

                if (responseToken.Status == "ok")
                {
                    status = "ok";
                    token = responseToken.Message.Token;
                }
                else if (responseToken.Status == "error")
                    status = "error";
            }
            catch
            {
                status = "error";
            }
            return status;
        }

        public static async Task<string> EditTodoTasksAsync(TodoTasks todoTasks)
        {
            var status = "";
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("text", todoTasks.Text),
                    new KeyValuePair<string, string>("status", todoTasks.Status)
                });

                int userId = todoTasks.Id;
                string pathVariables = $"/{userId}";

                var client = new HttpClient();
                var response = await client.PostAsync(API_URL + EDIT + pathVariables + DEVELOPER, formContent);
                string result = await response.Content.ReadAsStringAsync();
                var responseToken = JsonConvert.DeserializeObject<ResponseToken>(result);

                if (responseToken.Status == "ok")
                    status = "ok";
                else if (responseToken.Status == "error")
                    status = "error";
            }
            catch
            {
                status = "error";
            }
            return status;
        }
    }
}
