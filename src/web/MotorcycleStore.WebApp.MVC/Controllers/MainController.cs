using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult response)
    {
        if(response != null && response.Errors.Messages.Count != 0) return true;

        return false;        
    }   
}
