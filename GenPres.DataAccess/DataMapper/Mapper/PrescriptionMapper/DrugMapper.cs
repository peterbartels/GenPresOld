﻿using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper
{
    public class DrugMapper : DataMapper<Drug, Database.Drug>
    {
        public DrugMapper()
            : base(new GenPresDataContextManager())
        {
        }

        public DrugMapper(IDataContextManager context)
            : base(context)
        {
        }

        public override Database.Drug MapFromBoToDao(Drug _bo, Database.Drug _dao)
        {
            _dao.Name = _bo.Generic;
            _dao.Route = _bo.Route;
            _dao.Shape = _bo.Shape;
            return _dao;
        }

        public override Drug MapFromDaoToBo(Database.Drug _dao, Drug _bo)
        {
            _bo.Generic = _dao.Name;
            _bo.Route = _dao.Route;
            _bo.Shape = _dao.Shape;
            return _bo;
        }
    }
}
