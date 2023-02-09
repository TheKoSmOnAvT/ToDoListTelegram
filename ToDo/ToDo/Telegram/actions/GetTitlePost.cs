using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ToDo.Entity;
using ToDo.Model;
using ToDo.Service;
using ToDo.Telegram.Bot;
using ToDo.Telegram.Utils;

namespace ToDo.Telegram.actions
{

    /// <summary>
    /// Получение название задание и формирование таблицы тегов
    /// </summary>
    public class GetTitlePost : IChain
    {
        private TagService tagService;

        public GetTitlePost(TagService tagService)
        {
            this.tagService = tagService;
        }


        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if ((update.Message?.ReplyToMessage?.Text?.Contains("Опишите задачу") ?? false) && (update.Message?.ReplyToMessage?.From?.IsBot ?? false))
            {

                int page = 0;
                int countElements = 6;

                //берется ответ на сообщение
                //TODO: save в бд
                var titlePost = update.Message.Text;

                var tags = tagService.Get(page * countElements, countElements);

                var keyboard = TagsTable.CreateButtonsList(tags, page);


                botClient.DeleteMessageAsync(
                      chatId: update.Message.Chat.Id,
                      messageId: update.Message.ReplyToMessage.MessageId
                    );

                botClient.DeleteMessageAsync(
                      chatId: update.Message.Chat.Id,
                      messageId: update.Message.MessageId
                    );


                botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: $"Выберите тематику записи",
                    replyMarkup: keyboard,
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


