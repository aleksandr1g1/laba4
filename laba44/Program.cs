using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiService
{
    public class Поездка
    {
        public string НомерАвтомобиля { get; set; }
        public decimal СтоимостьПоездки { get; set; }

        public Поездка(string номерАвтомобиля, decimal стоимостьПоездки)
        {
            НомерАвтомобиля = CheckValidString(номерАвтомобиля, "Номер автомобиля");
            СтоимостьПоездки = CheckValidDecimal(стоимостьПоездки, "Стоимость поездки");
        }

        private static string CheckValidString(string value, string description)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{description} не может быть пустым или состоять только из пробелов.");
            return value;
        }

        private static decimal CheckValidDecimal(decimal value, string description)
        {
            if (value < 0)
                throw new ArgumentException($"{description} не может быть отрицательным.");
            return value;
        }
    }

    public class Такси
    {
        public List<Поездка> Поездки { get; set; }

        public Такси()
        {
            Поездки = new List<Поездка>();
        }

        public void ДобавитьПоездку(Поездка поездка)
        {
            Поездки.Add(поездка);
        }

        public void ВывестиДоходы()
        {
            var доходыПоАвтомобилям = from поездка in Поездки
                                      group поездка by поездка.НомерАвтомобиля into группа
                                      select new
                                      {
                                          НомерАвтомобиля = группа.Key,
                                          СуммарныйДоход = группа.Sum(п => п.СтоимостьПоездки)
                                      };

            foreach (var доход in доходыПоАвтомобилям)
            {
                Console.WriteLine($"Автомобиль {доход.НомерАвтомобиля}: Суммарный доход - {доход.СуммарныйДоход:C}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var такси = new Такси();

            такси.ДобавитьПоездку(new Поездка("А123Б45", 150.50m));
            такси.ДобавитьПоездку(new Поездка("В234Г56", 200.75m));
            такси.ДобавитьПоездку(new Поездка("А123Б45", 120.30m));
            такси.ДобавитьПоездку(new Поездка("Д345Е67", 175.00m));
            такси.ДобавитьПоездку(new Поездка("В234Г56", 250.00m));

            такси.ВывестиДоходы();
        }
    }
}