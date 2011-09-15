﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Business.Util;

namespace GenPres.Business.Calculation
{

    public class PrescriptionCalculator
    {
        public decimal[] SubstanceIncrements = new decimal[1]{0.24m};
        public decimal[] ComponentIncrements = new decimal[1] { 1 };

        private readonly List<ICalculationCombination> _combinations = new List<ICalculationCombination>();
        private readonly Prescription _prescription;

        public static List<string> PropertySequence()
        {
            var prescription = Prescription.NewPrescription();
            return new List<string>()
            {
                PropertyHelper.MemberName(() => prescription.Doses[0].Total),
                PropertyHelper.MemberName(() => prescription.Doses[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Substances[0].DrugConcentration),
                PropertyHelper.MemberName(() => prescription.Duration),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Substances[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Doses[0].Rate),
                PropertyHelper.MemberName(() => prescription.Drug.Quantity),
                PropertyHelper.MemberName(() => prescription.Total),
                PropertyHelper.MemberName(() => prescription.Quantity),
                PropertyHelper.MemberName(() => prescription.Rate),
            };
        }

        public PrescriptionCalculator(Prescription prescription)
        {
            _prescription = prescription;
            SetIncrements(_prescription);
        }

        public void Start()
        {
            _combinations.Add(new MultiplierCombination(
                _prescription,
                () => _prescription.Total, () => _prescription.Frequency, () => _prescription.Quantity
            ));

            _combinations.Add(new MultiplierCombination(
                _prescription,
                () => _prescription.Doses[0].Total, () => _prescription.Frequency, () => _prescription.Doses[0].Quantity
            ));
            Execute();

            for (int i = 0; i < _combinations.Count; i++) _combinations[i].Finish();
        }

        public void AddCalculation(ICalculationCombination combi)
        {
            _combinations.Add(combi);
        }

        private void CorrectPropertyIncrements(int index)
        {
            
        }

        public void Execute()
        {
            for (int i = 0; i < _combinations.Count; i++)
                _combinations[i].Calculate();   
        }

        public void ConvertCombinationsValuesToArray()
        {
            for (int i = 0; i < _combinations.Count; i++)
            {
                _combinations[i].ConvertCombinationsValuesToArray();    
            }
        }

        public void Finish()
        {
            _combinations[0].Finish();
        }

        public static void Calculate(Prescription prescription)
        {
            var pc = new PrescriptionCalculator(prescription);
            pc.Start();
        }

        private static void SetIncrements(Prescription prescription)
        {
            var componentInc = new decimal[] { 1 };
            var substanceInc = new decimal[] { 0.24m };
            var freqInc = new decimal[] { 1 };

            SetIncrementValues(prescription, () => prescription.Frequency, freqInc, true);
            SetIncrementValues(prescription, () => prescription.Quantity, componentInc, true);
            SetIncrementValues(prescription, () => prescription.Total, componentInc, true);

            SetIncrementValues(prescription, () => prescription.Doses[0].Quantity, substanceInc, true);
            SetIncrementValues(prescription, () => prescription.Doses[0].Total, substanceInc, true);
        }

        private static void SetIncrementValues(Prescription p, Expression<Func<UnitValue>> property, decimal[] values, bool incrementStepping)
        {
            UnitValue unitValue = PropertyHelper.GetUnitValue(property);
            unitValue.Factor.IncrementSizes = values;
            unitValue.Factor.IncrementStepping = incrementStepping;
        }

        public void CheckStates()
        {
            for (int i = 0; i < _combinations.Count; i++)
            {
                if (_combinations[i].GetUserCount() > 0)
                {
                    
                }
            }
        }
    } 
}
