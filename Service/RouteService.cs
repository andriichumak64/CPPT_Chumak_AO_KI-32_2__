using System.Text.RegularExpressions;
using lab4_23.Api;
using lab4_23.Entity;

namespace lab4_23.Service;

public class RouteService
{
    public List<Route>? CurrentRoutes { get; private set; }

    public async Task<(bool success, string message, List<Route> routes)> UseGetRoutes()
    {
        var response = await GetRoutesResponse.Execute();

        if (response.success)
            CurrentRoutes = response.routes;

        return response;
    }
    
    public async Task<(bool success, string message, List<Comment> comments)> UseGetComments(int routeId)
    {
        var response = await GetCommentsResponse.Execute(routeId);
        return response;
    }

    public async Task<(bool success, string message)> UsePostComments(
        string userComment, int routeId, int userId)
    {
        if (string.IsNullOrWhiteSpace(userComment))
            return (false, "Коментар не може бути порожнім.");

        if (userComment.Length < 3)
            return (false, "Коментар занадто короткий. Мінімум 3 символи.");

        if (userComment.Length > 500)
            return (false, "Коментар занадто довгий. Максимум 500 символів.");

        var badSymbols = @"<>|{}[]$#%";
        if (userComment.Any(c => badSymbols.Contains(c)))
            return (false, "Коментар містить недопустимі символи.");

        if (userComment.Distinct().Count() <= 2 && userComment.Length > 10)
            return (false, "Коментар виглядає як спам.");
        
        var (success, message, comment) =
            await PostCommentRequest.Execute(userComment, userId, routeId);

        return (success, message);
    }
}