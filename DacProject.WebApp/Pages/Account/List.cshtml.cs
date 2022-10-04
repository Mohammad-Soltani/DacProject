using DacProject.WebApp.Helper;
using DacProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DacProject.WebApp.Pages.Account
{
    public class ListModel : PageModel
    {
        private readonly IDacProjectAPI _api;
        public List<UserViewModel> Users { get; set; }
        public ListModel(IDacProjectAPI api)
        {
            _api = api;
        }
        public async Task<IActionResult> OnGet()
        {
            HttpClient client = _api.Initial();
            client.DefaultRequestHeaders.Clear();
            HttpContext.Request.Cookies.TryGetValue("token", out string token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("api/Users");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<UserViewModel>>(res);
                return Page();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToPage("Login");
            else return Page();
        }
    }
}
