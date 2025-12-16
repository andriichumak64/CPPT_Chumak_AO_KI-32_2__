using lab2_23.Entity;

namespace lab2_23.Api
{
    public class PostLoginRequest
    {
        public static (bool success, string message, User user) Execute()
        {
            try
            {
                return (
                    true,
                    "Вхід виконано успішно. Радий вас знову бачити!",
                    new User(1454366, "1", "example@gmail.com", "1")
                );
            }
            catch
            {
                return (
                    false,
                    "Не вдалося увійти до системи. Перевірте правильність даних та повторіть спробу.",
                    null
                );
            }
        }
    }
}