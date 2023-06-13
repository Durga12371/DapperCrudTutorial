using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Globalization;
using Newtonsoft.Json;

namespace MVC_Project.Controllers
{
    public class SuperHeroController : Controller
    {
        Uri BaseAddress = new Uri("http://localhost:44350/api");
        private readonly HttpClient _client;
        
        public SuperHeroController()
        {
             _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<SuperHeroViewModel> demo = new List<SuperHeroViewModel>();
            HttpResponseMessage respo = _client.GetAsync("http://localhost:44350/api/SuperHero").Result;

            if (respo.IsSuccessStatusCode)
            {
                String data = respo.Content.ReadAsStringAsync().Result;
                demo = JsonConvert.DeserializeObject<List<SuperHeroViewModel>>(data);


            }
            return View(demo);
        }








    }
}