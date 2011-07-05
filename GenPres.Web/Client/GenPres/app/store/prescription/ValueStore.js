Ext.define('GenPres.store.prescription.ValueStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.valuestore',

    autoLoad:false,

    fields: [
        { name: 'Value', type: 'string' }
    ],

    proxy : {
        type:'direct',
        directFn : Prescription.GetShapes,
        extraParams:{
            generic: "",
            route : ""
        },
        paramOrder : ['generic', 'route']
    }
});