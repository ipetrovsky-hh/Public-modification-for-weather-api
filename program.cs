// WeatherAPI.com — C# (.NET 6+) Example
// Docs: https://www.weatherapi.com/docs/
// Sign up free: https://www.weatherapi.com/signup.aspx
//
// Requirements: .NET 6 or later
// Run: dotnet run

using System.ComponentModel.Design;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using WeatherExample;

const string ApiKey = "your_api_key";
const string BaseUrl = "https://api.weatherapi.com/v1";

var client = new WeatherClient(ApiKey);

// Current weather
Console.WriteLine("=== Current Weather: Moscow ===");
var current = await client.GetCurrentWeatherAsync("Moscow");
var loc = current.Location;
var w = current.Current;

Console.WriteLine($"Страна Город Время  {loc.Name}, {loc.Country} ({loc.LocalTime})");
Console.WriteLine($"Температура 🌡️  {w.TempC}°C (feels like {w.FeelsLikeC}°C)");
Console.WriteLine($"Солнечность 🌤️  {w.Condition.Text}");
Console.WriteLine($"Ветер км/ч {w.WindKph} km/h {w.WindDir}");
Console.WriteLine($"Влажность 💧 Humidity: {w.Humidity}%");
Console.WriteLine($"Индекс ☀️  UV Index: {w.Uv}");

if (w.AirQuality != null)
    Console.WriteLine($"🌬️  US EPA Index: {w.AirQuality.UsEpaIndex} / 6");

// Forecast
Console.WriteLine("\n=== 3-Day Forecast: Moscow ===");
var forecast = await client.GetForecastAsync("Moscow", 3);

foreach (var day in forecast.Forecast.ForecastDays)
{
    Console.WriteLine(
        $"  {day.Date} | {day.Day.Condition.Text} | " +
        $"Max {day.Day.MaxTempC}°C | Min {day.Day.MinTempC}°C | " +
        $"Rain {day.Day.DailyChanceOfRain}%"
    );
}

Console.WriteLine("\n=== Hours Forecast: Moscow ===  For Each Our");
var forecastHour = await client.GetForecastAsync("Moscow", 1);
foreach (var hour in forecastHour.Forecast.ForecastDays)
{
    for (var i = 0; i < hour.Hour.Count(); i++)
    {
        string dayYN = "";

        if (hour.Hour[i].Is_day.ToString() == "1")
        {
            dayYN = "День";
        }
        else
        {
            dayYN = "Ночь";
        }
        ;

        Console.WriteLine(
            $"Дата: {hour.Date} номер часа: {i}  |" +
            $"Время суток: {dayYN}      |" +
            $"Condition  {hour.Hour[i].Condition.Text}   |" +
            $"Температура C {hour.Hour[i].TempC}°C       |" +
            $"Вероятность дождя {hour.Hour[i].Chance_of_rain}% |" +
            $"Вероятность снега {hour.Hour[i].Chance_of_snow}% "
        );
    }
}
