﻿using System;
using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;
using GenPres.Business.Aspect;

public enum StatusEnum
{
    New = 0,
    Dirty = 1
}

public interface IChangeTrackable
{
    StatusEnum State { get; set; }
}

namespace GenPres.Business.Domain
{
    
    public class User : IUser, IChangeTrackable
    {
        private StatusEnum _state = StatusEnum.New;

        public StatusEnum State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _id;
        private string _userName;
        private string _password;

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Id { get; set; }

        [LowerCase]
        [ChangeState]
        public string UserName { get; set; }

        [ChangeState]
        public string PassCrypt { get; set; }

        public bool IsNew { get; set; }

        public void OnCreate()
        {
            
        }

        public void OnNew()
        {
            
        }

        public void OnInitExisting()
        {
            
        }

        private static IUserRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IUserRepository>(); }
        }

        public static User NewUser()
        {
            return ObjectFactory<User>.New();
        }

        public static bool AuthenticateUser(string username, string password)
        {
            AvailableObject<IUser> userObjectAvailable = Repository.GetUserByUsername(username);
            if (userObjectAvailable.IsAvailable)
            {
                return (AuthenticationFunctions.MD5(password) == userObjectAvailable.Object.PassCrypt);
            }
            return false;
        }
    }
}

