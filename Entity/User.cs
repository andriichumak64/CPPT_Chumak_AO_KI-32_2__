namespace lab3_23.Entity;

public class User
{
    public User(string login, string email, string password)
    {
        Login = login;
        Email = email;
        Password = password;
    }
    
    public User() { }

    public long Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}