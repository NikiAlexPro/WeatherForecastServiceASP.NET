
create database weatherdb;
use weatherdb;

create table ForecastWeather(
Id int primary key auto_increment,
CityName varchar(30) not null,
WeatherDate date not null,
TempDay varchar(30) not null,
TempNight varchar(30) not null,
Pressure varchar(30) not null,
AirHumidity varchar(30) not null,
WindDirection varchar(30) not null
);


select * from ForecastWeather;

