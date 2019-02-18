'use strict';

QUnit.module("DiscreteInputController Tests");

var oController = sap.ui.controller("ui5prompting.controllers.DiscreteInput");

QUnit.test("Update Values", function (assert) {
    mockInitController(oController);
    resetModel();
    
    oController.updateValues({
        value : "test"
    });
    var value = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0].value;
    assert.ok(value == "test",
            "Add value correct.");
    
    oController.updateValues({
        value : "test1"
    });
    value = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0].value;
    assert.ok(value == "test1",
            "Update value correct.");

});

QUnit.test("Set Values", function (assert) {
    mockInitController(oController);
    resetModel();
    
    oController.setValues([{
        value : "test"
    }]);
    var value = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0].value;
    assert.ok(value == "test",
            "Set value correct.");
});

QUnit.test("Remove All", function (assert) {
    mockInitController(oController);
    resetModel();
    
    oController.setValues([{
        value : "test"
    }]);
    
    oController.promptData.isOptionalPrompt = false;
    oMockInput.setInputFail = function() {
        assert.ok(true, "Validation Optional correct");
    };

    oController.removeAll();
    var values = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath);
    assert.ok(values.length == 0, "Remove All values correct.");
    
    oMockInput.setInputFail = undefined;
});
