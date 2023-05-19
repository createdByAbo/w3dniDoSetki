using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using w3dniDoSetki.Exceptions;
using w3dniDoSetki.Models;
using w3dniDoSetki.Services;


namespace w3dniDoSetki.Controllers.Api;

public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public ActionResult Id(int id) //get user by id
    {
        try
        {
            var u = _userService.GetUserById(id);
            return Ok(u);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}