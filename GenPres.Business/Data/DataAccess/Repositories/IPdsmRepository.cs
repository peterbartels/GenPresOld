﻿using System.Collections.Generic;
using GenPres.Business.Domain;
using GenPres.Business.Domain.PatientDomain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPdsmRepository
    {
        List<IPatient> GetPatientsByLogicalUnitId(int logicalUnitId);
    }
}
