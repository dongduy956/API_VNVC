using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Enum;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;

namespace VNVCWEBAPI.REPO.Repository
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly VNVCContext context;
        private readonly DbSet<T> entities;
        public Repository(VNVCContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }
        public async Task<bool> Delete(T entity)
        {
            if (entity == null)
                return false;

            entities.Attach(entity);

            entity.isTrash = true;
            entities.Entry(entity).Property(x => x.isTrash).IsModified = true;

            await this.SavechangesAsync();

            return true;
        }

        public async Task<bool> DeleteFromTrash(T entity)
        {
            if (entity == null)
                return false;

            entities.Remove(entity);
            await this.SavechangesAsync();

            return true;
        }

        public async Task<bool> DeleteFromTrashRange(IEnumerable<T> lstEntity)
        {
            if (lstEntity == null || lstEntity.Count() < 1)
                return false;
            entities.RemoveRange(lstEntity);

            await this.SavechangesAsync();
            return true;
        }

        public async Task<bool> DeleteRange(IEnumerable<T> lstEntity)
        {
            foreach (var item in lstEntity)
            {
                await this.Delete(item);
            }
            return true;
        }

        public IQueryable<T> GetAll(SelectEnum.Select select)
        {
            switch (select)
            {
                case SelectEnum.Select.NONTRASH:
                    return entities.Where(x => x.isTrash == false);
                case SelectEnum.Select.TRASH:
                    return entities.Where(x => x.isTrash == true);
                default:
                    return entities.AsQueryable();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await entities.FindAsync(id);
            return entity;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            entity.Created = DateTime.Now;

            await entities.AddAsync(entity);
            await this.SavechangesAsync();

            return true;
        }

        public async Task<bool> InsertRangeAsync(IEnumerable<T> lstEntity)
        {
            foreach (var item in lstEntity)
            {
                item.Created = DateTime.Now;
            }

            await entities.AddRangeAsync(lstEntity);
            await this.SavechangesAsync();
            return true;

        }

        public async Task SavechangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity, params string[]? parameter)
        {

            context.Entry(entity).State = EntityState.Modified;

            if (parameter != null)
            {
                foreach (var param in parameter)
                {
                    context.Entry(entity).Property(param).IsModified = false;
                }
            }

            context.Entry(entity).Property(x => x.Created).IsModified = false;
            await this.SavechangesAsync();
            return true;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> lstEntity)
        {
            context.UpdateRange(lstEntity);
            await this.SavechangesAsync();
            return true;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> preicate)
        {
            return entities.Where(preicate);
        }
    }
}
