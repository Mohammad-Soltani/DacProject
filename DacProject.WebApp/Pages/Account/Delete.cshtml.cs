using DacProject.WebApp.Helper;
using DacProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DacProject.WebApp.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly IDacProjectAPI _api;
        public UserViewModel user { get; set; }
        public DeleteModel(IDacProjectAPI api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            HttpClient client = _api.Initial();
            client.DefaultRequestHeaders.Clear();
            HttpContext.Request.Cookies.TryGetValue("token", out string token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(string.Format("api/Users/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserViewModel>(json);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(UserViewModel user)
        {
            HttpClient client = _api.Initial();
            client.DefaultRequestHeaders.Clear();
            HttpContext.Request.Cookies.TryGetValue("token", out string token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync(string.Format("api/Users/{0}", user.Id));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("List", new { id = Request.Query["Role"] });
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToPage("Login");
            else return Page();
        }
    }
}
