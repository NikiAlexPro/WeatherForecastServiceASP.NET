using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    interface IRepository
    {
        void Save(WeatherForecast w);
        //WeatherForecast Get(int id);
        //
    }
}
