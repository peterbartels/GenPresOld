﻿using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Data.Repositories;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class PatientAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<ILogicalUnitRepository>().Use<LogicalUnitRepository>();
            _registry.For<IPdsmRepository>().Use<PdmsRepository>();
            _registry.For<IPatientRepository>().Use<PatientRepository>();
            
            _hasBeenCalled = true;
            return _registry;
        }
        
    }
}
