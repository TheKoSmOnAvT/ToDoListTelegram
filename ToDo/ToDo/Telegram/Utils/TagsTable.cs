using Telegram.Bot.Types.ReplyMarkups;
using ToDo.Model;

namespace ToDo.Telegram.Utils
{
    public class TagsTable
    {

        public static InlineKeyboardMarkup CreateButtonsList(List<TagModel> tags, int page)
        {
            List<List<InlineKeyboardButton>> controlsRow = new();
            foreach (var tag in tags)
            {
                controlsRow.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton(tag.title) { CallbackData = $"{((int)CallBackEnum.TagsHasBeenSelected)}_{tag.id}" } });
            }


            var control = new List<InlineKeyboardButton>();

            var pagePrev = page - 1;
            if (page != 0)
            {
                control.Add(new InlineKeyboardButton("<-") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{(pagePrev)}" });
            }
            if (tags.Count >= 6)
            {
                control.Add(new InlineKeyboardButton("->") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{(page + 1)}" });
            }

            //new List<InlineKeyboardButton>() {
            //        new InlineKeyboardButton("<-") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{(page - 1 >=0 ? page - 1 : page) }"},
            //        new InlineKeyboardButton("->") { CallbackData = $"{((int)CallBackEnum.TagsListNextPage)}_{page + 1}"},
            //    }

            controlsRow.Add(control);


            return new InlineKeyboardMarkup(controlsRow);
        }
    }
}
