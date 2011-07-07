﻿
namespace GenPres.DataAccess.DataMapper
{
    public interface IDataMapper<T, TC>
    {
        void MapFromBoToDao(T bo, TC dao);
        void MapFromDaoToBo(TC dao, T bo);
    }
}