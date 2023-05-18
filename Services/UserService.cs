namespace w3dniDoSetki.Services;

using Entities;

public interface IUserService
{
    User GetUserById(int id);
}

public class UserService : IUserService
{
    private readonly W3dnidosetkiContext _context;

    public UserService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
    {
        try
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .First();
            return user;
        }
        catch (InvalidOperationException)
        {
            return new User();
        }
    }
}