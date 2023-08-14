namespace AuthJwt;
public static class UserRepository
{
    public static User Get(string userName, string password)
    {
        //Mocking the user data
        var user = new List<User>
        {
            new User { Id = 1, Name = "admin", Password = "admin", Role = "admin" },
            new User { Id = 2, Name = "user", Password = "user", Role = "user" }
        };

        return user.Where(x => x.Name.ToLower() == userName.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
    
}
