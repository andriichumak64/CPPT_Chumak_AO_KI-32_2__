using lab3_23.Entity;
using Microsoft.EntityFrameworkCore;

namespace lab3_23.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, User? User)> RegisterUser(string login, string email, string password)
    {
        try
        {
            const int minUsernameLength = 3;
            const int maxUsernameLength = 20;

            if (string.IsNullOrWhiteSpace(login) || login.Length < minUsernameLength || login.Length > maxUsernameLength)
            {
                return (false, $"Логін має бути від {minUsernameLength} до {maxUsernameLength} символів.", null);
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                return (false, "Некоректна електронна адреса.", null);
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
            {
                return (false, "Пароль має бути не менше 4 символів.", null);
            }

            if (await _context.Users.AnyAsync(u => u.Login == login))
            {
                return (false, "Користувач із таким логіном вже існує.", null);
            }

            var user = new User
            {
                Login = login,
                Email = email,
                Password = password
            };

            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return (true, "Реєстрація успішна!", user);
            }

            return (false, "Не вдалося зберегти користувача.", null);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка під час реєстрації: {ex.Message}", null);
        }
    }
    
    public async Task<(bool Success, string Message, User? User)> LoginUser(string login, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Логін і пароль не можуть бути порожніми.", null);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
            {
                return (false, "Користувача з таким логіном не знайдено.", null);
            }

            if (user.Password != password)
            {
                return (false, "Невірний пароль.", null);
            }

            return (true, "Вхід виконано успішно!", user);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка під час входу: {ex.Message}", null);
        }
    }
}