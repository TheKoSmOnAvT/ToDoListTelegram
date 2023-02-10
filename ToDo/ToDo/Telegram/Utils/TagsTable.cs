using Telegram.Bot.Types.ReplyMarkups;
using ToDo.Model;

namespace ToDo.Telegram.Utils
{
    public class TagsTable
    {

        public static InlineKeyboardMarkup CreateButtonsList(List<TagModel> tags, int page, int postId)
        {
            List<List<InlineKeyboardButton>> controlsRow = new();
            foreach (var tag in tags)
            {
                controlsRow.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton(tag.title) { CallbackData = $"{((int)CallBackEnum.TagsHasBeenSelected)}_{tag.id}_{postId}" } });
            }


            var control = new List<InlineKeyboardButton>();

            var pagePrev = page - 1;
            if (page != 0)
            {
                control.Add(new InlineKeyboardButton("<-") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{(pagePrev)}_{postId}" });
            }
            if (tags.Count >= GlobalParams.CountTagsOnPage)
            {
                control.Add(new InlineKeyboardButton("->") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{(page + 1)}_{postId}" });
            }

            controlsRow.Add(control);
            return new InlineKeyboardMarkup(controlsRow);
        }
    }
}
