using lab2_23.Service;
using lab2_23.Entity;
using System.Windows;

namespace lab2_23.Pages
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

        private void LoadComments()
        {
            var result = _service.UseGetComments();

            if (!result.success)
            {
                MessageBox.Show(result.message);
                return;
            }

            var filtered = result.chairs
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