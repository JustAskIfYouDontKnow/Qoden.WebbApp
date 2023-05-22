using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Test
{
    public static class ApiExtensions
    {
        public static async Task<HttpResponseMessage> SignInAsync(this HttpClient client, HttpContent account) =>
            await client.PostAsync($"api/Login/Login", account);

        public static async Task GetAccountAsync(this HttpClient client) => await client.GetAsync("api/Account/GetAccountFromCookies");

        public static async Task<HttpResponseMessage> GetAccountByIdAsync(this HttpClient client, int id) => 
            await client.GetAsync($"api/Account/GetByInternalId?Id={id}");

        public static async Task CountAsync(this HttpClient client) => 
            await client.PostAsync("api/Account/UpdateCount", null);

        public static async Task<T> Response<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<T>();
        }
    }
}