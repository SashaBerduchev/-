using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Авто_Ресурс_Сервис.Parser;

namespace Авто_Ресурс_Сервис.Windows
{
    /// <summary>
    /// Interaction logic for TelegramSendMessageWindow.xaml
    /// </summary>
    public partial class TelegramSendMessageWindow : Window
    {
        TeleGramChats telegramBot;
        static TelegramBotClient bot = new TelegramBotClient("5578780345:AAHU8lkJPlRgtOc13xYoDnVtzc_Nc1k5HDE");
        public TelegramSendMessageWindow(TeleGramChats tele)
        {
            InitializeComponent();
            telegramBot = tele;
            idChat.Text = telegramBot.idChat.ToString();
            Login.Text = telegramBot.Login;
            Name.Text = telegramBot.Name;

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            //bot.StartReceiving(
            //    HandleUpdateAsync,
            //    HandleErrorAsync,
            //    receiverOptions,
            //    cancellationToken
            //);
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            MessageBox.Show(exception.ToString());
        }
        private void SendTg_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
            this.Close();
        }

        private async Task SendMessage()
        {
            await bot.SendTextMessageAsync(telegramBot.idChat, sms.Text);
            
        }
    }
}