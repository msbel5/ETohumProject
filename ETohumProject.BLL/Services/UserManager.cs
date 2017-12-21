using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ETohumProject.BLL.Interfaces;
using ETohumProject.DAL.Models;
using ETohumProject.DAL.Repositories;

namespace ETohumProject.BLL.Services
{
    public class UserManager:IManager<User>
    {
        private UserRepository _repository;

        public UserManager()
        {
            _repository = new UserRepository();
        }
        public User Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public bool Add(User entity)
        {
            return _repository.Add(entity);
        }

        public bool AddRange(IEnumerable<User> entities)
        {
            return _repository.AddRange(entities);
        }

        public bool Update(int entityId, User entity)
        {
            return _repository.Update(entityId, entity);
        }

        public bool Remove(User entity)
        {
            return _repository.Remove(entity);
        }

        public bool RemoveRange(IEnumerable<User> entities)
        {
            return _repository.RemoveRange(entities);
        }
    }
}
