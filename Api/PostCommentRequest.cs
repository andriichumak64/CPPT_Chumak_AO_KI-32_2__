using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab4_23.Entity;

namespace lab4_23.Api
{
    public class PostCommentRequest
    {
        private const string ServerAddress = "localhost";
        private const int ServerPort = 5000;

        public class PostCommentResponseWrapper
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Comment comment { get; set; }
        }
        public static async Task<(bool success, string message, Comment comment)> Execute(
            string text, int userId, int routeId)
        {
            try
            {
                var requestModel = new
                {
                    command = "post_comment",
                    message = text,
                    userId = userId.ToString(),
                    routeId = routeId.ToString()
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

                var decoded = JsonSerializer.Deserialize<PostCommentResponseWrapper>(response);
                if (decoded == null)
                    return (false, "Помилка читання відповіді сервера", null);

                return (
                    decoded.success,
                    decoded.message,
                    decoded.comment
                );
            }
            catch
            {
                return (false, "Не вдалося створити коментар", null);
            }
        }
    }
}
