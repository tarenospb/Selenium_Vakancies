using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Vakancies
{
    class Program
    {
        public static string otdel = "Разработка продуктов";
        public static string lang = "Английский";
        public static int vakancy = 6;
        public static int vak = 0;//количество вакансий
        public static FirefoxDriver driver;

        static void Automation() //выполнение действий над браузером
        {
            // запуск браузера
            driver.Navigate().GoToUrl("https://careers.veeam.ru/vacancies");
            driver.Manage().Window.Maximize();
            //поиск элементов меню как кнопок
            var elemBut = driver.FindElements(By.TagName("button"));
            // поиск поля выбора языков и его выбор
            foreach (var i in elemBut) 
            { 
                if (i.Text == "Все языки") 
                { 
                    i.Click(); 
                } 
            }
            var elemBut3 = driver.FindElements(By.ClassName("custom-control"));
            foreach (var i in elemBut3) 
            { 
                if (i.Text == lang) 
                { 
                    i.Click(); 
                } 
            }

            // поиск поля выбора технологий и его выбор
            foreach (var i in elemBut) 
            { 
                if (i.Text == "Все отделы") 
                { 
                    i.Click(); 
                } 
            }
            var elemBut2 = driver.FindElements(By.ClassName("dropdown-item"));
            foreach (var i in elemBut2) 
            { 
                if (i.Text == otdel) 
                { 
                    i.Click(); 
                } 
            }
        }

        static void Counts() // подсчет вакансий по количеству карточек
        {
            var elemCard = driver.FindElements(By.ClassName("card-sm"));
            foreach (var i in elemCard) { vak++; }
            Console.WriteLine("---------------------------------");
            Console.Write("Count of vakancies - {0}", vak);
            // сравнение с ожиданием
            if (vak == vakancy) { 
                Console.Write(" - it is OK"); 
            }
            else if (vak > vakancy)
            { 
                Console.Write(" - it is greater than the expectation."); 
            }
            else 
            { 
                Console.Write(" - it is less than the expectation."); 
            }
            //закрытие браузера
            driver.Quit();
        }

        static void Main(string[] args)
        {
            try
            {
                //загрузка параметров из cmd
                // monitor.exe {Отдел} {Язык} {Число ожидаемых вакансий}
                otdel = args[0];
                lang = args[1];
                if (Int32.TryParse(args[2], out vakancy) == false)
                {
                    Console.WriteLine("Params is incorreсt.");
                    Environment.Exit(0);
                };
                // загрузка драйвера
                driver = new FirefoxDriver();
                Automation();
                Counts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}