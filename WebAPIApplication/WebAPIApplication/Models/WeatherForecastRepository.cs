using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    public class WeatherForecastRepository : IDisposable, IRepository
    {

        WeatherForecastContext context = new WeatherForecastContext();

        public void Save(WeatherForecast w)
        {
            //Условия вставки
            var weather = context.ForecastWeather.Where(x => x.WeatherDate == w.WeatherDate && x.CityName == w.CityName).FirstOrDefault();
            if (weather is null)
            {
                context.Add(w);
            }
            else
            {
                weather.TempDay = w.TempDay;
                weather.TempNight = w.TempNight;
                weather.Pressure = w.Pressure;
                weather.AirHumidity = w.AirHumidity;
                weather.WindDirection = w.WindDirection;

            }
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
