"use strict";

jQuery.sap.require("ui5prompting.inputs.BaseInput");

/** - Main module for testing- */
QUnit.module("BaseInput Tests");

ui5prompting.inputs.BaseInput.extend("BaseInputTesting", {
    getSubmitValue : function() {},
    getCRValidationValue : function() {},
    getCurrentValue : function() {}
});

function getDummySettings() {
    return {
        promptData : {
            fetchLOV: "false"
        },
        viewerName : undefined,
        options : undefined
    };
};

var validator = bobj.crv.params.Validator.getInstance;

function buildDummyValidator(result) {
    bobj.crv.params.Validator.getInstance = function() {
        return {
            validateValue : function() {
                return result;
            }
        };
    };
}

function restoreDummyValidator() {
    bobj.crv.params.Validator.getInstance = validator;
}


QUnit.test("Empty value", function (assert) {
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.setValue(undefined);
    baseInput.attachValueChange(function(result) {
        assert.ok(result.result == true, "Empty value passed validation");
    });
    baseInput.fireValueChange();

    baseInput.setValue("");
    baseInput.fireValueChange();
});

QUnit.test("Pass validation", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.setValue("anything");
    baseInput.attachValueChange(function(result) {
        assert.ok(result.result == true, "Validation passed");
    });
    baseInput.fireValueChange();
    assert.ok(baseInput.getValueState() == sap.ui.core.ValueState.Success, "Value status set correctly");
    restoreDummyValidator();
});

QUnit.test("Falied validation", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.ERROR);
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.setValue("anything");
    baseInput.attachValueChange(function(result) {
        assert.ok(result.result == false, "Validation failed");
    });
    baseInput.fireValueChange();
    assert.ok(baseInput.getValueState() == sap.ui.core.ValueState.Error, "Value status set correctly");
    restoreDummyValidator();
});

QUnit.test("Inclusive value", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.attachValueChange(function(result) {
        assert.ok(result.inc == false, "Inclusive value set correctly");
    });
    baseInput.setValue("anything");
    baseInput.setInclusive(false);
    baseInput.fireValueChange();
    restoreDummyValidator();
});

QUnit.test("Inclusive value(adjust after setValue)", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.attachValueChange(function(result) {
        assert.ok(result.inc == true, "Inclusive value should be true after setValue");
    });
    baseInput.setInclusive(undefined);
    baseInput.setValue("anything");
    baseInput.fireValueChange();
    restoreDummyValidator();
});

QUnit.test("Inclusive value(undefined)", function (assert) {
    buildDummyValidator(bobj.crv.params.Validator.ValueStatus.OK);
    var baseInput = new BaseInputTesting(getDummySettings());
    baseInput.attachValueChange(function(result) {
        assert.ok(result.value == undefined, "Value should be removed after set setInclusive to undefined");
    });
    baseInput.setValue("anything");
    baseInput.setInclusive(undefined);
    baseInput.fireValueChange();
    restoreDummyValidator();
});