using AutoMapper;
using Microsoft.Extensions.Hosting;
using ToDo.Entity;
using ToDo.Model;
using ToDo.Repository;

namespace ToDo.Service
{
    public class PostService
    {

        private PostRepository postRepository;
        private TagRepository tagRepository;
        private readonly IMapper _mapper;

        public PostService(IMapper _mapper, PostRepository repository, TagRepository tagRepository)
        {
            postRepository = repository;
            this._mapper = _mapper;
            this.tagRepository = tagRepository;
        }

        public int Add(PostModel post)
        {
            return postRepository.Add(_mapper.Map<Post>(post));
        }

        public int Add(string title, long telegramUserId)
        {
            return postRepository.Add(title, telegramUserId);
        }

        public List<PostModel> Get(int skip, int limit, int userId)
        {
            return postRepository.Get(skip, limit, userId).Select(x => _mapper.Map<PostModel>(x)).ToList();
        }

        public bool Remove(int id)
        {
            return postRepository.Remove(id);
        }

        public bool SetTag(int postId, int tagId)
        {
            var tag = tagRepository.Get(tagId);
            return postRepository.SetTag(postId, tag);
        }
    }
}
