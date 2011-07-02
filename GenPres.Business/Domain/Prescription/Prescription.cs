﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;

namespace GenPres.Business.Domain.Prescription
{
    public class Prescription : IPrescription
    {
        #region Private Fields

        private DateTime _startDate;

        private DateTime _endDate;

        private DateTime _creationDate;

        private IDrug _drug;

        #endregion

        #region Public Properties

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        public IDrug Drug
        {
            get { return _drug; }
            set { _drug = value; }
        }

        #endregion

        private static IPrescriptionRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IPrescriptionRepository>(); }
        }

        public static Prescription NewPrescription()
        {
            Prescription prescription = new Prescription();
            prescription.CreationDate = DateTime.Now;
            prescription.Drug = Domain.Prescription.Drug.NewDrug();
            return prescription;
        }

        public static IPrescription[] GetPrescriptions()
        {
            return Repository.GetPrescriptions();
        }

        public void Save()
        {
            Repository.SavePrescription(this);
        }
    }
}
