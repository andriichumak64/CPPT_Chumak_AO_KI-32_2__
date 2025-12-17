using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab4_23.Entity;

namespace lab4_23.Api
{
    public class PostLoginRequest
    {
        private const string ServerAddress = "localhost";
        private const int ServerPort = 5000;

        public class LoginResponseWrapper
        {
            public bool success { get; set; }
            public string message { get; set; }
            public User user { get; set; }
        }

        public static async Task<(bool success, string message, User user)> Execute(
            string login,
            string password)
        {
            try
            {
                var requestModel = new
                {
                    command = "login",  
                    login = login,
                    password = password
                };

                var jsonRequest = JsonSerializer.Serialize(requestModel);
                var bytes = Encoding.UTF8.GetBytes(jsonRequest);

                using var client = new TcpClient(ServerAddress, ServerPort);
                using var stream = client.GetStream();

                await stream.WriteAsync(bytes, 0, bytes.Length);

                using var memoryStream = new MemoryStream();
                var buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, bytesRead);
                }

                var data = memoryStream.ToArray();
                string response = Encoding.UTF8.GetString(data, 0, data.Length);

                var decoded = JsonSerializer.Deserialize<LoginResponseWrapper>(response);
                if (decoded == null)
                    return (false, "Помилка обробки відповіді сервера.", null);

                return (decoded.success, decoded.message, decoded.user);
            }
            catch (JsonException ex)
            {
                return (false, $"Помилка обробки JSON: {ex.Message}", null);
            }
            catch (Exception)
            {
                return (false, "Не вдалося виконати вхід. Перевірте підключення до сервера.", null);
            }
        }
    }
}
