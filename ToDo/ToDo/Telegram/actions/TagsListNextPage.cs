using Telegram.Bot;
using Telegram.Bot.Types;
using ToDo.Service;
using ToDo.Telegram.Bot;
using ToDo.Telegram.Utils;

namespace ToDo.Telegram.actions
{
    /// <summary>
    /// Изменение страницы таблицы
    /// </summary>
    public class TagsListNextPage : IChain
    {
        private TagService tagService;

        public TagsListNextPage(TagService tagService)
        {
            this.tagService = tagService;
        }


        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if (idCallBack == CallBackEnum.TagsListNextPage)
            {
                int page = int.Parse(callbackItems[1]);
                int countElements = 6;

                var tags = tagService.Get(page * countElements, countElements);

                var keyboard = TagsTable.CreateButtonsList(tags, page);

                botClient.EditMessageTextAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    messageId: update.CallbackQuery.Message.MessageId,
                    text: $"Выберите тематику записи",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken
                ); ; ;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
