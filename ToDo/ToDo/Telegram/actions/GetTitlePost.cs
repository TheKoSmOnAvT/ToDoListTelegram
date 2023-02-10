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

        private PostService postService;
        public GetTitlePost(TagService tagService, PostService postService)
        {
            this.tagService = tagService;
            this.postService = postService;
        }


        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if ((update.Message?.ReplyToMessage?.Text?.Contains("Опишите задачу") ?? false) && (update.Message?.ReplyToMessage?.From?.IsBot ?? false))
            {

                int page = 0;
                int countElements = GlobalParams.CountTagsOnPage;

                //берется ответ на сообщение
                var idPost = postService.Add(update.Message.Text, update.Message.Chat.Id);


                var tags = tagService.Get(page * countElements, countElements);

                var keyboard = TagsTable.CreateButtonsList(tags, page, idPost);


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


