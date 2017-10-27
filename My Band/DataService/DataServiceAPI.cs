using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using My_Band.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace My_Band.DataService
{
    public class DataServiceAPI
    {
        HttpClient client = new HttpClient();
      
        private const string _urlBase = "http://XamarinWebAPI2.somee.com/XamarinWebAPI/api/";


        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                string list = _urlBase + "home/list/";
                var response = await client.GetStringAsync(list);
                var users = JsonConvert.DeserializeObject<List<UserModel>>(response);
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Boolean> AddUsersAsync(UserModel user)
        {
            try
            {
                var data = JsonConvert.SerializeObject(user);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                string urlAddUser = _urlBase + "home/post";

                HttpResponseMessage response = null;

                response = await client.PostAsync(urlAddUser, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir o usuário");
                }
                
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUser(UserModel user)
        {
            string update = _urlBase + "home/Put";
            var uri = new Uri(string.Format(update, user.ID));

            var data = JsonConvert.SerializeObject(user);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(update, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar o usuário!");
            }
        }

        public async Task DeleteUser(UserModel user)
        {
            string delete = _urlBase + "home/DeleteGet";
            var uri = new Uri(string.Format(delete, user.ID));
            await client.DeleteAsync(uri);
        }
        public async Task<TokenModel> PostLogin(UserLoginModel userLogin)
        {
            try
            {
                string urlLogin = _urlBase + "token";

                var user = new List<KeyValuePair<string, string>>();
                user.Add(new KeyValuePair<string, string>("grant_type", "password"));
                user.Add(new KeyValuePair<string, string>("username", userLogin.username));
                user.Add(new KeyValuePair<string, string>("password", userLogin.password));
                user.Add(new KeyValuePair<string, string>("client_id", userLogin.username));
                user.Add(new KeyValuePair<string, string>("client_password", userLogin.password));

                var request = new HttpRequestMessage(HttpMethod.Post, urlLogin) {Content = new FormUrlEncodedContent(user) };

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    TokenModel token = JsonConvert.DeserializeObject<TokenModel>(responseContent);
                
                    return token;
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<UserModel> FindByName(string name, TokenModel token)
        {
            try
            {
                string urlFind = _urlBase + "home/findbyname?name=" + name;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.Token_Type, token.Access_Token);
                HttpResponseMessage response = await client.GetAsync(urlFind);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(responseContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
