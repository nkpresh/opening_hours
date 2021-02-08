using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Opening_Hours
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Redeem();
        }
        public static void Redeem()
        {
            string ProjectLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string SecondLocation = Directory.GetParent(ProjectLocation).FullName;
            string FilePath = Path.Combine(SecondLocation, "json1.json");
            string ReadJsonFile = File.ReadAllText(FilePath);
            var jsonFile = JsonConvert.DeserializeObject<Day>(ReadJsonFile);
            GetSchedule(jsonFile);

        }
        public static void GetSchedule(Day day)
        {
            List<List<Period>> ItWorkedOO = new List<List<Period>>
            {
                day.Monday,
                day.Tuesday,
                day.Wednesday,
                day.Thursday,
                day.Friday,
                day.Saturday,
                day.Sunday
            };
            int i = 0;
            var Value = Enum.GetValues(typeof(DayOfWeek));
            foreach (var items in ItWorkedOO)
            {
                string KeyValuePair = Value.GetValue(i) + ": ";
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        if (item.Type == "open")
                        {
                            if ((item.Value / 3600) > 12)
                            {
                                KeyValuePair += (item.Value / 3600) - 12 + "pm-";
                            }
                            else
                            {
                                KeyValuePair += (item.Value / 3600) + "am-";
                            }
                        }
                        else
                        {
                            if ((item.Value / 3600) > 12)
                            {
                                KeyValuePair += (item.Value / 3600) - 12 + "pm,";
                            }
                            else
                            {
                                KeyValuePair += (item.Value / 3600) + "am,";
                            }
                        }
                    }
                    Console.WriteLine(KeyValuePair);
                }
                else
                {
                    KeyValuePair += "Closed";
                    Console.WriteLine(KeyValuePair);
                }
                i++;
            }
            Console.ReadKey();

        }
    }
    public class Day
    {
        public List<Period> Monday { get; set; }
        public List<Period> Tuesday { get; set; }
        public List<Period> Wednesday { get; set; }
        public List<Period> Thursday { get; set; }
        public List<Period> Friday { get; set; }
        public List<Period> Saturday { get; set; }
        public List<Period> Sunday { get; set; }
    }
    public class Period
    {
        public string Type { get; set; }
        public int Value { get; set; }
    }
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
