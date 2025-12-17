using System.Windows;
using lab4_23.Entity;
using lab4_23.Service;

namespace lab4_23.Pages
{
    public partial class CommentsList : Window
    {
        private readonly RouteService _service = new();
        private readonly Route _route;

        public CommentsList(Route route)
        {
            InitializeComponent();
            _route = route;

            Title = $"Коментарі: {_route.Name}";
            TitleText.Text = $"Коментарі до маршруту: {_route.Name}";

            LoadComments();
        }

        private async void LoadComments()
        {
            var result = await _service.UseGetComments(_route.Id);

            if (!result.success)
            {
                MessageBox.Show(result.message);
                return;
            }

            var filtered = result.comments
                .Where(c => c.Route.Id == _route.Id)
                .ToList();

            CommentsListBox.ItemsSource = filtered;

            // --- Вивід "коментарів немає" ---
            EmptyText.Visibility = filtered.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}