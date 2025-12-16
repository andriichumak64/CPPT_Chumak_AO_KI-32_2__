namespace lab2_23.Entity
{
    public class Comment
    {
        public Comment(int id, string message, User user, Route route)
        {
            Id = id;
            Message = message;
            User = user;
            Route = route;
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public Route Route { get; set; }
    }
}