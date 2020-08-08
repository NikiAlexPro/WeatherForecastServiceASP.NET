using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static WebAPIApplication.GeoCoder;
using System.Text;

namespace WebAPIApplication
{
    public class CityDefinition
    {
        public static string[] GetCityCoordinates(string cityName)
        {
            try
            {
                //Запрос Яндекс.Геокодер
                string apikey = "737dd3bf-9479-4896-9e7f-85b5303c8f75";
                HttpWebRequest requestGeo = (HttpWebRequest)WebRequest.Create($"https://geocode-maps.yandex.ru/1.x/?apikey={apikey}&format=json&geocode={cityName}&results=1");

                HttpWebResponse responseGeo = (HttpWebResponse)requestGeo.GetResponse();

                using (Stream streamGeo = responseGeo.GetResponseStream())
                {
                    using (StreamReader WebstreamGeo = new StreamReader(streamGeo))
                    {
                        string streamres = WebstreamGeo.ReadToEnd();
                        var fact = JsonConvert.DeserializeObject<GeocoderResponseContainer>(streamres);
                        string proj = fact.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Point.GeoPoint;

                        StringBuilder tmp = new StringBuilder(proj);
                        tmp.Replace('.', ',');
                        string[] latlon = tmp.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        return latlon;
                    }
                }
            }
            catch(Exception)
            {
                //Москва-Тест
                StringBuilder tmp = new StringBuilder("37,622504 55,753215");
                tmp.Replace('.', ',');
                string[] latlon = tmp.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return latlon;
            }
            
        }

        public static string ToYandexCoordinate(double val)
        {
            var tmp = new StringBuilder(val.ToString());
            tmp.Replace(',', '.');
            return tmp.ToString();
        }

        public static Dictionary<int, string> monthDictionary = new Dictionary<int, string>()
        {
            { 1, "january"},
            { 2, "february"},
            { 3, "march"},
            { 4, "april"},
            { 5, "may"},
            { 6, "june"},
            { 7, "july"},
            { 8, "august"},
            { 9, "september"},
            { 10, "october"},
            { 11, "november"},
            { 12, "december"}
        };
    }
}
