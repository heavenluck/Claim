"use strict";

jQuery.sap.require("ui5prompting.inputs.DerivedInputs");
jQuery.sap.require("ui5prompting.utils.Formatter");

QUnit.module("DerivedInput Tests");

function getDummySettings() {
    return {
        promptData : {
            fetchLOV: "false"
        },
        viewerName : undefined,
        options : {
            booleanFormat : {
                "false" : "False",
                "true" : "True",
            }
        }
    };
};

QUnit.test("StringInput functions correctly format values", function (assert) {
    var strInput = new StringInput(getDummySettings());

    // Control the value stored in the input
    var testStr = "thisisatesT98";
    strInput.setValue(testStr);
    assert.strictEqual(strInput.getCurrentValue(), testStr, "Current value is properly formatted");
    assert.strictEqual(strInput.getCRValidationValue(), testStr, "Validation value is properly formatted");
    assert.strictEqual(strInput.getSubmitValue(), testStr, "Submit value is properly formatted");

    var testStr2 = "";
    strInput.setValue(testStr2);
    assert.strictEqual(strInput.getSubmitValue(), undefined, "Empty string counts as undefined prompt value");
});

QUnit.test("NumberInput functions correctly format values", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var numInput = new NumberInput(getDummySettings());

    // Control the value stored in the input
    var testNum = 45;
    numInput.setValue(testNum);
    assert.strictEqual(numInput.getCurrentValue(), testNum, "Current value is properly formatted");
    assert.strictEqual(numInput.getCRValidationValue(), testNum, "Validation value is properly formatted");
    assert.strictEqual(numInput.getSubmitValue(), testNum, "Submit value is properly formatted");

    numInput.setValue(NaN);
    assert.strictEqual(numInput.getSubmitValue(), undefined, "NaN counts as undefined prompt value");
});

QUnit.test("BooleanInput functions correctly format values", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var boolInput = new BooleanInput(getDummySettings());

    // Control the value stored in the input
    var valKey = "true";
    boolInput.setValue(valKey);
    assert.strictEqual(boolInput.getCurrentValue(), valKey, "Current value is properly formatted");
    assert.strictEqual(boolInput.getCRValidationValue(), true, "Validation value is properly formatted");
    assert.strictEqual(boolInput.getSubmitValue(), true, "Submit value is properly formatted");

    boolInput.setSelectedKey(undefined);
    boolInput.setValue(undefined);
    assert.strictEqual(boolInput.getSubmitValue(), undefined, "undefined counts as undefined prompt value");
});

QUnit.test("DateInput setValue function correctly sets date", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var dateInput = new DateInput(getDummySettings());

    // Control the value stored in the input
    var valDate = new Date(1999,11,12); //Month is 0-11
    dateInput.setValue(valDate);

    assert.strictEqual(dateInput.getCurrentValue(), valDate, "Date object was set correctly");

    var strDate = "Date(1999,12,12)";
    dateInput.setValue(strDate);

    assert.strictEqual(dateInput.getCurrentValue(), valDate, "Date string was set correctly");

    // Control the value stored in the input
    var valDate = new Date(1997,0,9); //Month is 0-11
    dateInput.setValue(valDate);

    assert.strictEqual(dateInput.getSubmitValue(), "Date(1997,1,9)", "DateInput formats submit value correctly");

});

QUnit.test("DateTimeInput setValue function correctly sets date", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var dateTimeInput = new DateTimeInput(getDummySettings());

    // Control the value stored in the input
    var valDate = new Date(1999,11,12,1,22,33); //Month is 0-11
    dateTimeInput.setValue(valDate);

    assert.strictEqual(dateTimeInput.getCurrentValue(), valDate, "DateTime object was set correctly");

    var strDate = "DateTime(1999,12,12,1,22,33)";
    dateTimeInput.setValue(strDate);

    assert.strictEqual(dateTimeInput.getCurrentValue(), valDate, "DateTime string was set correctly");
});

QUnit.test("DateTimeInput functions correctly format date", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var dateTimeInput = new DateTimeInput(getDummySettings());

    // Control the value stored in the input
    var valDate = new Date(1997,0,9,3,22,11); //Month is 0-11
    dateTimeInput.setValue(valDate);

    assert.strictEqual(dateTimeInput.getSubmitValue(), "DateTime(1997,1,9,3,22,11)", "DateInput formats submit value correctly");
});

QUnit.test("TimeInput setValue function correctly sets date", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var timeInput = new TimeInput(getDummySettings());

    // Control the value stored in the input
    var valDate = new Date(0,0,0,15,14,59); //Month is 0-11
    timeInput.setValue(valDate);

    assert.strictEqual(timeInput.getCurrentValue(), valDate, "Time object was set correctly");

    var strDate = "Time(15,14,59)";
    timeInput.setValue(strDate);

    assert.strictEqual(timeInput.getCurrentValue(), valDate, "Time string was set correctly");
});

QUnit.test("TimeInput functions correctly format date", function (assert) {
    //oPromptData, sValuesPath, sModelName, sViewId, oOptions
    var timeInput = new TimeInput(getDummySettings());

    // Control the value stored in the input
    var valDate = new Date(0,0,0,20,9,1); //Month is 0-11
    timeInput.setValue(valDate);

    assert.strictEqual(timeInput.getSubmitValue(), "Time(20,9,1)", "TimeInput formats submit value correctly");
});