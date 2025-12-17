using System.Text.RegularExpressions;
using lab4_23.Api;
using lab4_23.Entity;

namespace lab4_23.Service;

public static class UserService
{
    public static User? CurrentUser { get; private set; }

    public static async Task<(bool success, string message, User? user)> Login(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) ||
            string.IsNullOrWhiteSpace(password))
            return (false, "Поле вводу не може бути пустим", null);

        var (success, message, user) = await PostLoginRequest.Execute(login, password);

        if (!success)
            return (success, message, user);

        CurrentUser = user;

        return (success, message, user);
    }

    public static async Task<(bool success, string message, User? user)> Register(
        string login, string email, string password, string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(login) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
            return (false, "Поле вводу не може бути пустим", null);

        if (login.Length < 6) return (false, "Логін повинен містити мінімум 6 символів", null);
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return (false, "Некоректна адреса електронної пошти", null);
        if (password != confirmPassword) return (false, "Паролі не співпадають", null);
        if (password.Length < 4) return (false, "Пароль повинен містити більше 4 символів", null);
        if (!password.Any(char.IsDigit)) return (false, "Пароль повинен містити хоча б одну цифру", null);
        if (!password.Any(char.IsLower)) return (false, "Пароль повинен містити хоча б одну малу літеру", null);
        if (!password.Any(char.IsUpper)) return (false, "Пароль повинен містити хоча б одну велику літеру", null);

        var (success, message, user) = await PostRegisterRequest.Execute(login, email, password);

        if (!success)
            return (success, message, user);

        CurrentUser = user;

        return (success, message, user);
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}