using DevOne.Security.Cryptography.BCrypt;
using w3dniDoSetki.Exceptions;
using w3dniDoSetki.Models.DTOs;

namespace w3dniDoSetki.Services;

using Entities;

public interface IUserService
{
    bool Login(LoginDto userData);
}

public class UserService : IUserService
{
    private readonly W3dnidosetkiContext _context;

    public UserService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public bool Login(LoginDto userData)
    {
        var usr = _context.Users
            .Where(u => u.Email == userData.Email)
            .Where(u => u.IsVerified == true)
            .First();
        if (BCryptHelper.CheckPassword(userData.Password, usr.PasswordHash))
        {
            return true;
        }
        else
        {
            throw new NotFoundException();
        }
    }
}