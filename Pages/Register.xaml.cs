using System.Windows;
using lab2_23.Service;


namespace lab2_23.Pages;

public partial class Register : Window
{
    public Register()
    {
        InitializeComponent();
    }

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        var result = UserService.Register(LoginBox.Text, EmailBox.Text,
            PasswordBox.Password, ConfirmPasswordBox.Password);

        if (result.success)
        {
            MessageBox.Show(result.message, "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            new Login().Show();
            Close();
        }
        else
        {
            MessageBox.Show(result.message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GoLogin_Click(object sender, RoutedEventArgs e)
    {
        new Login().Show();
        Close();
    }
}
