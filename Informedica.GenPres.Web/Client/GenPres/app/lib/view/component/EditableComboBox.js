Ext.define('GenPres.lib.view.component.EditableComboBox',{
    extend:'Ext.form.field.ComboBox',
    alias:['widget.editablecombo', 'widget.editcombo'],
    
    trigger1Cls: Ext.baseCSSPrefix + 'form-clear-trigger',
    trigger2Cls: Ext.baseCSSPrefix + 'form-arrow-trigger',
    trigger3Cls: Ext.baseCSSPrefix + 'form-search-trigger',

    multiSelect: false,

    initComponent: function () {
        var me = this;
        me.addEvents('editoradd');
        //debugger;
        me.callParent(arguments);
    },

    onTrigger1Click:function(){
    	var me=this;

    	me.clearValue();
    },

    onTrigger2Click:function(){
        var me=this;
        if(!me.readOnly&&!me.disabled){
            if(me.isExpanded){me.collapse();}
            else{
                me.onFocus({});
                if(me.triggerAction==='all'){me.doQuery(me.allQuery,true);}
                else{me.doQuery(me.getRawValue());}
            }
            me.inputEl.focus();
        }
    },

    onTrigger3Click: function () {
        var me = this;
        me.fireEvent('editoradd', me);

        //Ext.MessageBox.alert('Edit or Add item');
    }
});