'use strict';

sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "./BasePromptController"
], function (Controller, BaseInputController) {

    return BaseInputController.extend("ui5prompting.controllers.MultiInput", {

        updateValues: function(oEvent) {
            var valuesObj = this.getValueObj(oEvent);
            var curVals = this.getModelValues();
            for (var i = 0; i < valuesObj.length; i++) {
                var index = curVals.findIndex(function(x) { return x.value == valuesObj[i].value; });
                if (index == -1)
                    curVals.push(valuesObj[i]);
                else
                    curVals[index] = valuesObj[i];
            }
            this.setModelValues(curVals);
        },

        onRemoveValues: function(aRemovedIds) {
            for (var i = 0; i < aRemovedIds.length; i++) {
                var curVals = this.getModelValues();
                var index = curVals.findIndex(function(v){ return v.value == aRemovedIds[i].value; });
                if (index != -1) {
                    curVals[index].selected = false;
                    curVals.splice(index, 1);
                }
                this.setModelValues(curVals);
            }
            this.updateAll(this.typeValidation());
        },

        typeValidation: function() {
            if (!this.promptData.isOptionalPrompt && !this.promptData.allowNullValue && this.isNull()) {
                this.getView().getMultiInput().setValueStateText(sap.ui.getCore().getModel("ui5ResourceBundle").getText("ValueRequired"));
                return false;
            }
            return true;
        },

        getValueObj: function(oEvent) {
            var values = [];
            if (oEvent.value != undefined) {
                values.push({
                    value: oEvent.value,
                    labels: oEvent.labels
                });
            } else if (oEvent.values) {
                values = oEvent.values;
            }

            return values;
        },

        removeAll: function() {
            this.getModelValues().forEach(function(v) {
                v.selected = false;
            })
            this.setModelValues([]);
            this.updateAll(this.typeValidation());
        },

        setValues: function(values) {
            if (values && values.length > 0)
                this.updateValues({values : values});
            this.updateAll(this.typeValidation());
        }
    });

});


