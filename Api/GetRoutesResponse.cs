using lab2_23.Entity;

namespace lab2_23.Api
{
    public class GetRoutesResponse
    {
        public static (bool success, string message, List<Route> routes) Execute()
{
    try
    {
        var routes = new List<Route>
        {
            new Route(1, "Маршрутка", "Маршрутка №12: Центр — Вокзал", 12.00, "Mercedes Sprinter"),
            new Route(2, "Маршрутка", "Маршрутка №25: Південний — Ринок", 15.00, "Volkswagen Crafter"),
            new Route(3, "Маршрутка", "Маршрутка №49: Кільцева — Центр", 14.00, "Ford Transit"),
            new Route(4, "Маршрутка", "Маршрутка №7: Лісова — Площа Миру", 10.00, "Iveco Daily"),
            new Route(5, "Маршрутка", "Маршрутка №30: Автостанція — Лікарня", 11.00, "Bogdan A091"),

            new Route(6, "Трамвай", "Трамвай №1: Центр — Парк", 8.00, "Tatra KT4"),
            new Route(7, "Трамвай", "Трамвай №3: Вокзал — Ринок", 9.00, "Tatra T3"),
            new Route(8, "Трамвай", "Трамвай №8: Міст — Площа Свободи", 10.00, "Electron T5"),
            new Route(9, "Трамвай", "Трамвай №11: Оперний театр — Лісопарк", 7.50, "Pesa 120Na"),
            new Route(10, "Трамвай", "Трамвай №5: Річковий порт — Центр", 9.00, "Tatra KT4SU"),

            new Route(11, "Потяг", "Потяг: Київ — Львів", 320.00, "InterCity+ Hyundai Rotem HRCS2"),
            new Route(12, "Потяг", "Потяг: Харків — Одеса", 450.00, "InterCity Tarpan"),
            new Route(13, "Потяг", "Потяг: Дніпро — Івано-Франківськ", 380.00, "Ukrzaliznytsia Night Train"),
            new Route(14, "Потяг", "Потяг: Запоріжжя — Ужгород", 520.00, "Passenger Express 81/82"),
            new Route(15, "Потяг", "Потяг: Чернігів — Луцьк", 290.00, "Regional Train DR1A"),

            new Route(16, "Таксі", "Таксі: Центр — Аеропорт", 250.00, "Toyota Prius"),
            new Route(17, "Таксі", "Таксі: Вокзал — Мікрорайон Сонячний", 120.00, "Renault Logan"),
            new Route(18, "Таксі", "Таксі: ТРЦ — Старе місто", 95.00, "Skoda Octavia"),
            new Route(19, "Таксі", "Таксі: Арена — Парк Шевченка", 110.00, "Hyundai Elantra"),
            new Route(20, "Таксі", "Таксі: Площа Миру — Університет", 85.00, "Volkswagen Passat")
        };

        return (true, "Маршрути успішно завантажено", routes);
    }
    catch
    {
        return (false, "Не вдалося завантажити маршрути", null);
    }
}

    }
}
