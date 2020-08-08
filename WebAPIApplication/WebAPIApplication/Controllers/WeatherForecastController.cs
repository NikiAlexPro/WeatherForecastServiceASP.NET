using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using WebAPIApplication.Models;
using Ninject;

namespace WebAPIApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IRepository repository;

        public WeatherForecastController()
        {
            IKernel ninjectkernel = new StandardKernel();
            ninjectkernel.Bind<IRepository>().To<WeatherForecastRepository>();
            repository = ninjectkernel.Get<IRepository>();
        }


        [HttpGet("{city},{day},{month}")]
        public IActionResult Get(string city, int day, int month)
        {
            //Яндекс Геокодер -> координаты в запросе погоды
            string[] latlon = CityDefinition.GetCityCoordinates(city);
            double slat = Convert.ToDouble(latlon[1]);
            double slon = Convert.ToDouble(latlon[0]);
            string monthDefinition = CityDefinition.monthDictionary[month];

            var html = $@"https://yandex.ru/pogoda/month/{monthDefinition}?lat={CityDefinition.ToYandexCoordinate(slat)}&lon={CityDefinition.ToYandexCoordinate(slon)}&via=cnav";
            var web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var fg = htmlDoc.DocumentNode.SelectNodes($"//div[@class='climate-calendar-day__day'][. ='{day}']/../..//div[@class='climate-calendar-day__detailed-container-center']");
            
            //2-температура днем 1-температура ночью
            var tempWeatherArray = fg[0].SelectNodes("*//div").Where(x => x.Name == "div").ToArray();
            
            //1-давление 3-влажность 5 - скорость ветра
            var paramWeatherArray = fg[0].SelectNodes("*//td").Where(x => x.Name == "td").ToArray();

            var icon = fg[0].SelectSingleNode("//img").Attributes["src"].Value;
            var calendarDay = fg[0].SelectSingleNode("h6").InnerText;
            DateTime weatherdate = DateTime.Parse($"{day}/{month}/{DateTime.Now.Year.ToString()}").Date;
            string tempDay = tempWeatherArray[2].InnerText;
            string tempNight = tempWeatherArray[1].InnerText;
            string pressure = paramWeatherArray[1].InnerText;
            string airHumidity = paramWeatherArray[3].InnerText;
            string windDirection = paramWeatherArray[5].InnerText;


            WeatherForecast weatherForecast = new WeatherForecast()
            {
                CityName = city,
                WeatherDate = weatherdate,
                TempDay = tempDay,
                TempNight = tempNight,
                Pressure = pressure,
                AirHumidity = airHumidity,
                WindDirection = windDirection

            };

            repository.Save(weatherForecast);

            return Ok(new
            {
                IconWeather = icon,
                CalendarDay = calendarDay,
                CityName = city,
                WeatherDate = weatherdate,
                TempDay = tempDay,
                TempNight = tempNight,
                Pressure = pressure,
                AirHumidity = airHumidity,
                WindDirection = windDirection
            });
        }
    }
}
