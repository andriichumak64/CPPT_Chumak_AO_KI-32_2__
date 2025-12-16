using System.Windows;
using lab2_23.Service;
using lab2_23.Pages;

using lab2_23.Api;

namespace lab2_23.Pages;

public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        var result = UserService.Login(LoginBox.Text, PasswordBox.Password);
        
        if (result.success)
        {
            MessageBox.Show(result.message, "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            var menu = new MainWindow();
            menu.Show();
            Close();
        }
        else
        {
            MessageBox.Show(result.message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void GoRegister_Click(object sender, RoutedEventArgs e)
    {
        new Register().Show();
        Close();
    }
}