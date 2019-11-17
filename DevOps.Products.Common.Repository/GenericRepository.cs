using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Common.Repository
{
    public class GenericRepository<TContext, TEntity, TDTO> : IGenericRepository<TEntity, TDTO> 
                                                            where TContext : DbContext 
                                                            where TEntity : Entity
                                                            where TDTO : class
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public GenericRepository(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDTO>> Get(Expression<Func<TEntity, bool>> filter = null,
                                                 string includeProperties = "")
        {
            IQueryable<TEntity> entities = _dbSet;

            entities = entities.Where(entity => entity.IsActive);

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                entities = entities.Include(includeProperty);
            }

            IQueryable<TDTO> dtos = _mapper.ProjectTo<TDTO>(entities);

            return await dtos.ToListAsync();
        }

        public async Task<TDTO> GetByID(int id)
        {
            TEntity foundEntity = await _dbSet.Where(entity => entity.IsActive).SingleAsync(entity => entity.ID == id);
            return _mapper.Map<TDTO>(foundEntity);
        }

        public async Task Create(TDTO dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            entity.IsActive = true;

            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);

            entityToDelete.IsActive = false;

            _dbSet.Attach(entityToDelete);
            _context.Entry(entityToDelete).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Update(TDTO dto)
        {
            TEntity entityToUpdate = _mapper.Map<TEntity>(dto);
            
            if ((await _dbSet.FindAsync(entityToUpdate.ID)).IsActive == false) throw new DbUpdateConcurrencyException();

            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> EntityExists(int id)
        {
            return await _dbSet.AnyAsync(entity => entity.ID == id);
        }
    }
}
