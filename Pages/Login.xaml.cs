using System.Windows;
using lab4_23.Service;

namespace lab4_23.Pages;

public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        var result = await UserService.Login(LoginBox.Text, PasswordBox.Password);
        
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