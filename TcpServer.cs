using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab3_23.Services;

public class TcpServer
{
    private readonly UserService _userService;
    private readonly RouteService _routeService;
    private readonly ILogger _logger;

    public TcpServer(
        UserService userService,
        RouteService routeService,
        ILogger<TcpServer> logger)
    {
        _userService = userService;
        _routeService = routeService;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        _logger.LogInformation("TCP Server started on port 5000.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        _logger.LogInformation("Client connected.");
        using (client)
        using (var stream = client.GetStream())
        {
            var buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine(request);

            try
            {
                var requestData = JsonSerializer.Deserialize<Dictionary<string, string>>(request);
                if (requestData == null)
                {
                    await Send(stream, new { success = "false", message = "Invalid command." });
                    return;
                }

                var command = requestData["command"].ToLower();
                if (command == "register")
                {
                    var (success, message, user) = await _userService.RegisterUser(
                        requestData["login"],
                        requestData["email"],
                        requestData["password"]
                    );

                    await Send(stream, new { success = success.ToString(), message, user });
                }
                
                else if (command == "login")
                {
                    var (success, message, user) = await _userService.LoginUser(
                        requestData["login"],
                        requestData["password"]
                    );

                    await Send(stream, new { success = success.ToString(), message, user });
                }
                else if (command == "get_routes")
                {
                    var (success, message, routes) = await _routeService.GetRoutes();

                    await Send(stream, new
                    {
                        success = success.ToString(),
                        message,
                        routes
                    });
                }
                else if (command == "post_route")
                {
                    var (success, message, route) = await _routeService.PostRoute(
                        requestData["type"],
                        requestData["name"],
                        double.Parse(requestData["price"]),
                        requestData["transportName"]
                    );

                    await Send(stream, new
                    {
                        success = success.ToString(),
                        message,
                        route
                    });
                }
                else if (command == "get_comments")
                {
                    var (success, message, comments) = await _routeService.GetComments(
                        int.Parse(requestData["routeId"])
                    );

                    await Send(stream, new
                    {
                        success = success.ToString(),
                        message,
                        comments
                    });
                }
                else if (command == "post_comment")
                {
                    var (success, message, comment) = await _routeService.PostComment(
                        requestData["message"],
                        long.Parse(requestData["userId"]),
                        int.Parse(requestData["routeId"])
                    );

                    await Send(stream, new
                    {
                        success = success.ToString(),
                        message,
                        comment
                    });
                }
            }
            catch (JsonException ex)
            {
                var response = new { success = false.ToString(), message = $"Invalid JSON format: {ex.Message}" };
                var jsonResponse = JsonSerializer.Serialize(response);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
            }
            catch (InvalidOperationException ex)
            {
                var response = new
                    { success = false.ToString(), message = $"Error InvalidOperationException: {ex.Message}" };
                var jsonResponse = JsonSerializer.Serialize(response);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
            }
            catch (Exception ex)
            {
                var response = new { success = false.ToString(), message = $"Error: {ex.Message}" };
                var jsonResponse = JsonSerializer.Serialize(response);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
            }
        }
    }
    private async Task Send(NetworkStream stream, object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        await stream.WriteAsync(Encoding.UTF8.GetBytes(json));
    }
}