using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yamaanco.Application.Interfaces;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IYamaancoDbContext Context;

        public Repository(IYamaancoDbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Count(predicate);
        }

        public long Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate)?.ToList();
        }

        public TEntity Get(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Single(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<long> CountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().CountAsync(predicate);
        }

        public void Remove(string id)
        {
            TEntity entityToDelete = Context.Set<TEntity>().Find(id);
            Context.Set<TEntity>().Remove(entityToDelete);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}