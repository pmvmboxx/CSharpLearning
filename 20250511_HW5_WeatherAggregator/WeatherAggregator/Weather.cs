using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator
{
    // TODO: битовое представление
    [Flags]
    public enum Weather : byte
    {
        None = 0,
        Sunny = 1,
        Cloudy = 2,
        Rainy = 4,
        Snowy = 8,
        Thunderstorm = 16,
        Windy = 32,
        Foggy = 64
    }
}
