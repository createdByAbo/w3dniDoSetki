using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using w3dniDoSetki.Entities;
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
}
