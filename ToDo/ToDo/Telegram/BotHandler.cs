using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Diagnostics;

namespace ToDo.Telegram.Bot
{
    public class BotHandler
    {
        private IEnumerable<IChain> chains;

        public BotHandler(IEnumerable<IChain> chains)
        {
            this.chains = chains;
        }

        public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            CallBackEnum? idCallBack = null;
            List<string> callbackItems = new();
            if (update.CallbackQuery != null)
            {
                try
                {
                    callbackItems = update.CallbackQuery.Data.Split("_").ToList();
                    idCallBack = (CallBackEnum)int.Parse(callbackItems.First());
                }
                catch { }
            }

            foreach (var chain in chains)
            {
                if (chain.isExecute(botClient, update, cancellationToken, idCallBack, callbackItems))
                {
                    break;
                }
            }

            return Task.CompletedTask;
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Trace.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
