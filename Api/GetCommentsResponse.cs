using lab2_23.Entity;

namespace lab2_23.Api
{
    public class GetCommentsResponse
    {
        public static (bool success, string message, List<Comment> comments) Execute()
        {
            try
            {
                var routesResponse = GetRoutesResponse.Execute();

                if (!routesResponse.success)
                    return (false, "Не вдалося завантажити коментарі, бо маршрути недоступні", null);

                var routes = routesResponse.routes;

                var comments = new List<Comment>
                {
                    new Comment(1,
                        "Трамвай №3 знову стоїть — якась машина запаркувалась просто на рейках.",
                        new User(1, "UrbanWatcher", "uw@gmail.com", "pass1"),
                        routes[6]),

                    new Comment(2,
                        "Маршрутка №25 щойно зробила 'швидкісний маневр'. Пасажири ще досі в шоці.",
                        new User(2, "FastLife", "fl@gmail.com", "pass2"),
                        routes[1]),

                    new Comment(3,
                        "Потяг Київ—Львів запізнюється на 12 хвилин. Нормально, навіть мало.",
                        new User(3, "TrainPro", "tp@gmail.com", "pass3"),
                        routes[10]),

                    new Comment(4,
                        "Таксі до аеропорту їде без кондиціонера. Почуваюсь як у сауні.",
                        new User(4, "HotPassenger", "hp@gmail.com", "pass4"),
                        routes[15]),

                    new Comment(5,
                        "Трамвай №1 зупинився на зупинці, бо водій купив каву. Затримка +4 хв.",
                        new User(5, "CoffeeHunter", "ch@gmail.com", "pass5"),
                        routes[5]),

                    new Comment(6,
                        "Маршрутка №49 вмістила більше людей, ніж фізично можливо. Ньютон в шоці.",
                        new User(6, "PhysicsGuy", "pg@gmail.com", "pass6"),
                        routes[2]),

                    new Comment(7,
                        "Потяг Харків—Одеса рухається без затримок. Підозріло…",
                        new User(7, "Suspicious", "sus@gmail.com", "pass7"),
                        routes[11]),

                    new Comment(8,
                        "Таксі до парку приїхало за 3 хвилини. Або рекорд, або телепортація.",
                        new User(8, "Speedy", "spd@gmail.com", "pass8"),
                        routes[17]),

                    new Comment(9,
                        "Трамвай №8 настільки повільний, що пішохід його обігнав.",
                        new User(9, "SlowDown", "sd@gmail.com", "pass9"),
                        routes[7]),

                    new Comment(10,
                        "Потяг Дніпро — Івано-Франківськ зупинився посеред поля. Красиво, але холодно.",
                        new User(10, "Frozen", "fz@gmail.com", "pass10"),
                        routes[12])
                };

                return (true, "Коментарі успішно завантажено", comments);
            }
            catch
            {
                return (false, "Не вдалося завантажити коментарі", null);
            }
        }
    }
}
