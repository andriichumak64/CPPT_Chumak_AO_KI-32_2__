using lab2_23.Entity;

namespace lab2_23.Api
{
    public class PostCommentRequest
    {
        public static (bool success, string message, Comment comment) Execute(string message)
        {
            try
            {
                var user = new User(1, "login", "email@gmail.com", "password");
                var route = new Route(1, "Маршрутка", "Маршрутка №12: Центр — Вокзал", 12.00, "Мерседес");
                var newComment = new Comment(1, message, user, route);

                return (
                    true,
                    "Коментар успішно додано",
                    newComment
                );
            }
            catch
            {
                return (
                    false,
                    "Не вдалося додати коментар",
                    null
                );
            }
        }
    }
}