using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevOne.Security.Cryptography.BCrypt;
using dotenv.net;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using w3dniDoSetki.Exceptions;
using w3dniDoSetki.JWT;
using w3dniDoSetki.Models.DTOs;

namespace w3dniDoSetki.Services;


public interface IUserService
{
    string Login(LoginDto userData);
    string GetUserRoleByEmail(string mail);
}

public class UserService : IUserService
{
    private readonly W3dnidosetkiContext _context;
    private readonly AuthSettings _authenticationSettings;

    public UserService(W3dnidosetkiContext context, AuthSettings authSettings)
    {
        _context = context;
        _authenticationSettings = authSettings;
    }
    
    public List<string> CreateToken(JwtDataDTO JwtDataDTO)
    {
        List<string> returnData = new List<string>();
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, JwtDataDTO.Id.ToString()),
            new Claim(ClaimTypes.Email, JwtDataDTO.Email),
            new Claim(ClaimTypes.Role, GetUserRoleByEmail(JwtDataDTO.Email)),
        };
        DotEnv.Load();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SecKey")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);
        Console.WriteLine($"iss: {_authenticationSettings.JwtIssuer}");

        var token = new JwtSecurityToken("https://ex.com", "https://ex.com", claims, expires: DateTime.Now + TimeSpan.FromHours(1), signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        returnData.Add(tokenHandler.WriteToken(token));
        returnData.Add(expires.ToShortTimeString());
        return returnData;
    }

    public string Login(LoginDto userData)
    {
        var usr = _context.Users
            .Where(u => u.Email == userData.Email)
            .Where(u => u.IsVerified == true)
            .First();
        if (BCryptHelper.CheckPassword(userData.Password, usr.PasswordHash))
        {
            return CreateToken(new JwtDataDTO(userData.Password, userData.Email, usr.Id))[0];
        }
        else
        {
            throw new NotFoundException();
        }
    }

    public string GetUserRoleByEmail(string mail)
    {
        var isAdmin = _context.Users
            .Where(u => u.Email == mail)
            .Select(u => u.IsAdmin).Single();
        return (isAdmin == true) ? "Admin" : "User";
        throw new NotFoundException();
    }
}