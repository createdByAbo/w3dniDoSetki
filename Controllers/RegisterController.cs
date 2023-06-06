using Microsoft.AspNetCore.Mvc;
using w3dniDoSetki.Entities;
using w3dniDoSetki.Models.DTOs;

namespace w3dniDoSetki.Controllers;

public class RegisterController : Controller
{
    private readonly W3dnidosetkiContext _context;

    public RegisterController(W3dnidosetkiContext context)
    {
        _context = context;
    }
    // GET
    public ActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult Index( IFormCollection collection)
    {
        RegisterUserDTO userDto = new RegisterUserDTO(collection["firstname"], collection["lastname"], collection["email"],
            collection["password"], collection["phonenumber"]);
        User user = new User();
        user.Email = userDto.Email;
        user.Name = userDto.Name;
        user.LastName = userDto.LastName;
        user.PasswordHash = userDto.PasswordHash;
        user.PhoneNumber = userDto.PhoneNumber.ToString();
        _context.Users.Add(user);
        _context.SaveChanges();
        return View();
    }
}