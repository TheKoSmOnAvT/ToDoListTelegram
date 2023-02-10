using Telegram.Bot;
using Telegram.Bot.Types;
using ToDo.Service;
using ToDo.Telegram.Bot;
using ToDo.Telegram.Utils;

namespace ToDo.Telegram.actions
{
    public class TagsHasBeenSelected : IChain
    {
        private PostService postService;
        public TagsHasBeenSelected(PostService postService)
        {
            this.postService = postService;
        }

        public bool isExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, CallBackEnum? idCallBack, List<string> callbackItems)
        {
            if (idCallBack == CallBackEnum.TagsHasBeenSelected)
            {
                int tagId = int.Parse(callbackItems[1]);
                int postId = int.Parse(callbackItems[2]);

                var ok = postService.SetTag(postId, tagId);
                if(ok == false)
                {
                    throw new Exception("TagsHasBeenSelected set tag ex");
                }


                botClient.EditMessageTextAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    messageId: update.CallbackQuery.Message.MessageId,
                    text: $"Задача добавлена",
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
