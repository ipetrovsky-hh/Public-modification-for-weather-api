using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherExample
{
    public record Location(
        string Name, 
        string Region, 
        string Country,
        double Lat, 
        double Lon,
        [property: JsonPropertyName("tz_id")] string TzId,
        [property: JsonPropertyName("localtime")] string LocalTime
    );

    public record Condition(string Text, string Icon, int Code);

    public record AirQuality(
        double Co, 
        double No2, 
        double O3, 
        double So2,
        [property: JsonPropertyName("pm2_5")] double Pm25,
        [property: JsonPropertyName("pm10")] double Pm10,
        [property: JsonPropertyName("us-epa-index")] int UsEpaIndex
    );

    public record CurrentWeather(
        [property: JsonPropertyName("temp_c")] double TempC,
        [property: JsonPropertyName("temp_f")] double TempF,
        [property: JsonPropertyName("feelslike_c")] double FeelsLikeC,
        Condition Condition,
        [property: JsonPropertyName("wind_kph")] double WindKph,
        [property: JsonPropertyName("wind_dir")] string WindDir,
        int Humidity,
        [property: JsonPropertyName("vis_km")] double VisKm,
        double Uv,
        [property: JsonPropertyName("gust_kph")] double GustKph,
        [property: JsonPropertyName("air_quality")] AirQuality? AirQuality
    );

    public record CurrentResponse(Location Location, CurrentWeather Current);

    public record ForecastDayDetail(
        [property: JsonPropertyName("maxtemp_c")] double MaxTempC,
        [property: JsonPropertyName("mintemp_c")] double MinTempC,
        Condition Condition,
        [property: JsonPropertyName("daily_chance_of_rain")] int DailyChanceOfRain
    );

    public record Astro(
        string Sunrise, 
        string Sunset, 
        string Moonrise, 
        string Moonset,
        [property: JsonPropertyName("moon_phase")] string MoonPhase,
        [property: JsonPropertyName("moon_illumination")] int MoonIllumination
    );

    public record ForecastDay(string Date, ForecastDayDetail Day, Astro Astro);

    public record ForecastData([property: JsonPropertyName("forecastday")] List<ForecastDay> ForecastDays);

    public record ForecastResponse(Location Location, CurrentWeather Current, ForecastData Forecast);

    public record AstroData(Astro Astro);

    public record AstronomyResponse(Location Location, AstroData Astronomy);

    public record SearchResult(int Id, string Name, string Region, string Country, double Lat, double Lon);

    public record ApiError(int Code, string Message);

    public record ErrorResponse(ApiError Error);
}
