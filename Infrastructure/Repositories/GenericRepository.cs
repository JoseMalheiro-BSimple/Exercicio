﻿using System.Threading.Tasks;
using Domain.IRepository;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<TDomain, TDataModel> : IGenericRepository<TDomain, TDataModel> where TDomain : class where TDataModel : class
    {
        protected readonly DbContext _context;
        private readonly IMapper<TDomain, TDataModel> _mapper;
        public GenericRepository(DbContext context, IMapper<TDomain, TDataModel> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            _context.Set<TDataModel>().Add(dataModel);
        }

        public async Task AddAsync(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            await _context.Set<TDataModel>().AddAsync(dataModel);
        }

        public void AddRange(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.ToDataModel(e));
            _context.Set<TDataModel>().AddRange(dataModels);
        }

        public async Task AddRangeAsync(IEnumerable<TDomain> entities)
        {
            var dataModels = entities.Select(e => _mapper.ToDataModel(e));
            await _context.Set<TDataModel>().AddRangeAsync(dataModels);
        }

        public IEnumerable<TDomain> GetAll()
        {
            var dataModels = _context.Set<TDataModel>().ToList();
            return _mapper.ToDomain(dataModels);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var dataModels = await _context.Set<TDataModel>().ToListAsync();
            return _mapper.ToDomain(dataModels); ;
        }

        public abstract TDomain? GetById(long id);

        public abstract Task<TDomain?> GetByIdAsync(long id);

        public void Remove(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            _context.Set<TDataModel>().Remove(dataModel);
        }

        public async Task RemoveAsync(TDomain entity)
        {
            var dataModel = _mapper.ToDataModel(entity);
            _context.Set<TDataModel>().Remove(dataModel);
            await SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<TDomain> entities)
        {
            var dataModels = _mapper.ToDataModel(entities);
            _context.Set<TDataModel>().RemoveRange(dataModels);
        }

        public async Task RemoveRangeAsync(IEnumerable<TDomain> entities)
        {
            var dataModels = _mapper.ToDataModel(entities);
            _context.Set<TDataModel>().RemoveRange(dataModels);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
