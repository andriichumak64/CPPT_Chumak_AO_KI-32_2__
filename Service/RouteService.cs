using System.Text.RegularExpressions;
using lab2_23.Api;
using lab2_23.Entity;

namespace lab2_23.Service;

public class RouteService
{
    public List<Route> CurrentRoutes { get; private set; }

    public (bool success, string message, List<Route> chairs) UseGetRoutes()
    {
        var response = GetRoutesResponse.Execute();
        return response;
    }
    
    public (bool success, string message, List<Comment> chairs) UseGetComments()
    {
        var response = GetCommentsResponse.Execute();
        return response;
    }

    public (bool success, string message) UsePostComments(string comment)
    {
        if (comment == null)
            return (false, "Коментар не може бути відсутнім.");
        
        if (string.IsNullOrWhiteSpace(comment))
            return (false, "Коментар не може бути порожнім.");
        
        if (comment.Length < 3)
            return (false, "Коментар занадто короткий. Мінімум 3 символи.");
        
        if (comment.Length > 500)
            return (false, "Коментар занадто довгий. Максимум 500 символів.");
        
        string badSymbols = @"<>|{}[]$#%";
        if (comment.Any(c => badSymbols.Contains(c)))
            return (false, "Коментар містить недопустимі символи.");
        
        if (comment.Distinct().Count() <= 2 && comment.Length > 10)
            return (false, "Коментар виглядає як спам.");
        
        var response = PostCommentRequest.Execute(comment);

        return (response.success, response.message);
    }
}