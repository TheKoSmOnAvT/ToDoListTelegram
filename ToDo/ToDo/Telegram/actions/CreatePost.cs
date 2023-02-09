using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ToDo.Telegram.Bot;

namespace ToDo.Telegram.actions
{
    /// <summary>
    /// Инициализацяи создания поста
    /// </summary>
    public class CreatePost : IChain
    {

        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if (update.Message?.Text?.StartsWith("/create") ?? false)
            {
                //отсылается сообщение для иниц создания сообщения
                botClient.SendTextMessageAsync(
                   chatId: update.Message.Chat.Id,
                   text: "Опишите задачу",
                   replyMarkup: new ForceReplyMarkup { Selective = true }
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
