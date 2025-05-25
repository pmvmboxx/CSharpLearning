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
        None         = 0b00000000,   // 0
        Sunny        = 0b00000001,   // 1
        Cloudy       = 0b00000010,   // 2
        Rainy        = 0b00000100,   // 4
        Snowy        = 0b00001000,   // 8
        Thunderstorm = 0b00010000,   // 16
        Windy        = 0b00100000,   // 32
        Foggy        = 0b01000000    // 64
    }
}
