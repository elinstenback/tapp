using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using labb2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace labb2.Controllers
{
    public class HomeController : Controller
    {
        // Index
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Stuck at home?";
            // Deserialiserar json-fil för att kunna skriva ut  
            var JsonStr = System.IO.File.ReadAllText("post.json");
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<PostModel>>(JsonStr);


            // Hämtar sessionsvariabel och läser ut namn på startsida när person lagt till en post
            string theName = HttpContext.Session.GetString("nameofperson");
            ViewBag.name = theName;
            string theMessage = HttpContext.Session.GetString("messagetoperson");
            ViewBag.personalMessage = theMessage;

            return View(JsonObj);
        }

        // About
        public IActionResult About()
        {
            ViewData["PageTitle"] = "About the app";
            return View();
        }

        // Posts
        public IActionResult Posts()
        {
            ViewData["PageTitle"] = "Add tip";
            return View();
        }

        // PostModel
        [HttpPost]
        public IActionResult Posts(PostModel model)
        {
            if(ModelState.IsValid)
            {
                // Läs in befintlig
                var JsonStr = System.IO.File.ReadAllText("post.json");
                var JsonObj = JsonConvert.DeserializeObject<List<PostModel>>(JsonStr);
                JsonObj.Add(model);

                // Lagrar det inmatade namnet i sessionsvariabel
                ViewBag.name = model.Name;
                string yourName = ViewBag.name;
                HttpContext.Session.SetString("nameofperson", yourName);
                string yourMessage = ", your tip is now in the list.";
                HttpContext.Session.SetString("messagetoperson", yourMessage);

                // Konvertera och lagra
                System.IO.File.WriteAllText("post.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));

                // Rensa formulär
                ModelState.Clear();

                // ViewBag
                ViewBag.postMessage = ", that's a great tip!";
                ViewBag.thanks = "Thanks";
                return View();
            } 
            return View();
        }
    }
}
