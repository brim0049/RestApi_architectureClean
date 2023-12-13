using System;
using System.Collections.Generic;
using System.Text;
using MESolution.SharedKernel.Interfaces;
using MESolution.SharedKernel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;


namespace MESolution.Infrastructure
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly MESolutionContext _MESolutionContext;
        public EfRepository(MESolutionContext MESolutionContext)
        {
            _MESolutionContext = MESolutionContext;

        }

        public async Task<T> AddAsync(T entity)
        {
            await _MESolutionContext.Set<T>().AddAsync(entity);
            await _MESolutionContext.SaveChangesAsync();
            return entity;

        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _MESolutionContext.Set<T>().Remove(entity);
            await _MESolutionContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _MESolutionContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _MESolutionContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _MESolutionContext.Entry(entity).State = EntityState.Modified;
            await _MESolutionContext.SaveChangesAsync();
        }
        public async Task PatchAsync<T>(int id, JsonPatchDocument<T> patchDoc) where T : class
        {
            var entity = await _MESolutionContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                // Gérer le cas où l'entité n'est pas trouvée
                return;
            }

            patchDoc.ApplyTo(entity);

            // Marquer explicitement les propriétés modifiées comme modifiées
            foreach (var entry in _MESolutionContext.Entry(entity).Properties)
            {
                if (entry.IsModified)
                {
                    _MESolutionContext.Entry(entity).Property(entry.Metadata.Name).IsModified = true;
                }
            }

            await _MESolutionContext.SaveChangesAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_MESolutionContext.Set<T>().AsQueryable(), spec);
        }

    }
}

