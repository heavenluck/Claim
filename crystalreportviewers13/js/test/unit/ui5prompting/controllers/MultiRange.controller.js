'use strict';

jQuery.sap.require("ui5prompting.utils.Formatter");

QUnit.module("MultiRangeController Tests");

var oController = sap.ui.controller("ui5prompting.controllers.MultiRange");

var START_PART = bobj.crv.fiori.range.START_PART;
var END_PART = bobj.crv.fiori.range.END_PART;
var RANGE_TOKEN = bobj.crv.fiori.range.RANGE_TOKEN;

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

    var formattedValue = ValueFormatter.FormatRangeForDisplay(range);
    range[RANGE_TOKEN] = {
        value: formattedValue,
        labels: [formattedValue]
    };

    return range;
}

function isEqual(oRange1, oRange2) {
    if (!oRange1[START_PART] && !oRange2[START_PART]) {
        return true;
    } else if ((oRange1[START_PART] && !oRange2[START_PART]) || (!oRange1[START_PART] && oRange2[START_PART])) {
        return false;
    }

    return oRange1[START_PART].value == oRange2[START_PART].value && oRange1[START_PART].inc == oRange2[START_PART].inc
        && oRange1[END_PART].value == oRange2[END_PART].value && oRange1[END_PART].inc == oRange2[END_PART].inc;
}

QUnit.test("Update Values", function (assert) {
    mockInitController(oController);
    resetModel();

    var range = [generateRangeValue("StringA", "StringB", true, true)];
    oController.updateValues(range);
    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(isEqual(range, modelRange), "Add value correct.");

    range = [generateRangeValue("StringC", "StringE", false, false)];
    oController.updateValues(range);
    modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(isEqual(range, modelRange), "Update value correct.");
});

QUnit.test("Update Multi Values", function (assert) {
    mockInitController(oController);
    resetModel();

    var range = {
        values: [
            generateRangeValue("StringA", "StringB", true, true),
            generateRangeValue("StringC", "StringE", false, false)
        ]
    };

    oController.updateValues(range);
    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(isEqual(range.values[0], modelRange[0]), "Update value correct.");
    assert.ok(isEqual(range.values[1], modelRange[1]), "Update value correct.");
});

QUnit.test("Update Multi Duplicated Values", function (assert) {
    mockInitController(oController);
    resetModel();

    var range = {
        values: [
            generateRangeValue("StringA", "StringB", true, true),
            generateRangeValue("StringA", "StringB", true, true)
        ]
    };

    oController.updateValues(range);
    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(modelRange.length == 1, "Duplicated value is deleted correct.");
});

QUnit.test("Remove Values", function (assert) {
    mockInitController(oController);
    resetModel();

    var range = {
        values: [
        generateRangeValue("StringA", "StringB", true, true),
        generateRangeValue("StringC", "StringE", false, false),
        generateRangeValue(undefined, "StringH", undefined, false)
        ]
    };

    oController.updateValues(range);

    oController.onRemoveValues([range.values[0][RANGE_TOKEN].value]);
    range.values.splice(0, 1);

    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(modelRange.length == 2, "Remove single value correct.");
    assert.ok(isEqual(range.values[0], modelRange[0]) && isEqual(range.values[1], modelRange[1]), "Remove single value validation correct.");

    oController.onRemoveValues([range.values[0][RANGE_TOKEN].value, range.values[1][RANGE_TOKEN].value]);
    range.values.splice(0, 2);

    modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(modelRange.length == 0, "Remove multi value correct.");
});
