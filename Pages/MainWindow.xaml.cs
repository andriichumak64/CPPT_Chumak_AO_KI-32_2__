using System.Windows;
using System.Windows.Controls;
using lab4_23.Entity;
using lab4_23.Pages;
using lab4_23.Service;

namespace lab4_23
{
    public partial class MainWindow : Window
    {
        private readonly RouteService _routeService = new();
        private List<Route> _allRoutes = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadRoutes();
        }

        private async void LoadRoutes()
        {
            var result = await _routeService.UseGetRoutes();
            if (!result.success)
            {
                MessageBox.Show(result.message);
                return;
            }

            _allRoutes = result.routes;
            RoutesList.ItemsSource = _allRoutes;
        }
        
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            string routeQuery = SearchRouteBox.Text.ToLower().Trim();
            string transportQuery = SearchTransportBox.Text.ToLower().Trim();

            var filtered = _allRoutes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(routeQuery))
                filtered = filtered.Where(r => r.Name.ToLower().Contains(routeQuery));

            if (!string.IsNullOrWhiteSpace(transportQuery))
                filtered = filtered.Where(r => r.TransportName.ToLower().Contains(transportQuery));

            RoutesList.ItemsSource = filtered.ToList();
        }

        private void OpenComments_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Route route)
                new CommentsList(route).ShowDialog();
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Route route)
                new CommentForm(route).ShowDialog();
        }
    }
}