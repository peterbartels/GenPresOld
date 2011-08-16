﻿using GenPres.Business.Data.IRepositories;

namespace GenPres.Business.Domain.Patients
{
    public class LogicalUnit 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private static ILogicalUnitRepository Repository
        {
            get { return StructureMap.ObjectFactory.GetInstance<ILogicalUnitRepository>(); }
        }

        public static LogicalUnit NewLogicalUnit()
        {
            return new LogicalUnit();
        }

        public static LogicalUnit[] GetLogicalUnits()
        {
            return Repository.GetLogicalUnits();
        }
    }
}
