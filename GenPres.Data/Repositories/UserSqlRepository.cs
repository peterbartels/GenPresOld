﻿using System;
using GenPres.Business;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Users;

namespace GenPres.Data.Repositories
{
    public class UserRepository : NHibernateRepository<User, Guid>, IUserRepository
    {

        public UserRepository()
            : base(SessionManager.SessionFactory)
        {
            
        }

        public User GetByName(string name)
        {
            var user = Business.Domain.Users.User.NewUser();
            user.UserName = name;
            return user;
        }

        public bool GetUserByUsername(string user, string password)
        {
            var foundUser = FindSingle(i => i.UserName == user && i.PassCrypt == AuthenticationFunctions.MD5(password));
            return (foundUser != null);
        }

        public void Save(User user)
        {
            base.Add(user);
        }
    }
}
