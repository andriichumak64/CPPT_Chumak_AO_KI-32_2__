using lab3_23.Entity;
using Microsoft.EntityFrameworkCore;

namespace lab3_23.Services;

public class RouteService
{
    private readonly AppDbContext _context;

    public RouteService(AppDbContext context)
    {
        _context = context;
    }

    // -------------------------------
    // GET ROUTES
    // -------------------------------
    public async Task<(bool Success, string Message, List<Route>? Routes)> GetRoutes()
    {
        try
        {
            var routes = await _context.Routes.ToListAsync();

            if (routes.Count == 0)
                return (false, "Немає доступних маршрутів.", null);

            return (true, "Список маршрутів отримано успішно!", routes);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка: {ex.Message}", null);
        }
    }

    // -------------------------------
    // CREATE ROUTE
    // -------------------------------
    public async Task<(bool Success, string Message, Route? Route)> PostRoute(
        string type, string name, double price, string transportName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(type))
                return (false, "Тип маршруту не може бути порожнім.", null);

            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return (false, "Назва маршруту має містити мінімум 2 символи.", null);

            if (price <= 0)
                return (false, "Ціна має бути більшою за 0.", null);

            if (string.IsNullOrWhiteSpace(transportName) || transportName.Length < 2)
                return (false, "Назва транспорту має містити мінімум 2 символи.", null);

            var route = new Route
            {
                Type = type,
                Name = name,
                Price = price,
                TransportName = transportName
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return (true, "Маршрут створено!", route);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка: {ex.Message}", null);
        }
    }


    // -------------------------------
    // CREATE COMMENT
    // -------------------------------
    public async Task<(bool Success, string Message, Comment? Comment)> PostComment(
        string message, long userId, int routeId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message) || message.Length < 2)
                return (false, "Коментар має містити мінімум 2 символи.", null);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return (false, "Користувача не знайдено.", null);

            var route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == routeId);
            if (route == null)
                return (false, "Маршрут не знайдено.", null);

            var comment = new Comment
            {
                Message = message,
                User = user,
                Route = route
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return (true, "Коментар створено!", comment);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка: {ex.Message}", null);
        }
    }


    // -------------------------------
    // GET COMMENTS
    // -------------------------------
    public async Task<(bool Success, string Message, List<Comment>? Comments)> GetComments(int routeId)
    {
        try
        {
            var routeExists = await _context.Routes.AnyAsync(r => r.Id == routeId);
            if (!routeExists)
                return (false, "Маршрут не існує.", null);

            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.Route.Id == routeId)
                .ToListAsync();

            if (comments.Count == 0)
                return (false, "Коментарів поки немає.", null);

            return (true, "Коментарі отримано!", comments);
        }
        catch (Exception ex)
        {
            return (false, $"Помилка: {ex.Message}", null);
        }
    }
}
