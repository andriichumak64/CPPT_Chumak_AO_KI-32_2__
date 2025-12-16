using lab2_23.Entity;

namespace lab2_23.Api
{
    public class PostRegisterRequest
    {
        public static (bool success, string message, User user) Execute()
        {
            try
            {
                return (
                    true,
                    "Реєстрація пройшла успішно! Ласкаво просимо до системи моніторингу транспорту.",
                    new User(1454366, "1", "example@gmail.com", "1")
                );
            }
            catch
            {
                return (
                    false,
                    "Сталася помилка під час реєстрації. Спробуйте ще раз.",
                    null
                );
            }
        }
    }
}