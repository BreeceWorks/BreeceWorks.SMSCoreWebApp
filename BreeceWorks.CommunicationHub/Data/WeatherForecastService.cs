using BreeceWorks.CommunicationHub.Dispatcher.Contracts;
using BreeceWorks.CommunicationHub.Dispatcher.Proxies;

namespace BreeceWorks.CommunicationHub.Data
{
    public class WeatherForecastService
    {
        private IDispatcher _dispatcher { get; set; }

        public WeatherForecastService(IDispatcher dispatcher) 
        {
            _dispatcher = dispatcher;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }

        
    }
}
