using DacProject.WebApp.Helper;
using DacProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DacProject.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IDacProjectAPI _api;
        [BindProperty]
        public UserLoginViewModel credential { get; set; }

        public LoginModel(IDacProjectAPI api)
        {
            _api = api;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var myContent = JsonConvert.SerializeObject(credential);
            HttpClient client = _api.Initial();
            var response = await client.PostAsync("api/Logins/Authenticate", new StringContent(myContent, Encoding.UTF8, "application/json"));
            if(response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("token", token,
                new CookieOptions { Expires = DateTime.Now.AddMinutes(20) });
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var res = await client.GetAsync(string.Format("api/Users/GetUserByEmail?Email={0}", credential.Email));
                var obj = JsonConvert.DeserializeObject<UserViewModel>(res.Content.ReadAsStringAsync().Result);
                if (obj is null)
                {
                    return RedirectToPage("Login", new { UserHasExist = false });
                }
                return RedirectToPage("List", new { id = obj.Role });
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return RedirectToPage("Login", new { IncorrectPasswordFormat = true });
            }
            return RedirectToPage("Login", new { Valid = false });
        }
    }
}
