using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

using System.Configuration;

namespace ToDo.Telegram.Bot
{
    public class InitBot : BackgroundService
    {

        private TelegramBotClient BotClient;
        private BotHandler botHandler;
        private IConfiguration configuration;


        public InitBot(BotHandler BotHandler, IConfiguration configuration) // BotHandler BotHandler,
        {
            this.botHandler = BotHandler;
            this.configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (configuration["telegram-token"] == null)
            {
                throw new ArgumentNullException("Telegram bot token is null");
            }

            BotClient = new TelegramBotClient(configuration["telegram-token"]!);

            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            return BotClient.ReceiveAsync(
                   updateHandler: botHandler.HandleUpdateAsync,
                   pollingErrorHandler: botHandler.HandlePollingErrorAsync,
                   receiverOptions: receiverOptions,
                   cancellationToken: cts.Token
               );


        }
    }
}
