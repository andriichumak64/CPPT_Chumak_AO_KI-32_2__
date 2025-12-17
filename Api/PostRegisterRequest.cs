using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab4_23.Entity;

namespace lab4_23.Api
{
    public class PostRegisterRequest
    {
        private const string ServerAddress = "localhost";
        private const int ServerPort = 5000;

        public class RegisterResponseWrapper
        {
            public bool success { get; set; }
            public string message { get; set; }
            public User user { get; set; }
        }

        public static async Task<(bool success, string message, User user)> Execute(
            string login,
            string email,
            string password)
        {
            try
            {
                var requestModel = new
                {
                    command = "register",
                    login = login,
                    email = email,
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

                var decoded = JsonSerializer.Deserialize<RegisterResponseWrapper>(response);
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
                return (false, "Не вдалося виконати реєстрацію. Перевірте підключення до сервера.", null);
            }
        }
    }
}
