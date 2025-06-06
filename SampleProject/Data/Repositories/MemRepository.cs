using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister]
    public class MemRepository<T> : IMemRepository<T> where T : IdObject
    {
        protected readonly List<T> _mem;

        public MemRepository(List<T> mem)
        {
            _mem = mem;
        }

        public void Save(T entity)
        {
            _mem.Add(entity);
        }

        public void Delete(T entity)
        {
            _mem.Remove(entity);
        }

        public T Get(Guid id)
        {
            return IdObject.GetById(_mem,id);
        }

    }
}