using lab2_23.Service;
using lab2_23.Entity;
using System.Windows;

namespace lab2_23.Pages
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

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var result = _service.UsePostComments(CommentText.Text);

            MessageBox.Show(result.message);

            if (result.success)
                Close();
        }
    }
}