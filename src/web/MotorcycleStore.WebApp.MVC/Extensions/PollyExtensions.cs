using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;

namespace MotorcycleStore.WebApp.MVC.Extensions;

public class PollyExtensions
{
    public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetryAsync()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
            [
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            ], (outcome, timespan, retryCount, context) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Trying for the {retryCount} time!");
                Console.ForegroundColor = ConsoleColor.White;
            });
    }
}
