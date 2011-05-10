Ext.define('GenPres.view.main.MainViewLeft', {
    extend: 'Ext.Panel',
    layout:'vbox',
    region: 'west',
    xtype: 'panel',
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items:[
        {html:'<img src="Client/GenPres/style/images/logo.png" style="margin-top:22px;" />', height: 82},
        Ext.create('GenPres.view.main.PatientTree')
    ],
    width: 200,
    split: true,
    margins: '0 5 5 5'
});