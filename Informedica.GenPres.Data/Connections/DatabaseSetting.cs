﻿using Informedica.GenPres.Business.Domain.Databases;

namespace Informedica.GenPres.Data.Connections
{
    public class DatabaseSetting : IDatabaseSetting
    {
        #region Implementation of IDatabase

        public string Name { get; set; }
        public string GenPresConnectionString { get; set; }
        public string PdmsConnectionString { get; set; }
        public string GenFormWebservice { get; set; }
        public string Machine { get; set; }

        #endregion
    }
}