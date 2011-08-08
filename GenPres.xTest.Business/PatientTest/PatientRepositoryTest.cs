﻿using GenPres.Business.Data.IRepositories;
using GenPres.Data.Repositories;
using GenPres.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.PatientTest
{
    [TestClass]
    public class PatientRepositoryTest : BaseGenPresTest
    {
        private static void InitializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            StructureMap.ObjectFactory.Configure(x => x.For<IPdsmRepository>().Use(repository));
            return;
        }

        [Isolated]
        [TestMethod]
        public void PatientServiceGetPatientsByLogicalUnitIdCallsRepository()
        {
            InitializePatientRepositoryTest();
            var patientRepository = StructureMap.ObjectFactory.GetInstance<IPdsmRepository>();
            PatientService.GetPatientsByLogicalUnit(1);
            Isolate.Verify.WasCalledWithExactArguments(() => patientRepository.GetPatientsByLogicalUnitId(1));
        }
    }
}