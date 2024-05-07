using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MotorcycleStore.Identity.API.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Errors = [];

    protected ActionResult CustomResponse(object? result = null)
    {
        if(ValidOperation())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        
        foreach (var erro in erros)
        {
            AddError(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool ValidOperation()
    {
        return Errors.Count == 0;
    }

    protected void AddError(string error)
    {
        Errors.Add(error);
    }

    protected void ClearErrors()
    {
        Errors.Clear();
    }
}
