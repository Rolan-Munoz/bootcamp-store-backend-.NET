using System;
using System.Linq.Expressions;
using System.Reflection;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(StoreContext storeContext)
        {
            _context = storeContext;
            _dbSet = _context.Set<T>();
        }

        

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList<T>();
        }

        public virtual T GetById(long id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new ElementNotFoundException();
            }

            return entity;
        }

        public virtual T Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(long id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
            {
                throw new ElementNotFoundException();
            }

           _dbSet.Remove(entity);
            _context.SaveChanges();
        }


        protected virtual IQueryable<T> ApplySortOrder(IQueryable<T> entities, string sortOrder)
        {
            var orderByParameters = sortOrder.Split(',');
            var orderByAttribute = Char.ToUpper(orderByParameters[0][0]) + orderByParameters[0][1..];
            var orderByDireccion = orderByParameters.Length > 1 ? orderByParameters[1] : "asc";

            var propertyInfo = typeof(T).GetProperty(orderByAttribute, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

           if (propertyInfo != null)
            {
                var parameter = Expression.Parameter(typeof(Item), "x");
                var property = Expression.Property(parameter, propertyInfo);

                if (propertyInfo.PropertyType.IsValueType)
                {
                    var orderByExpression = Expression.Lambda<Func<T, dynamic>>(Expression.Convert(property, typeof(object)), parameter);

                    entities = orderByDireccion.Equals("asc", StringComparison.OrdinalIgnoreCase)
                        ? entities.OrderBy(orderByExpression)
                        : entities.OrderByDescending(orderByExpression);
                }
                else
                {
                    var orderByExpression = Expression.Lambda<Func<T, object>>(property, parameter);

                    entities = orderByDireccion.Equals("asc", StringComparison.OrdinalIgnoreCase)
                        ? entities.OrderBy(orderByExpression)
                        : entities.OrderByDescending(orderByExpression);
                }
            }
            return entities;
        }



    }
}

