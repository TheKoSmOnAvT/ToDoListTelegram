using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using ToDo.Telegram;
using ToDo.Telegram.Bot;

namespace ToDo.Service.telegram.actions
{

    /// <summary>
    /// Команда /start
    /// </summary>
    public class Start : IChain
    {
        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if (update.Message?.Text?.StartsWith("/start") ?? false)
            {

                botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "СТАРТУЕМ",
                    cancellationToken: cancellationToken
                    );
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
