using Microsoft.AspNetCore.Mvc;
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
        var u = _userService.GetUserById(id);
        return Ok(u);
    }
}