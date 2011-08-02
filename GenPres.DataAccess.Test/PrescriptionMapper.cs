﻿using System.Collections.Generic;
using GenPres.Business.Domain;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.DataMapper;
using GenPres.Database;

namespace GenPres.DataAccess.Test
{
    public class PrescriptionMapper : DataMapper<PrescriptionBo, Prescription>
    {
        public PrescriptionMapper()
            : base(DalServiceProvider.Instance.Resolve<IDataContextManager>())
        {
            
        }

        public PrescriptionMapper(IDataContextManager manager)
            : base(manager)
        {

        }

        public void MapChilds(bool toBo, PrescriptionBo _bo, Prescription _dao)
        {
            var childMapper = new ChildMapper<PrescriptionBo, Prescription>(_bo, _dao, ContextManager);
            childMapper.Map(x => x.Drug, y => y.Drug, typeof(DrugMapper), toBo);
        }

        public override Prescription MapFromBoToDao(PrescriptionBo rootBo, Prescription dao)
        {
            MapChilds(false, rootBo, dao);
            return dao;
        }

        public override PrescriptionBo MapFromDaoToBo(Prescription dao, PrescriptionBo rootBo)
        {
            MapChilds(true, rootBo, dao);
            return rootBo;
        }
    }
    
    public class DrugBo : ISavable
    {
        public string Generic { get; set; }
        public List<ComponentBo> Components { get; set; }

        public bool IsNew { get { return (Id == 0); } }

        public void OnCreate()
        {

        }

        public void OnNew()
        {

        }

        public void OnInitExisting()
        {

        }

        public void Save()
        {

        }

        public int Id { get; set; }
    }

    public class ComponentBo : ISavable
    {

        public string Name;

        public bool IsNew { get { return (Id == 0); } }

        public void OnCreate()
        {

        }

        public void OnNew()
        {

        }

        public void OnInitExisting()
        {

        }

        public void Save()
        {

        }

        public int Id { get; set; }
    }
    public class ComponentMapper : DataMapper<ComponentBo, Component>
    {
        public ComponentMapper()
            : base(DalServiceProvider.Instance.Resolve<IDataContextManager>())
        {
            
        }

        public ComponentMapper(IDataContextManager manager)
            : base(manager)
        {

        }

        public override Component MapFromBoToDao(ComponentBo _bo, Component _dao)
        {
            _dao.ComponentName = _bo.Name;
            return _dao;
        }

        public override ComponentBo MapFromDaoToBo(Component _dao, ComponentBo _bo)
        {
            _bo.Name = _dao.ComponentName;
            return _bo;
        }
    }
    public class DrugMapper : DataMapper<DrugBo, Drug>
    {
        public DrugMapper()
            : base(DalServiceProvider.Instance.Resolve<IDataContextManager>())
        {

        }

        public DrugMapper(IDataContextManager manager) : base (manager)
        {

        }

        private void MapChilds(bool toBo, DrugBo _bo, Drug _dao)
        {
            var childMapper = new ChildMapper<DrugBo, Drug>(_bo, _dao, ContextManager);
            childMapper.MapCollection(x => x.Components, y => y.Components, typeof(ComponentMapper), toBo);
        }

        public override Drug MapFromBoToDao(DrugBo _bo, Drug _dao)
        {
            _dao.Name = _bo.Generic;
            MapChilds(false, _bo, _dao);
            return _dao;
        }

        public override DrugBo MapFromDaoToBo(Drug _dao, DrugBo _bo)
        {
            _bo.Generic = _dao.Name;
            MapChilds(true, _bo, _dao);
            return _bo;
        }
    }
}
