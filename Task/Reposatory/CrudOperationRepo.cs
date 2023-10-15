﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task.Models;

namespace Task.Reposatory
{
    public class CrudOperationRepo<T>:Icrudoperation<T>where T: class
    {
        private readonly Context context;

        public CrudOperationRepo(Context context)
        {
            this.context = context;
        }
        public List<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void insert(T Entity)
        {
            context.Set<T>().Add(Entity);
        }
        public void update(T Entity)
        {
            context.Set<T>().Update(Entity);
        }
        public void Delete(int id)
        {
            T Entity = GetById(id);
            context.Set<T>().Remove(Entity);
        }

        public int save()
        {
            return context.SaveChanges();

        }

        
    }
}
