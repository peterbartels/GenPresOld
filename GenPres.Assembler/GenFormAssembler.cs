﻿using System;
using GenPres.Business.WebService;
using StructureMap.Configuration.DSL;

namespace GenPres.Assembler
{
    public class GenFormAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();
            
            _registry.For<IGenFormService>().Use<GenFormService>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
