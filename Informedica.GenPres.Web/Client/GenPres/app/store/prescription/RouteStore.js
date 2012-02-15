Ext.define('GenPres.store.prescription.RouteStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.routestore',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetRoutes,
        extraParams:{
            generic: "",
            shape:""
        },
        paramOrder : ['generic', 'shape']
    }
});