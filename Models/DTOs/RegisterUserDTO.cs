using DevOne.Security.Cryptography.BCrypt;
using w3dniDoSetki.Exceptions;

namespace w3dniDoSetki.Models.DTOs;

public class RegisterUserDTO
{
    public RegisterUserDTO(string? name, string? lastName, string? email, string? password, string? phoneNumber)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = Int32.Parse(phoneNumber) == null ? throw new NotFoundException() : Int32.Parse(phoneNumber);;
        _password = password;
        PasswordHash = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt(10));
    }

    public string? Name { get; }
    public string? LastName { get; }
    public string? Email { get; }
    private string? _password; 
    public string? PasswordHash { get; }
    public int? PhoneNumber { get; }
}