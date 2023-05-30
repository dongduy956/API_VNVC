using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Enum;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.Repository;

namespace VNVCWEBAPI.REPO.IRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(SelectEnum.Select select);
        Task<T> GetAsync(int id);
        Task<bool> InsertAsync(T entity);
        Task<bool> InsertRangeAsync(IEnumerable<T> lstEntity);
        Task<bool> UpdateAsync(T entity, params string[]? parameter);
        Task<bool> UpdateRangeAsync(IEnumerable<T> lstEntity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(IEnumerable<T> lstEntity);
        Task<bool> DeleteFromTrash(T entity);
        Task<bool> DeleteFromTrashRange(IEnumerable<T> lstEntity);
        Task SavechangesAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> preicate);
    }
}
