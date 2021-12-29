using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;
using System.Collections.Generic;

namespace Times
{
    class Program
    {
        private static TelegramBotClient? Bot;

        public static async Task Main()
        {
            Bot = new TelegramBotClient("5019415762:AAFW_2JNQ70JqAdo6j5LQiu9wlnGj0-9Aqc");

            User me = await Bot.GetMeAsync();
            Console.Title = me.Username ?? "Time Bot";

            using var cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
            Bot.StartReceiving(HandleUpdateAsync,
                               HandleErrorAsync,
                               receiverOptions,
                               cts.Token);

            Console.WriteLine($"listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
        }


        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => OnMessage(botClient, update.Message!),
                UpdateType.EditedMessage => OnMessage(botClient, update.EditedMessage!),
                _ => UnknownUpdateHandler(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static Task UnknownUpdateHandler(ITelegramBotClient botC, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        private static async Task OnMessage(ITelegramBotClient botC, Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            var action = message.Text switch
            {
                "/help" or "/start" => Usage(botC, message),
                _ => setAnswer(botC, message)
            };
            Message sentMessage = await action;
            Console.WriteLine($"message_Id: {sentMessage.MessageId}");


        }


        static string setTimeMsg(string contry)
        {
            string temp = contry;
            DateTime gmt = default(DateTime);
            System.DateTime value = default(System.DateTime);
            gmt = DateTime.UtcNow;

            switch (temp)
            {
                case "India":
                case "Sri Lanka":
                    return gmt.AddMinutes(330).ToString();
                case "United Kingdom":
                case "Portugal":
                case "Sierra Leone":
                case "Senegal":
                case "Morocco":
                case "Mali":
                    return gmt.ToString();
                case "France":
                case "Spain":
                case "Slovenia":
                case "Slovakia":
                case "Poland":
                case "Nigeria":
                case "Niger":
                case "Hungary":
                case "Denmark":
                case "Czech Republic":
                    return gmt.AddMinutes(60).ToString();
                case "Botswana":
                case "Moldova":
                case "South Africa":
                case "Malawi":
                case "Lithuania":
                case "Libya":
                case "Turkey":
                case "Finland":
                case "Egypt":
                    return gmt.AddMinutes(120).ToString(); ;
                case "Bahrain":
                case "Somalia":
                case "Saudi Arabia":
                case "Russia":
                case "Qatar":
                case "Sudan":
                case "Madagascar":
                case "Iraq":
                    return gmt.AddMinutes(180).ToString();
                case "Iran":
                    return gmt.AddMinutes(210).ToString();
                case "Armenia":
                case "Seychelles":
                case "Reunion":
                case "Oman":
                case "Mauritius":
                case "United Arab Emirates":
                case "Georgia":
                case "Azerbaijan":
                    return gmt.AddMinutes(240).ToString();
                case "Afghanistan":
                    return gmt.AddMinutes(270).ToString();
                case "Pakistan":
                case "Maldives":
                case "Kyrgyzstan":
                    return gmt.AddMinutes(300).ToString();
                case "Nepal":
                    return gmt.AddMinutes(345).ToString();
                case "Bangladesh":
                case "Kazakhstan":
                    return gmt.AddMinutes(360).ToString();
                case "Myanmar":
                    return gmt.AddMinutes(390).ToString();
                case "Cambodia":
                case "Laos":
                    return gmt.AddMinutes(420).ToString();
                case "Philippines":
                case "Malaysia":
                case "Hong Kong":
                case "China":
                    return gmt.AddMinutes(480).ToString();
                case "Japan":
                case "Korea":
                    return gmt.AddMinutes(540).ToString();
                case "Micronesia":
                    return gmt.AddMinutes(720).ToString();
                case "Papua New Guinea":
                case "Australia":
                    return gmt.AddMinutes(600).ToString();
                case "New Caledonia":
                    return gmt.AddMinutes(660).ToString();
                case "New Zealand":
                case "Fiji":
                    return gmt.AddMinutes(720).ToString();
                case "Argentina":
                case "Brazil":
                    return gmt.AddMinutes(-180).ToString();
                case "Cuba":
                    return gmt.AddMinutes(-300).ToString();
                case "Aruba":
                case "Paraguay":
                case "Netherlands Antilles":
                case "Barbados":
                case "Chile":
                case "Dominican Republic":
                case "Guyana":
                    return gmt.AddMinutes(-240).ToString();
                case "Bahamas":
                    return gmt.AddMinutes(-240).ToString();
                case "Peru":
                case "Panama":
                case "Jamaica":
                case "Haiti":
                case "Colombia":
                case "Canary Islands":
                    return gmt.AddMinutes(-300).ToString();
                case "Bhutan":
                    return gmt.AddMinutes(360).ToString();
                case "Belize":
                case "Mexico":
                case "Honduras":
                case "Canada":
                    return gmt.AddMinutes(-360).ToString();
                case "Nicaragua":
                    return gmt.AddMinutes(-300).ToString();

                case "United States Of America":
                    return gmt.AddMinutes(-480).ToString();
                case "French Polynesia":
                    return gmt.AddMinutes(720).ToString();
                case "Samoa":
                    return gmt.AddMinutes(-660).ToString();
                case "Singapore":
                    return gmt.AddMinutes(480).ToString();
                case "Slovak Republic":
                    return gmt.AddMinutes(60).ToString();
                case "Solomon Islands":
                    return gmt.AddMinutes(660).ToString();
                case "St Helena":
                    return gmt.AddMinutes(0).ToString();
                case "St Kitts & Nevia":
                    return gmt.AddMinutes(-240).ToString();
                case "St Lucia":
                    return gmt.AddMinutes(-240).ToString();
                case "Surinam":
                    return gmt.AddMinutes(-180).ToString();
                case "Swaziland":
                    return gmt.AddMinutes(120).ToString();
                case "Sweden":
                    return gmt.AddMinutes(60).ToString();
                case "Switzerland":
                    return gmt.AddMinutes(60).ToString();
                case "Syria":
                    return gmt.AddMinutes(120).ToString();
                case "Taiwan":
                    return gmt.AddMinutes(480).ToString();
                case "Tajikistan":
                    return gmt.AddMinutes(300).ToString();
                case "Tanzania":
                    return gmt.AddMinutes(180).ToString();
                case "Thailand":
                    return gmt.AddMinutes(420).ToString();
                case "Tonga":
                    return gmt.AddMinutes(0).ToString();
                case "Trinidad & Tobago":
                    return gmt.AddMinutes(-240).ToString();
                case "Tunisia":
                    return gmt.AddMinutes(60).ToString();
                case "Turkmenistan":
                    return gmt.AddMinutes(300).ToString();
                case "Turks & Caicos Islands":
                    return gmt.AddMinutes(-240).ToString();
                case "Tuvalu":
                    return gmt.AddMinutes(720).ToString();
                case "Uganda":
                    return gmt.AddMinutes(180).ToString();
                case "Ukraine":
                    return gmt.AddMinutes(120).ToString();
                case "Uruguay":
                    return gmt.AddMinutes(-180).ToString();
                case "USA":
                    return gmt.AddMinutes(-480).ToString();
                case "Uzbekistan":
                    return gmt.AddMinutes(300).ToString();
                case "Vanuatu":
                    return gmt.AddMinutes(660).ToString();
                case "Venezuela":
                    return gmt.AddMinutes(-240).ToString();
                case "Vietnam":
                    return gmt.AddMinutes(420).ToString();
                case "Wallis & Futuna Islands":
                    return gmt.AddMinutes(720).ToString();
                case "Yemen":
                    return gmt.AddMinutes(180).ToString();
                case "Zambia":
                    return gmt.AddMinutes(120).ToString();
                case "Zimbabwe":
                    return gmt.AddMinutes(120).ToString();
                default:
                    return "Country not found!! Please check that the name is spelled correctly";
            }


        }



        static async Task<Message> setAnswer(ITelegramBotClient botC, Message message)
        {

            string countryTime = setTimeMsg(message.Text);

            return await botC.SendTextMessageAsync(
                chatId: message.Chat.Id,
                replyToMessageId: message.MessageId,
                text: countryTime
                    );

        }




        static async Task<Message> Usage(ITelegramBotClient botC, Message message)
        {
            const string Tips = "/help   - Get help\n" +
                                 "Or type one of the following words:\n" +
                                 "India" + "\n" +
                                 "Sri Lanka" + "\n" +
                                 "United Kingdom" + "\n" +
                                 "Portugal" + "\n" +
                                 "Sierra Leone" + "\n" +
                                 "Senegal" + "\n" +
                                 "Morocco" + "\n" +
                                 "Mali" + "\n" +
                                 "France" + "\n" +
                                 "Spain" + "\n" +
                                 "Slovenia" + "\n" +
                                 "Slovakia" + "\n" +
                                 "Poland" + "\n" +
                                 "Nigeria" + "\n" +
                                 "Niger" + "\n" +
                                 "Hungary" + "\n" +
                                 "Denmark" + "\n" +
                                 "Czech Republic" + "\n" +
                                 "Botswana" + "\n" +
                                 "Moldova" + "\n" +
                                 "South Africa" + "\n" +
                                 "Malawi" + "\n" +
                                 "Lithuania" + "\n" +
                                 "Libya" + "\n" +
                                 "Turkey" + "\n" +
                                 "Finland" + "\n" +
                                 "Egypt" + "\n" +
                                 "Bahrain" + "\n" +
                                 "Somalia" + "\n" +
                                 "Saudi Arabia" + "\n" +
                                 "Russia" + "\n" +
                                 "Qatar" + "\n" +
                                 "Sudan" + "\n" +
                                 "Madagascar" + "\n" +
                                 "Iraq" + "\n" +
                                 "Iran" + "\n" +
                                 "Armenia" + "\n" +
                                 "Seychelles" + "\n" +
                                 "Reunion" + "\n" +
                                 "Oman" + "\n" +
                                 "Mauritius" + "\n" +
                                 "United Arab Emirates" + "\n" +
                                 "Georgia" + "\n" +
                                 "Azerbaijan" + "\n" +
                                 "Afghanistan" + "\n" +
                                 "Pakistan" + "\n" +
                                 "Maldives" + "\n" +
                                 "Kyrgyzstan" + "\n" +
                                 "Nepal" + "\n" +
                                 "Bangladesh" + "\n" +
                                 "Kazakhstan" + "\n" +
                                 "Myanmar" + "\n" +
                                 "Cambodia" + "\n" +
                                 "Laos" + "\n" +
                                 "Philippines" + "\n" +
                                 "Malaysia" + "\n" +
                                 "Hong Kong" + "\n" +
                                 "China" + "\n" +
                                 "Japan" + "\n" +
                                 "Korea" + "\n" +
                                 "Micronesia" + "\n" +
                                 "Papua New Guinea" + "\n" +
                                 "Australia" + "\n" +
                                 "New Caledonia" + "\n" +
                                 "New Zealand" + "\n" +
                                 "Fiji" + "\n" +
                                 "Argentina" + "\n" +
                                 "Brazil" + "\n" +
                                 "Cuba" + "\n" +
                                 "Aruba" + "\n" +
                                 "Paraguay" + "\n" +
                                 "Netherlands Antilles" + "\n" +
                                 "Barbados" + "\n" +
                                 "Chile" + "\n" +
                                 "Dominican Republic" + "\n" +
                                 "Guyana" + "\n" +
                                 "Bahamas" + "\n" +
                                 "Peru" + "\n" +
                                 "Panama" + "\n" +
                                 "Jamaica" + "\n" +
                                 "Haiti" + "\n" +
                                 "Colombia" + "\n" +
                                 "Canary Islands" + "\n" +
                                 "Bhutan" + "\n" +
                                 "Belize" + "\n" +
                                 "Mexico" + "\n" +
                                 "Honduras" + "\n" +
                                 "Canada" + "\n" +
                                 "Nicaragua" + "\n" +
                                 "United States Of America" + "\n" +
                                 "French Polynesia" + "\n" +
                                 "Samoa" + "\n" +
                                 "Singapore" + "\n" +
                                 "Slovak Republic" + "\n" +
                                 "Solomon Islands" + "\n" +
                                 "St Helena" + "\n" +
                                 "St Kitts & Nevia" + "\n" +
                                 "St Lucia" + "\n" +
                                 "Surinam" + "\n" +
                                 "Swaziland" + "\n" +
                                 "Sweden" + "\n" +
                                 "Switzerland" + "\n" +
                                 "Syria" + "\n" +
                                 "Taiwan" + "\n" +
                                 "Tajikistan" + "\n" +
                                 "Tanzania" + "\n" +
                                 "Thailand" + "\n" +
                                 "Tonga" + "\n" +
                                 "Trinidad & Tobago" + "\n" +
                                 "Tunisia" + "\n" +
                                 "Turkmenistan" + "\n" +
                                 "Turks & Caicos Islands" + "\n" +
                                 "Tuvalu" + "\n" +
                                 "Uganda" + "\n" +
                                 "Ukraine" + "\n" +
                                 "Uruguay" + "\n" +
                                 "USA" + "\n" +
                                 "Uzbekistan" + "\n" +
                                 "Vanuatu" + "\n" +
                                 "Venezuela" + "\n" +
                                 "Vietnam" + "\n" +
                                 "Wallis & Futuna Islands" + "\n" +
                                 "Yemen" + "\n" +
                                 "Zambia" + "\n" +
                                 "Zimbabwe" + "\n"
                                 ;

            return await botC.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: Tips);
        }






    }
}
