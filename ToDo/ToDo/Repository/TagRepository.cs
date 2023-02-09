using Microsoft.EntityFrameworkCore;
using ToDo.Entity;
using ToDo.Exceptions;

namespace ToDo.Repository
{
    public class TagRepository
    {
        public IDbContextFactory<ToDoContext> ToDoContextFactory { get; set; }
        public TagRepository(IDbContextFactory<ToDoContext> toDoContext)
        {
            ToDoContextFactory = toDoContext;
        }

        public int Add(Tag tag)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            var entity = context.Add(tag);
            context.SaveChanges();
            return entity.Entity.id;
        }

        public bool Change(Tag tag)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            var entity = context.Tags.Where(x => x.id == tag.id).FirstOrDefault();
            if (entity == null)
            {
                throw new NotFoundException("tag change item not found");
            }
            entity.title = tag.title;

            return context.SaveChanges() != 0;
        }

        public List<Tag> Get()
        {
            using var context = ToDoContextFactory.CreateDbContext();
            return context.Tags.ToList();
        }

        public List<Tag> Get(int skip, int limit)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            return context.Tags.Skip(skip).Take(limit).ToList();
        }

        public bool Remove(int id)
        {
            using var context = ToDoContextFactory.CreateDbContext();
            //var quey = ToDoContext.Tags.Where(x => x.id == id).AsQueryable();
            context.Tags.Remove(new Tag()
            {
                id = id
            });
            return context.SaveChanges() == 1;
        }
    }
}
