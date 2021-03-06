﻿using System;
using System.Data;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.DAO.Mapper.Patients
{
    public class PdmsPatientMapper
    {
        public Patient MapDaoToBusinessObject(object daoObj, Patient patientBo)
        {
            var patientDao = (DataRow)daoObj;
            patientBo.LastName = patientDao["LastName"].ToString();
            patientBo.FirstName = patientDao["FirstName"].ToString();
            patientBo.Pid = patientDao["HospitalNumber"].ToString();
            patientBo.LogicalUnitId = int.Parse(patientDao["LOGICALUNITID"].ToString());

            decimal length = 0;
            decimal weight = 0;
            decimal.TryParse(patientDao["Length"].ToString(), out length);
            decimal.TryParse(patientDao["Weight"].ToString(), out weight);
            patientBo.Weight.BaseValue = weight;
            patientBo.Height.BaseValue = length;

            var addmissionDate = (DateTime?)patientDao["AddmissionDate"];

            if (addmissionDate != null)
            {
                var span = DateTime.Now.Subtract(addmissionDate.Value);
                patientBo.DaysRegistered = span.Days;
                patientBo.RegisterDate = addmissionDate.Value;
            }

            patientBo.Unit = patientDao["LogicalUnitName"].ToString();
            patientBo.Bed = patientDao["BedName"].ToString();

            patientBo.Gender = patientDao["GenderID"].ToString();
            

            return patientBo;
        }
    }
}
