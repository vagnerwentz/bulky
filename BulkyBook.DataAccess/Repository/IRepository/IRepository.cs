using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T entity);
    IEnumerable<T> GetAll();
    void RemoveRange(IEnumerable<T> entity);
    T GetFirstOrDefault(Expression<Func<T, bool>> filter);
}