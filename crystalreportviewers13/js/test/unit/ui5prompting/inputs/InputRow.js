"use strict";

jQuery.sap.require("ui5prompting.inputs.InputRow");
jQuery.sap.require("ui5prompting.utils.Formatter");

QUnit.module("InputRow Tests");


function getDummySettings(dataType) {
    return {
        promptData: {
            valueDataType: dataType,
            fetchLOV: "false"
        },
        viewerName: undefined,
        options: {
            booleanFormat: {
                "false": "False",
                "true": "True"
            },
            numberFormat: {
                decimalSeperator: ".",
                groupSeperator: ","
            },
            dateFormat: "M/d/yyyy",
            dateTimeFormat: "M/d/yyyy H:mm:ss",
            timeFormat: "H:mm:ss"
        }
    };
};

var validator = bobj.crv.params.Validator.getInstance;

function buildDummyValidator(result) {
    bobj.crv.params.Validator.getInstance = function() {
        return {
            validateValue: function() {
                return result;
            }
        };
    };
}

function restoreDummyValidator() {
    bobj.crv.params.Validator.getInstance = validator;
}

QUnit.test("DiscreteInput UI add button state", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var inputRow = new DiscreteInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    var discreteInput = inputRow.getInput();
    discreteInput.attachValueChange(function(oEvent){
        var result = discreteInput.validate();
        inputRow.updateAddButton(oEvent.result);
        if (result.result == false || result.value == undefined) {
            assert.ok(inputRow.getAddButton().getEnabled() == false, "Add button is disabled");
        } else {
            assert.ok(inputRow.getAddButton().getEnabled() == true, "Add button is enabled");
        }
    });

    discreteInput.setValue(undefined);
    discreteInput.fireValueChange();

    discreteInput.setValue("");
    discreteInput.fireValueChange();

    discreteInput.setValue("anything");
    discreteInput.fireValueChange();

    restoreDummyValidator();
});

var START_PART = bobj.crv.fiori.range.START_PART;
var END_PART = bobj.crv.fiori.range.END_PART;
var CONDITION_VALUE = bobj.crv.fiori.range.CONDITION_VALUE;

function generateRangeValue(oStart, oEnd, bStartInc, bEndInc) {
    var range = {};
    if (oStart != undefined) {
        range[START_PART] = {
            value: oStart,
            labels: [oStart],
            inc: bStartInc
        };
    }
    if (oEnd != undefined) {
        range[END_PART] = {
            value: oEnd,
            labels: [oEnd],
            inc: bEndInc
        };
    }

    return range;
}

QUnit.test("RangeInput functions correctly empty value", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    rangeInput.getPartInput(START_PART).attachValueChange(function(oPartEvent){
        var result = rangeInput.validate();
        assert.ok(result.result == true, START_PART + " empty value passed validation");
    });
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent){
        var result = rangeInput.validate();
        assert.ok(result.result == true, END_PART + " empty value passed validation");
    });

    rangeInput.setValue(undefined);
    rangeInput.fireValueChange();

    rangeInput.setValue("");
    rangeInput.fireValueChange();
    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly string values in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == true, ValueFormatter.FormatRangeForDisplay(result) + " is in bound");
    });

    var ranges = [
        generateRangeValue("This is an ordinary string.", "This is an ordinary string.(longer)", true, true),
        generateRangeValue("~`!@#$%^&*()_-+=|\\{[}]:;\"\'<,>.?/1234567890", "~`!@#$%^&*()_-+=|\\{[}]:;\"\'<,>.?/1234567890(longer)", true, true),
        generateRangeValue("null", "null(longer)", true, true),
        generateRangeValue("stringA", "stringA", true, true),
        generateRangeValue("stringA", "stringB", true, true),
        generateRangeValue("stringA", "stringB", false, true),
        generateRangeValue("stringA", "stringB", true, false),
        generateRangeValue("stringA", "stringC", false, false),
        generateRangeValue("stringA", undefined, true, undefined),
        generateRangeValue("stringA", undefined, false, undefined),
        generateRangeValue(undefined, "stringA", undefined, true),
        generateRangeValue(undefined, "stringA", undefined, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly string values not in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == false, ValueFormatter.FormatRangeForDisplay(result) + " is not in bound");
    });

    var ranges = [
        generateRangeValue("stringA", "stringA", true, false),
        generateRangeValue("stringA", "stringA", false, true),
        generateRangeValue("stringA", "stringA", false, false),
        generateRangeValue("stringA", "stringB", false, false),
        generateRangeValue("This is an ordinary string.(longer)", "This is an ordinary string.", true, true),
        generateRangeValue("stringB", "stringA", true, true),
        generateRangeValue("stringB", "stringA", false, true),
        generateRangeValue("stringB", "stringA", true, false),
        generateRangeValue("stringB", "stringA", false, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly currency/number values in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.NUMBER));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == true, ValueFormatter.FormatRangeForDisplay(result) + " is in bound");
    });

    var ranges = [
        generateRangeValue(-1, 1, true, true),
        generateRangeValue(1.0, 1.5, true, true),
        generateRangeValue(-922337203685478, 922337203685478, true, true),
        generateRangeValue(1, 1, true, true),
        generateRangeValue(1, 2, true, true),
        generateRangeValue(1, 2, true, true),
        generateRangeValue(1, 2, false, true),
        generateRangeValue(1, undefined, true, undefined),
        generateRangeValue(1, undefined, false, undefined),
        generateRangeValue(undefined, 1, undefined, true),
        generateRangeValue(undefined, 1, undefined, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly currency/number values not in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.NUMBER));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == false, ValueFormatter.FormatRangeForDisplay(result) + " is not in bound");
    });

    var ranges = [
        generateRangeValue(1, 1, true, false),
        generateRangeValue(1, 1, false, true),
        generateRangeValue(1, 1, false, false),
        generateRangeValue(2, 1, true, true),
        generateRangeValue(2, 1, false, true),
        generateRangeValue(2, 1, true, false),
        generateRangeValue(2, 1, false, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly date/datetime/time values in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.DATE_TIME));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == true, ValueFormatter.FormatRangeForDisplay(result) + " is in bound");
    });

    var ranges = [
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(1999,11,12,2,33,11), true, true),
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(2008,5,12,1,22,33), true, true),
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(2008,5,12,1,22,33), false, true),
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(2008,5,12,1,22,33), true, false),
        generateRangeValue(new Date(1999,11,12,2,33,11), undefined, true, undefined),
        generateRangeValue(new Date(1999,11,12,2,33,11), undefined, false, undefined),
        generateRangeValue(undefined, new Date(1999,11,12,2,33,11), undefined, true),
        generateRangeValue(undefined, new Date(1999,11,12,2,33,11), undefined, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput functions correctly date/datetime/time values not in bound", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.DATE_TIME));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        assert.ok(result.result == false, ValueFormatter.FormatRangeForDisplay(result) + " is not in bound");
    });

    var ranges = [
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(1999,11,12,2,33,11), true, false),
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(1999,11,12,2,33,11), false, true),
        generateRangeValue(new Date(1999,11,12,2,33,11), new Date(1999,11,12,2,33,11), false, false),
        generateRangeValue(new Date(2008,5,12,1,22,33), new Date(1999,11,12,2,33,11), true, true),
        generateRangeValue(new Date(2008,5,12,1,22,33), new Date(1999,11,12,2,33,11), false, true),
        generateRangeValue(new Date(2008,5,12,1,22,33), new Date(1999,11,12,2,33,11), true, false),
        generateRangeValue(new Date(2008,5,12,1,22,33), new Date(1999,11,12,2,33,11), false, false)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }

    restoreDummyValidator();
});

QUnit.test("RangeInput UI add button state", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        var rangeLabel = "No Value";
        if (result[START_PART] || result[END_PART]) {
        	rangeLabel = ValueFormatter.FormatRangeForDisplay(result);
        }
        if (result.result == false || (!result[START_PART] && !result[END_PART])) {
            assert.ok(rangeInput.getAddButton().getEnabled() == false, rangeLabel + ": Add button is disable");
        } else {
            assert.ok(rangeInput.getAddButton().getEnabled() == true, rangeLabel + ": Add button is enabled");
        }
    });

    rangeInput.setValue(undefined);
    rangeInput.fireValueChange();

    rangeInput.setValue("");
    rangeInput.fireValueChange();

    var ranges = [
        generateRangeValue("stringB", "stringA", true, true),
        generateRangeValue("stringA", "stringB", true, true),
        generateRangeValue("stringA", undefined, true, undefined),
        generateRangeValue(undefined, "stringB", undefined, true)
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }
    restoreDummyValidator();
});

QUnit.test("RangeInput UI dropdown arrow state", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var rangeInput = new RangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    rangeInput.getPartInput(START_PART).attachValueChange(function(oPartEvent) {
        var result = rangeInput.validate();
        var rangeLabel = "No Value";
        if (result[START_PART] || result[END_PART]) {
        	rangeLabel = ValueFormatter.FormatRangeForDisplay(result);
        }
        if (!result[START_PART] || result[START_PART].value == undefined || result[START_PART].inc == undefined) {
        	assert.ok(rangeInput.getPartArrow(START_PART).getEnabled() == false, rangeLabel + ": Start arrow is disabled");
        } else {
        	assert.ok(rangeInput.getPartArrow(START_PART).getEnabled() == true, rangeLabel + ": Start arrow is enabled");
        }
    });
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent){
        var result = rangeInput.validate();
        var rangeLabel = "No Value";
        if (result[START_PART] || result[END_PART]) {
        	rangeLabel = ValueFormatter.FormatRangeForDisplay(result);
        }
        if (!result || !result[END_PART] || result[END_PART].value == undefined || result[END_PART].inc == undefined) {
        	assert.ok(rangeInput.getPartArrow(END_PART).getEnabled() == false, rangeLabel + ": End arrow is disabled");
        } else {
        	assert.ok(rangeInput.getPartArrow(END_PART).getEnabled() == true, rangeLabel + ": End arrow is enabled");
        }
    });

    rangeInput.setValue(undefined);
    rangeInput.fireValueChange();

    rangeInput.setValue("");
    rangeInput.fireValueChange();

    var ranges = [
        generateRangeValue("stringA", "stringB", true, true),
        generateRangeValue("stringA", undefined, true, undefined),
        generateRangeValue(undefined, "stringB", undefined, true),
        generateRangeValue("stringB", "stringA", true, true),
    ];

    for (var i = 0; i < ranges.length; i++) {
        rangeInput.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }
    restoreDummyValidator();
});

function generateCondtionRangeValue(oStart, oEnd, bStartInc, bEndInc, conditionVal) {
    var range = generateRangeValue(oStart, oEnd, bStartInc, bEndInc);
    range[CONDITION_VALUE] = conditionVal;

    return range;
}

QUnit.test("ConditonRangeInput functions correctly empty value", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var inputRow = new ConditionRangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    var rangeInput = inputRow.getRangeInput();
    rangeInput.getPartInput(START_PART).attachValueChange(function(oPartEvent){
        var result = inputRow.validate();
        assert.ok(result.result == true, START_PART + " empty value passed validation");
    });
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent){
        oPartEvent.partType = END_PART;
        var result = inputRow.validate(oPartEvent);
        assert.ok(result.result == true, END_PART + " empty value passed validation");
    });

    inputRow.setValue(undefined);
    rangeInput.fireValueChange();

    inputRow.setValue("");
    rangeInput.fireValueChange();
    restoreDummyValidator();
});

QUnit.test("ConditionRangeInput UI add button state", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var inputRow = new ConditionRangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    var rangeInput = inputRow.getRangeInput();
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = inputRow.validate();
        var isBetween = (result[CONDITION_VALUE] == 1 || result[CONDITION_VALUE] == 8);
        var rangeLabel = "No Value";
        if (result[START_PART] || result[END_PART]) {
        	rangeLabel = ValueFormatter.FormatRangeForDisplay(result);
        }
        if (result.result == false
            || (!isBetween && !result[START_PART])
            || (isBetween && (!result[START_PART] || !result[END_PART]))) {
            assert.ok(rangeInput.getAddButton().getEnabled() == false, rangeLabel + ": Add button is disable");
        } else {
            assert.ok(rangeInput.getAddButton().getEnabled() == true, rangeLabel + ": Add button is enabled");
        }
    });

    inputRow.setValue(undefined);
    rangeInput.fireValueChange();

    var ranges = [
        generateCondtionRangeValue("stringA", undefined, true, undefined, 0),
        generateCondtionRangeValue("stringA", "stringB", true, true, 1),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 1),
        generateCondtionRangeValue(undefined, "stringB", undefined, true, 1),
        generateCondtionRangeValue("stringB", "stringA", true, true, 1)
    ];

    for (var i = 0; i < ranges.length; i++) {
        inputRow.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }
    restoreDummyValidator();
});

QUnit.test("ConditionRangeInput UI operator select", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var inputRow = new ConditionRangeInput(getDummySettings(bobj.crv.params.DataTypes.STRING));
    var rangeInput = inputRow.getRangeInput();
    rangeInput.getPartInput(END_PART).attachValueChange(function(oPartEvent) {
        var result = inputRow.validate();
        var isBetween = (result[CONDITION_VALUE] == 1 || result[CONDITION_VALUE] == 8);
        var rangeLabel = "No Value";
        if (result[START_PART] || result[END_PART]) {
        	rangeLabel = ValueFormatter.FormatRangeForDisplay(result);
        }
        assert.ok(inputRow.getOperatorSelect().getSelectedKey() == result[CONDITION_VALUE], rangeLabel + ": Operator " + inputRow.getOperatorSelect().getSelectedItem().getText() + " is selected correctly");
        //assert.ok(rangeInput.getRangeIcon().getVisible() == isBetween, rangeLabel + ": RangeIcon visible is " + isBetween);
        assert.ok(rangeInput.getPartInput(END_PART).getVisible() == isBetween, rangeLabel + ": EndInput visible is " + isBetween);
    });

    inputRow.setValue(undefined);
    rangeInput.fireValueChange();

    var ranges = [
        generateCondtionRangeValue("stringA", undefined, true, undefined, 0),
        generateCondtionRangeValue("stringA", "stringB", true, true, 1),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 2),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 3),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 4),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 5),
        generateCondtionRangeValue("stringA", undefined, true, undefined, 7),
        generateCondtionRangeValue("stringA", "stringB", true, true, 8)
    ];

    for (var i = 0; i < ranges.length; i++) {
        inputRow.setValue(ranges[i]);
        rangeInput.fireValueChange();
    }
    restoreDummyValidator();
});