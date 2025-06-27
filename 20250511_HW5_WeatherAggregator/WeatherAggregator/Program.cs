using System.Text;

namespace WeatherAggregator
{
    internal class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (Month m = Month.January; m <= Month.December; m++)
            {
                Weather w = GenerateWeather(m); // генерируется погода
                Console.WriteLine($"\n Weather for {m}:\n{GenerateWeatherView(w)}");
            }

            //for (int i = 1; i <= 12; i++)
            //{
            //    Month m = (Month)i;
            //    Weather w = GenerateWeather(m); // генерируется погода
            //    Console.WriteLine($"\n Weather for {m}:\n{ToWeatherString(w)}");
            //}
        }

        // TODO: magic numbers
        // TODO: перевести массив на работу с позициями
        // TODO: binary search - можно ли использовать
        public static Weather GenerateWeather(Month month, int maxWeatherState = 4)
        {
            Weather weather = Weather.None;
            // Weather[] possible = GetSeasonalWeatherFlags(month); // получает возможные варианты погоды для месяца
            Weather possible = GetWeather(month);

            int count = rand.Next(1, maxWeatherState + 1); // выбирается количество погодных состояний

            for (int i = 0; i < count; i++)
            {
                //Weather choice = possible[rand.Next(possible.Length)]; // генерируется один из возможных ариантов
                int bitPosition = rand.Next(8);

                Weather choice = (Weather)(1 << bitPosition);

                if (!HasContradiction(weather, choice)) // проверка на противоречие
                {
                    weather |= choice;
                }
            }

            return weather;
        }

        // массив возмодных погодных явлений, в зависимости от сезона
        // TODO: сделать использование break
        // TODO: статические массивы
        // TODO: флаги
        private static readonly Weather[] WinterWeather = new Weather[]
        {
            Weather.Snowy,
            Weather.Cloudy,
            Weather.Foggy,
            Weather.Windy
        };

        private static readonly Weather[] SpringWeather = new Weather[]
        {
            Weather.Sunny,
            Weather.Cloudy,
            Weather.Rainy,
            Weather.Windy
        };

        private static readonly Weather[] SummerWeather = new Weather[]
        {
            Weather.Sunny,
            Weather.Rainy,
            Weather.Thunderstorm,
            Weather.Windy
        };

        private static readonly Weather[] AutumnWeather = new Weather[]
        {
            Weather.Cloudy,
            Weather.Rainy,
            Weather.Foggy,
            Weather.Sunny
        };

        private static Weather AutumnWeatherTemp = Weather.Cloudy | Weather.Rainy | Weather.Foggy | Weather.Sunny;

        private static readonly Weather[] DefaultWeather = new Weather[] 
        { 
            Weather.Sunny 
        };

        // TODO: доделать с остальными

        private static Weather GetWeather(Month month)
        {
            Weather result = Weather.None;

            switch (month)
            {
                case Month.December:
                case Month.January:
                case Month.February:
                   //result = WinterWeather;
                    break;

                case Month.March:
                case Month.April:
                case Month.May:
                   // result = SpringWeather;
                    break;

                case Month.June:
                case Month.July:
                case Month.August:
                   // result = SummerWeather;
                    break;

                case Month.September:
                case Month.October:
                case Month.November:
                    result = AutumnWeatherTemp;
                    break;

                default:
                    result = Weather.None;
                    break;
            }
            return result;
        }

        private static Weather[] GetSeasonalWeatherFlags(Month month)
        {
            Weather[] result = DefaultWeather; // по умолчанию - солнечная погода

            switch (month)
            {
                case Month.December:
                case Month.January:
                case Month.February:
                    result = WinterWeather;
                    break;

                case Month.March:
                case Month.April:
                case Month.May:
                    result = SpringWeather;
                    break;

                case Month.June:
                case Month.July:
                case Month.August:
                    result = SummerWeather;
                    break;

                case Month.September:
                case Month.October:
                case Month.November:
                    result = AutumnWeather;
                    break;

                default:
                    result = DefaultWeather;
                    break;
            }
            return result;
        }

        // проверка на противоречия
        // Is##, Has##
        // TODO: уменьшить return - для каждого случая отдельный метод
        private static bool HasContradiction(Weather current, Weather next)
        {
            return IsContradictionSunnyFoggy(current, next) ||
                   IsContradictionThunderstormFoggy(current, next) ||
                   IsContradictionSunnySnowy(current, next) ||
                   current.HasFlag(next);
        }

        private static bool IsContradictionSunnySnowy(Weather current, Weather next)
        {
            return(current.HasFlag(Weather.Sunny) && next == Weather.Snowy) ||
                (current.HasFlag(Weather.Snowy) && next == Weather.Sunny);
        }

        private static bool IsContradictionThunderstormFoggy(Weather current, Weather next)
        {
            return (current.HasFlag(Weather.Thunderstorm) && next == Weather.Foggy) ||
                   (current.HasFlag(Weather.Foggy) && next == Weather.Thunderstorm);
        }

        private static bool IsContradictionSunnyFoggy(Weather current, Weather next)
        {
            return (current.HasFlag(Weather.Sunny) && next == Weather.Foggy) ||
                   (current.HasFlag(Weather.Foggy) && next == Weather.Sunny);
        }



        private static string GenerateWeatherView(Weather weather)
        {
            StringBuilder sb = new StringBuilder(8);
            //Array values = Enum.GetValues(typeof(Weather));

            for (Weather w = Weather.Sunny; w <= Weather.Foggy; w = (Weather)((byte)w << 1))
            {
                //Weather w = (Weather)values.GetValue(i);

                //if (w == 0) 
                //{ 
                //    continue;
                //}

                if (weather.HasFlag(w))
                {
                    sb.AppendLine($"{GetSymbol(w)}");
                }
            }

            return sb.ToString();
        }

        //private static string ToWeatherString(Weather weather)
        //{
        //    string result = "";
        //    for (int i = 1; i <= 64; i <<= 1)
        //    {
        //        Weather w = (Weather)i;
        //        if (weather.HasFlag(w))
        //        {
        //            result += $"{GetSymbol(w)} {w}\n";
        //        }
        //    }
        //    return result.Trim();
        //}

        // TODO: через HasFlag

        /// <summary>
        /// Symbols
        /// </summary>
        /// <param name="w">Requires one bit</param>
        /// <returns></returns>
        private static char GetSymbol(Weather w)
        {
             
            return w switch
            {
                Weather.None => '\u274C', 
                Weather.Sunny => '\u2600',
                Weather.Cloudy => '\u2601',
                Weather.Rainy => '\u2602',
                Weather.Snowy => '\u2744',
                Weather.Thunderstorm => '\u26A1',
                Weather.Windy => '\u2194',
                Weather.Foggy => '\u2248',
                _ => '?',
            };
        }

       
    }
}

