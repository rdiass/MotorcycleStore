using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult response)
    {
        if (response != null && response.Errors.Messages.Count != 0)
        {
            foreach (var error in response.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return true;
        }

        return false;        
    }   
}
