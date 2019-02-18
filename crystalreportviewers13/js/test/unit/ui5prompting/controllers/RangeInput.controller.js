'use strict';

QUnit.module("RangeInputController Tests");

var oController = sap.ui.controller("ui5prompting.controllers.RangeInput");

var START_PART = bobj.crv.fiori.range.START_PART;
var END_PART = bobj.crv.fiori.range.END_PART;

function generateRangeValue(oStart, oEnd, bStartInc, bEndInc) {
    var range = {};
    if (oStart != undefined) {
        range[START_PART] = {
            value: oStart,
            inc: bStartInc
        };
    }
    if (oEnd != undefined) {
        range[END_PART] = {
            value: oEnd,
            inc: bEndInc
        };
    }

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

    var range = generateRangeValue("StringA", "StringB", true, true);
    oController.updateValues(range);
    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0];
    assert.ok(isEqual(range, modelRange), "Add value correct.");

    range = generateRangeValue("StringC", "StringE", false, false);
    oController.updateValues(range);
    modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0];
    assert.ok(isEqual(range, modelRange), "Update value correct.");
});

QUnit.test("Set Values", function (assert) {
    mockInitController(oController);
    resetModel();
    oMockInput.setInputOK = function() {};

    var range = generateRangeValue("StringA", "StringB", true, true);
    oController.setValues([range]);
    var modelRange = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0];
    assert.ok(isEqual(range, modelRange), "Set value correct.");

    oMockInput.setInputOK = undefined;
});

QUnit.test("Remove All", function (assert) {
    mockInitController(oController);
    resetModel();
    oMockInput.setInputOK = function() {};

    var range = generateRangeValue("StringA", "StringB", true, true);
    oController.setValues([range]);

    oController.promptData.isOptionalPrompt = false;
    oMockInput.setInputFail = function() {
        assert.ok(true, "Validation Optional correct");
    };

    oController.removeAll();
    var values = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok((values.length == 0), "Remove All values correct.");

    oMockInput.setInputFail = undefined;
    oMockInput.setInputOK = undefined;
});
