using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDo.Entity;

namespace ToDo.Repository
{
    public class PostRepository
    {
        public IDbContextFactory<ToDoContext> ToDoContextFactory { get; set; }
        public PostRepository(IDbContextFactory<ToDoContext> toDoContext)
        {
            ToDoContextFactory = toDoContext;
        }


        public int Add(Post post)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            var entity = context.Add(post);
            context.SaveChanges();
            return entity.Entity.Id;
        }

        public int Add(string title, long telegramUserId)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            var entity = context.Add(new Post()
            {
                Title = title,
                TelegramUserId = telegramUserId
            });
            context.SaveChanges();
            return entity.Entity.Id;
        }


        public List<Post> Get(int skip, int limit, int userId)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            return context.Posts.Where(x => x.TelegramUserId == userId).Skip(skip).Take(limit).ToList();
        }

        public bool SetTag(int postId, Tag tag)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            var post = context.Posts.First(x => x.Id == postId);
            post.Tag = tag;

            var result = context.SaveChanges();
            return result == 1;
        }

        public bool Remove(int id)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            context.Posts.Remove(new Post()
            {
                Id = id
            });
            return context.SaveChanges() == 1;
        }

    }
}
