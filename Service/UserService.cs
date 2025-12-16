using System.Text.RegularExpressions;
using lab2_23.Api;
using lab2_23.Entity;

namespace lab2_23.Service;

public static class UserService
{
    public static User CurrentUser { get; private set; }

    public static (bool success, string message) Login(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) ||
            string.IsNullOrWhiteSpace(password))
            return (false, "Поле вводу не може бути пустим");

        var response = PostLoginRequest.Execute();
        if (response.user == null) return (response.success, response.message);

        return (response.success, response.message);
    }

    public static (bool success, string message) Register(string login, string email, string password,
        string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(login) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
            return (false, "Поле вводу не може бути пустим");

        if (login.Length < 6) return (false, "Логін повинен містити мінімум 6 символів");
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return (false, "Некоректна адреса електронної пошти");
        if (password != confirmPassword) return (false, "паролі не співпадають");
        if (password.Length < 4) return (false, "Пароль повинен містити більше 4 символів");
        if (!password.Any(char.IsDigit)) return (false, "Пароль повинен містити хоча б одну цифру");
        if (!password.Any(char.IsLower)) return (false, "Пароль повинен містити хоча б одну малу літеру");
        if (!password.Any(char.IsUpper)) return (false, "Пароль повинен містити хоча б одну велику літеру");

        var response = PostRegisterRequest.Execute();

        if (!response.success) return (response.success, response.message);

        return (response.success, response.message);
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}