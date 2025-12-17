using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab4_23.Entity;

namespace lab4_23.Api
{
    public class GetRoutesResponse
    {
        private const string ServerAddress = "localhost";
        private const int ServerPort = 5000;

        public class GetRoutesResponseWrapper
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Route> routes { get; set; }
        }
        public static async Task<(bool success, string message, List<Route> routes)> Execute()
        {
            try
            {
                var requestModel = new
                {
                    command = "get_routes"
                };

                var jsonRequest = JsonSerializer.Serialize(requestModel);
                var bytes = Encoding.UTF8.GetBytes(jsonRequest);

                using var client = new TcpClient(ServerAddress, ServerPort);
                using var stream = client.GetStream();

                await stream.WriteAsync(bytes, 0, bytes.Length);

                using var memory = new MemoryStream();
                var buffer = new byte[1024];
                int read;

                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    await memory.WriteAsync(buffer, 0, read);

                string response = Encoding.UTF8.GetString(memory.ToArray());

                var decoded = JsonSerializer.Deserialize<GetRoutesResponseWrapper>(response);
                if (decoded == null)
                    return (false, "Помилка читання відповіді сервера", null);

                return (
                    decoded.success,
                    decoded.message,
                    decoded.routes
                );
            }
            catch
            {
                return (false, "Не вдалося отримати маршрути", null);
            }
        }
    }
}