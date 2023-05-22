using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;
using WebApp.Database.Model;

namespace WebApp.Test
{
    // This project designed to simplify the process of solving code problems for you.
    // I recommend you to write tests to verify your code. But you can go by your own way and it's not a bad choice.
    // Remember that it's just a recommendation and presence or absence of tests will not have a large affect on
    // evaluation of result. 90% of the assessment will consist of quantity and quality of solved TODOs.
    // Good luck.:)
    [TestFixture]
    public class MyTestBase
    {
        private WebAppTestEnvironment Env { get; set; }
        private HttpClient Client { get; set; }
        protected HttpClient AliceClient { get; set; }
        protected HttpClient BobClient { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            Env = new WebAppTestEnvironment();
            Env.Start();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Env.Dispose();
            Client.Dispose();
        }

        [SetUp]
        public void Prepare()
        {
            Env.Prepare();
            Client = Env.WebAppHost.GetClient();
            
            var alice = new LoginAccount
            {
                UserName = "alice@mailinator.com",
                Password = null
            };
            
            var bob = new LoginAccount
            {
                UserName = "bob@mailinator.com",
                Password = null
            };
            
            AliceClient = CreateAuthorizedClientAsync(alice).GetAwaiter().GetResult(); 
            BobClient = CreateAuthorizedClientAsync(bob).GetAwaiter().GetResult();
        }


        private async Task<HttpClient> CreateAuthorizedClientAsync(LoginAccount account)
        {
            var client = Env.WebAppHost.GetClient();
            
            var json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var res = await client.SignInAsync(content);
            client.DefaultRequestHeaders.Add(HeaderNames.Cookie, res.Headers.GetValues(HeaderNames.SetCookie));
            return client;
        }
    }
}