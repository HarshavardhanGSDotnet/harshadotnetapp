using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlamesApp.Models;

namespace FlamesApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
        public IActionResult Result(FlamesModel model)
        {
            if (ModelState.IsValid)
            {
                var score = CalculateFlamesScore(model.YourName, model.SoulmateName);
                model.Result = GetFlamesResult(score);
                return View(model);
            }
            return View("Index");
        }

        public int CalculateFlamesScore(string yourName, string soulmateName)
        {
            yourName = yourName.ToLower();
            soulmateName = soulmateName.ToLower();

            List<char> yourNameList = new List<char>(yourName);
            List<char> soulmateNameList = new List<char>(soulmateName);

            foreach (char c in yourName)
            {
                if (soulmateNameList.Contains(c))
                {
                    yourNameList.Remove(c);
                    soulmateNameList.Remove(c);
                }
            }

            int score = yourNameList.Count + soulmateNameList.Count;
            return score;
        }

        public string GetFlamesResult(int score)
        {
            string flames = "FLAMES";

            while (flames.Length > 1)
            {
                int index = (score % flames.Length) - 1;
                if (index >= 0)
                {
                    flames = flames.Remove(index, 1);
                }
                else
                {
                    flames = flames.Remove(flames.Length - 1, 1);
                }
                flames = flames.Substring(index + 1) + flames.Substring(0, index + 1);
            }
            return flames;
        }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
