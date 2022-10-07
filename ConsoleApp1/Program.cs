using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Collections.Generic;
using HtmlAgilityPack;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp1
{
    class Program
    {
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient("5264377767:AAE_HB9eQbUYpWRzVKdVP8n0USTvH8J_QC0");//токен бота
            client.StartReceiving();
            client.OnMessage += OnMessage;//событие OnMessage
            Console.ReadLine();
            client.StopReceiving();
        }

        public static void MesSend(Telegram.Bot.Types.Message msg, string url, string xpath, string lang)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url + lang);
            List<string> hrefTags = new List<string>();
            string attributes = "href";
            var news = doc.DocumentNode.SelectNodes(xpath);
            for (int i = 0; i < 10; i++)
            {
                client.SendTextMessageAsync(msg.Chat.Id, url + news[i].GetAttributeValue(attributes, null));
            }
        }

        private static async void OnMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            string url = "https://lenta.inform.kz";
            string xpath = "//*[@id='lenta']/div[@class='lenta_news_block']/div/a[1]";
            string xpathPop = "//*[@id='popular']/div[@class = 'lenta_news_block']/div/a[1]";
            if (msg.Text.ToLower() == "/start" || msg.Text.ToLower() == "выбрать язык" || msg.Text.ToLower() == "тілді таңдау" || msg.Text.ToLower() == "select language")
            {
                var rkmLang = new ReplyKeyboardMarkup();
                rkmLang.Keyboard = new KeyboardButton[][]
                {
                 new KeyboardButton[]
                 {
                     new KeyboardButton("/kz")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("/ru")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("/en")
                 },
                };
                await client.SendTextMessageAsync(msg.Chat.Id, "Тілді таңдау \n Выбрать язык \n Select language", replyMarkup: rkmLang);
            }

            else if (msg.Text == "/kz")
            {
                var rkm1 = new ReplyKeyboardMarkup();
                rkm1.Keyboard = new KeyboardButton[][]
                {
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Бүгінгі жаңалықтар")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Танымал жаңалықтар")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Тілді таңдау")
                 },
                };
                await client.SendTextMessageAsync(msg.Chat.Id, msg.Text + " тілі таңдалды", replyMarkup: rkm1);
            }

            else if (msg.Text == "/ru")
            {
                var rkm2 = new ReplyKeyboardMarkup();
                rkm2.Keyboard = new KeyboardButton[][]
            {
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Новости сегодня")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Популярные новости")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Выбрать язык")
                 },
            };

                await client.SendTextMessageAsync(msg.Chat.Id, "Выбран язык " + msg.Text, replyMarkup: rkm2);
            }

            else if (msg.Text == "/en")
            {
                var rkm3 = new ReplyKeyboardMarkup();
                rkm3.Keyboard = new KeyboardButton[][]
            {
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Today news")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Popular news")
                 },
                 new KeyboardButton[]
                 {
                     new KeyboardButton("Select language")
                 },
            };
                await client.SendTextMessageAsync(msg.Chat.Id, msg.Text + " was selected", replyMarkup: rkm3);
            }

            else if (msg.Text == "Бүгінгі жаңалықтар")
            {
                string lang = "/kz";
                MesSend(msg, url, xpath, lang);
            }

            else if (msg.Text == "Танымал жаңалықтар")
            {
                string lang = "/kz";
                MesSend(msg, url, xpathPop, lang);
            }

            else if (msg.Text == "Новости сегодня")
            {
                string lang = "/ru";
                MesSend(msg, url, xpath, lang);
            }

            else if (msg.Text == "Популярные новости")
            {
                string lang = "/ru";
                MesSend(msg, url, xpathPop, lang);
            }

            else if (msg.Text == "Today news")
            {
                string lang = "/en";
                MesSend(msg, url, xpath, lang);
            }

            else if (msg.Text == "Popular news")
            {
                string lang = "/en";
                MesSend(msg, url, xpathPop, lang);
            }

            else
            {
                await client.SendTextMessageAsync(msg.Chat.Id, "Өтінеміз, төменгі батырмаларды басыңыз! \n Пожалуйста, нажмите кнопки ниже \n Please, push the buttons below");
            }
        }
    }
}

