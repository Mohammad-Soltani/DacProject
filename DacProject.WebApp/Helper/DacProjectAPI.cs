namespace DacProject.WebApp.Helper
{
    public class DacProjectAPI : IDacProjectAPI
    {
        public HttpClient Initial()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7123/");
            return client;
        }
    }
}
