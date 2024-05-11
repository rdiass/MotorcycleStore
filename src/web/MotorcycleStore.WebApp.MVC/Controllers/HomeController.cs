using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var model = new ErrorViewModel
            {
                ErrorCode = id,
                Title = "Error",
                Message = "An error has occurred! Please try again later or contact our support."
            };

            if (id == 500)
            {
                model.Title = "Oops!";
                model.Message = "Something went wrong! Please try again later.";
            }
            else if (id == 404)
            {
                model.Title = "Not found";
                model.Message = "The page you are looking for does not exist!";
            }
            else if (id == 403)
            {
                model.Title = "Access Denied";
                model.Message = "You do not have permission to do this.";
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", model);
        }

        [Route("system-unavailable")]
        public IActionResult ServiceUnavailable()
        {
            var modelErro = new ErrorViewModel
            {
                Message = "The service is temporary unavailable.",
                Title = "Service unavailable.",
                ErrorCode = 500
            };

            return View("Error", modelErro);
        }
    }
}
