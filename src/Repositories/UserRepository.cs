using AuthJwt.Data;

namespace AuthJwt;
public  class UserRepository
{
    private  readonly ApplicationContext _context;

    public  UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public  User Get(string userName, string password)
    {
        return _context.Users.Where(x => x.Name.ToLower() == userName.ToLower() && x.Password == x.Password).FirstOrDefault();
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // public static User Get(string userName, string password)
    // {
    //     //Mocking the user data
    //     var user = new List<User>
    //     {
    //         new User { Id = 1, Name = "admin", Password = "admin", Role = "admin" },
    //         new User { Id = 2, Name = "user", Password = "user", Role = "user" }
    //     };

    //     return user.Where(x => x.Name.ToLower() == userName.ToLower() && x.Password == x.Password).FirstOrDefault();
    // }
    
}
