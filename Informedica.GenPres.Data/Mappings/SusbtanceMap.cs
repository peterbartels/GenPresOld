﻿using FluentNHibernate.Mapping;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Data.Mappings
{
    public sealed class SubstanceMap : ClassMap<Substance>
    {
        public SubstanceMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(x => x.Name);
            //Component(x => x.ComponentConcentration).ColumnPrefix("ComponentConcentration_");
            //Component(x => x.DrugConcentration).ColumnPrefix("DrugConcentration_");
            //Component(x => x.Quantity).ColumnPrefix("Quantity_");
        }
    }
}
