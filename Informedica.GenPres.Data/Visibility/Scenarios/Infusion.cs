﻿using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class Infusion : IScenario
    {
        public void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto)
        {
            if (_scenarioIsTrue(prescription))
            {
                _prescriptionDto.prescriptionFrequency.visible = true;
                _prescriptionDto.prescriptionDuration.visible = true;
                _prescriptionDto.adminQuantity.visible = true;
                _prescriptionDto.doseQuantity.visible = true;
                _prescriptionDto.doseTotal.visible = true;
                _prescriptionDto.adminTotal.visible = true;
                _prescriptionDto.doseRate.visible = false;
                _prescriptionDto.adminRate.visible = true;
            }
        }

        private bool _scenarioIsTrue(Prescription prescription)
        {
            return (
                !prescription.OnRequest &&
                prescription.Infusion &&
                !prescription.Continuous
            );
        }
    }
}
