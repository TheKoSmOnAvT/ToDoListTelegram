using AutoMapper;
using System;
using ToDo.Entity;
using ToDo.Model;
using ToDo.Repository;

namespace ToDo.Service
{
    
    public class TagService
    {
        private TagRepository Repository;
        private readonly IMapper _mapper;
        public TagService(IMapper _mapper, TagRepository repository)
        {
            Repository = repository;
            this._mapper = _mapper;
        }

        public List<TagModel> Get()
        {
            return Repository.Get().Select(x => _mapper.Map<TagModel>(x)).ToList();
        }

        public List<TagModel> Get(int skip, int offset)
        {
            return Repository.Get(skip, offset).Select(x => _mapper.Map<TagModel>(x)).ToList();
        }

        public int Create(TagModel newTag)
        {
            return Repository.Add(_mapper.Map<Tag>(newTag));
        }

        public bool Change(TagModel tagToChange)
        {
            return Repository.Change(_mapper.Map<Tag>(tagToChange));
        }

        public bool Remove(int id)
        {
            return Repository.Remove(id);
        }
    }
}
