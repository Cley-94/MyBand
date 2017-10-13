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

namespace My_Band.DataService
{
    public class DataServiceAPI
    {
        HttpClient client = new HttpClient();
        private const string _urlBase = "http://XamarinWebAPI2.somee.com/XamarinWebAPI/api/Home";

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var response = await client.GetStringAsync(_urlBase);
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

                string urlAddUser = _urlBase + "/post";

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
            var uri = new Uri(string.Format(_urlBase, user.ID));

            var data = JsonConvert.SerializeObject(user);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(_urlBase, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar o usuário!");
            }
        }

        public async Task DeleteUser(UserModel user)
        {
            var uri = new Uri(string.Format(_urlBase, user.ID));
            await client.DeleteAsync(uri);
        }
        public async Task<Boolean> PostLogin(UserLoginModel userLogin)
        {
            try
            {
                string urlLogin = _urlBase + "/postlogin";

                var data = JsonConvert.SerializeObject(userLogin);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync(urlLogin, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if(responseContent == "true")
                    return true;
                else
                    return false;
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
