/* ===================================================== Helper Functions/Objects ===================================================== */

function CreateInput(oSettings) {
    // rename
    var type = oSettings.promptData.valueDataType;
    if (oSettings.placeholder == undefined) {
        oSettings.placeholder = sap.ui.getCore().getModel("ui5ResourceBundle").getText("hintDiscrete");
    }
    switch (type) {
    case bobj.crv.params.DataTypes.STRING:
        return new StringInput(oSettings);
    case bobj.crv.params.DataTypes.CURRENCY:
    case bobj.crv.params.DataTypes.NUMBER:
        return new NumberInput(oSettings);
    case bobj.crv.params.DataTypes.BOOLEAN:
        return new BooleanInput(oSettings);
    case bobj.crv.params.DataTypes.DATE_TIME:
        return new DateTimeInput(oSettings);
    case bobj.crv.params.DataTypes.DATE:
        return new DateInput(oSettings);
    case bobj.crv.params.DataTypes.TIME:
        return new TimeInput(oSettings);
    default:
        // TODO: Logging, how are we going to handle?
        console.error("Tried to create unknown component type: " + type);
    }
};

/* ================================================== Derived Base Components ===================================================== */
'use strict';
jQuery.sap.require("ui5prompting.inputs.BaseInput");

// StringInput
ui5prompting.inputs.BaseInput.extend("StringInput", {
    renderer: "sap.m.InputRenderer",

    constructor: function(oSettings) {
        ui5prompting.inputs.BaseInput.call(this, oSettings);
    },

    getSubmitValue: function() {
        var sValue = this.getValue();
        if (sValue != "")
            return sValue;
    },

    getCurrentValue: function() {
        return this.getValue();
    },

    getCRValidationValue: function() {
        return this.getCurrentValue();
    }

});

ui5prompting.inputs.BaseInput.extend("NumberInput", {

    renderer: "sap.m.InputRenderer",

    constructor: function(oSettings) {
        oSettings.type = "Number";
        ui5prompting.inputs.BaseInput.call(this, oSettings);
    },

    getSubmitValue: function() {
        var newValue = this.getCurrentValue();
        if (!isNaN(newValue))
           return newValue;
    },

    getCurrentValue: function() {
        return parseFloat(this.getValue());
    },

    getCRValidationValue: function() {
        return this.getCurrentValue();
    }

});

//BooleanInput
ui5prompting.inputs.BaseSelectInput.extend("BooleanInput", {

    renderer: "sap.m.SelectRenderer",

    constructor: function(oSettings) {
        oSettings.width = "100%";
        oSettings.allowShowValueHelp = false;
        oSettings.forceSelection = false;
        ui5prompting.inputs.BaseSelectInput.call(this, oSettings);

        this.boolOpts = oSettings.options.booleanFormat;
        for (var propName in this.boolOpts) {
            this.addItem(new sap.ui.core.Item( {text: this.boolOpts[propName], key: propName}));
        }
    },

    getSubmitValue: function() {
        var sKey = this.getCurrentValue();
        if (sKey)
            return sKey == "true";
    },

    getCurrentValue: function() {
        var selItem = this.getSelectedItem();
        if (selItem)
            return selItem.getKey();
    },

    getCRValidationValue: function() {
        return this.getSubmitValue();
    },

    setValue: function(bool) {
        if ((typeof bool == "string" && (bool.toLowerCase() == "true" || bool.toLowerCase() == "false"))
                ||  bool == "boolean") {
            this.setSelectedKey(bool);
        }
    }
});

// Redefine constants just to shorten the expressions. These will not be exported (see 'use strict')
var DATE_PREFIX = bobj.crv.fiori.date.DATE_PREFIX;
var DATETIME_PREFIX = bobj.crv.fiori.date.DATETIME_PREFIX;
var TIME_PREFIX = bobj.crv.fiori.date.TIME_PREFIX;
var DATE_SUFFIX = bobj.crv.fiori.date.DATE_SUFFIX;
var DATE_SEP = bobj.crv.fiori.date.DATE_SEP;

// DateInput
ui5prompting.inputs.BaseDateInput.extend("DateInput", {

    constructor: function(oSettings) {
        oSettings.displayFormat = oSettings.options.dateFormat;
        ui5prompting.inputs.BaseDateInput.call(this, oSettings);
    },

    getSubmitValue: function() {
        var oDate = this.getCurrentValue();
        if (oDate) {
            return  DATE_PREFIX + oDate.getFullYear() + DATE_SEP + (oDate.getMonth() + 1) + DATE_SEP + oDate.getDate() + DATE_SUFFIX;
        }
    },

    parseSubmitValue: function(sDate) {
        var numList = sDate.substring(DATE_PREFIX.length, sDate.length - DATE_SUFFIX.length).split(DATE_SEP);
        if (numList.length == 3)
            return new Date(numList[0], numList[1]-1, numList[2]);
    }

});

// DateTimeInput
ui5prompting.inputs.BaseDateTimeInput.extend("DateTimeInput", {

    constructor: function(oSettings) {
        oSettings.displayFormat = oSettings.options.dateTimeFormat;
        ui5prompting.inputs.BaseDateTimeInput.call(this, oSettings);
    },

    getSubmitValue: function() {
        var oDateTime = this.getCurrentValue();
        if (oDateTime) {
            return DATETIME_PREFIX + oDateTime.getFullYear() + DATE_SEP + (oDateTime.getMonth() + 1) + DATE_SEP + oDateTime.getDate() + DATE_SEP +
                    oDateTime.getHours() + DATE_SEP + oDateTime.getMinutes() + DATE_SEP + oDateTime.getSeconds() + DATE_SUFFIX;
        }
    },

    parseSubmitValue: function(sDate) {
        var numList = sDate.substring(DATETIME_PREFIX.length, sDate.length - DATE_SUFFIX.length).split(DATE_SEP);
        if (numList.length == 6)
            return new Date(numList[0], numList[1]-1, numList[2], numList[3], numList[4], numList[5]);
    }

});

// TimeInput
ui5prompting.inputs.BaseTimeInput.extend("TimeInput", {

    constructor: function(oSettings) {
        if (oSettings.width == undefined) {
            oSettings.width = "100%";
        }
        oSettings.displayFormat = oSettings.options.timeFormat;
        ui5prompting.inputs.BaseTimeInput.call(this, oSettings);
    },

    getSubmitValue: function() {
        var oTime = this.getCurrentValue();
        if (oTime) {
            return TIME_PREFIX + oTime.getHours() + DATE_SEP + oTime.getMinutes() + DATE_SEP + oTime.getSeconds() + DATE_SUFFIX;
        }
    },

    parseSubmitValue: function(sDate) {
        var numList = sDate.substring(TIME_PREFIX.length, sDate.length - DATE_SUFFIX.length).split(DATE_SEP);
        if (numList.length == 3)
            return new Date(0, 0, 0, numList[0], numList[1], numList[2]);
    }

});