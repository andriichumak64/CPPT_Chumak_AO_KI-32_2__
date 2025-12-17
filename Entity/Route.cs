namespace lab4_23.Entity
{
    public class Route
    {
        public Route(int id, string type, string name, double price, string transportName)
        {
            Id = id;
            Type = type;
            Name = name;
            Price = price;
            TransportName = transportName;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string TransportName { get; set; }
    }
}