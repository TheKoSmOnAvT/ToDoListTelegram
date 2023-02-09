using Telegram.Bot;
using Telegram.Bot.Types;

namespace ToDo.Telegram.Bot
{
    public interface IChain
    { 
        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems);
    }
}