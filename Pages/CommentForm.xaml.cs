using System.Windows;
using lab4_23.Entity;
using lab4_23.Service;

namespace lab4_23.Pages
{
    public partial class CommentForm : Window
    {
        private readonly RouteService _service = new();
        private readonly Route _route;

        public CommentForm(Route route)
        {
            InitializeComponent();
            _route = route;

            Title = $"Новий коментар: {_route.Name}";
            TitleText.Text = $"Новий коментар до маршруту: {_route.Name}";
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.UsePostComments(CommentText.Text, _route.Id, UserService.CurrentUser!.Id);

            MessageBox.Show(result.message);

            if (result.success)
                Close();
        }
    }
}