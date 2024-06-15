using Microsoft.AspNetCore.Mvc;
using FlamesApp.Models;

namespace FlamesApp.Controllers
{
    public class FlamesController : Controller
    {
        [HttpGet]
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
            string new_string ="";

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
                
                switch (flames)
                {
                    case "F":
                    new_string = "Congratulations you are friends";
                    break;
                    case "L":
                    new_string = "You two are supposed to be lovers";
                    break;
                    case "A":
                    new_string = "You two are so Affectionate on Each other";
                    break;
                    case "M":
                    new_string = "Are you two looking for Marriage";
                    break;
                    case "E":
                    new_string = "You are Enemies maybe sharing a hatered";
                    break;
                    case "S":
                    new_string = "You both have a chances to be  sibling";
                    break;
                    default:
                    new_string ="Not got a proper calculation for you sorry";
                    break;

                }
            }
            return new_string;
        }
    }
}
