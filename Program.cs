using System;
using Serilog;
namespace Bank_logging
{
    class Program
    {
        public static void Main()
        {
            var Kurs = 67;
            double coef = 0.37;
            Console.WriteLine("1 доллар = " + 65);
            double znach = 0;
            var prov = false;
            var result = 0d;
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithProperty("Курс доллора по отношению к рублю:", Kurs)
            .WriteTo.Seq("http://localhost:5341/", apiKey: "LmfckVulgaO0Z1LKtG4l")
            .CreateLogger();
            while (!prov || znach < 1)
            {

                Console.WriteLine("Введите количество денег: ");
                var str = Console.ReadLine();
                prov = double.TryParse(str, out znach);
                if (!prov || znach < 1)
                {
                    Console.WriteLine("Введено не правильное значение.");
                    Log.Error("Введенно не правильное значение. Обмен валюты не выполнен.");
                }
            }
            Log.Information($"Ввод прошёл успешно. Количество долларов: {znach}");
            result = znach * Kurs;

            Console.WriteLine("Процент налога: " + coef);
            if (znach < 500)
            {
                Log.Information($"Коммиссия = 8 рублей");
                Console.WriteLine("С перевода на нынешний курс получилось с налогом в 8 рублей: " + (result - 8));
            }
            else
            {
                Log.Information($"Коммиссия = {coef}% рублей");
                Console.WriteLine("С перевода на нынешний курс получилось с налогом в 0.37%: " + (result - result * (coef / 100)));
            }
            Log.Information($"Обмен валютой был успешно завершён. Сумма перевода равна {result}");
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}
