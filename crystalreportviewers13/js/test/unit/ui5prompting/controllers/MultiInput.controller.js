'use strict';

QUnit.module("MultiInputController Tests");

var oController = sap.ui.controller("ui5prompting.controllers.MultiInput");

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
    value = sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[1].value;
    assert.ok(value == "test1",
            "Add value correct.");

});

QUnit.test("Update Multi Values", function (assert) {
    mockInitController(oController);
    resetModel();
    
    oController.updateValues({
        values : [
            {value : "test"}, 
            {value : "test1"}
           ]
        });
    assert.ok(sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0].value == "test", "Add value correct.");
    assert.ok(sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[1].value == "test1", "Add value correct.");
});

QUnit.test("Remove Values", function (assert) {
    mockInitController(oController);
    resetModel();
    
    oController.updateValues({
        values : [
            {value : "test"}, 
            {value : "test1"},
            {value : "test2"}
           ]
        });
    
    oController.onRemoveValues([{value : "test"}]);
    assert.ok(sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath).length == 2, "Remove single value correct.");
    assert.ok(sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[0].value == "test1" &&
            sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath)[1].value == "test2", "Remove single value validation correct.");
    
    oController.onRemoveValues([{value : "test1"}, {value : "test2"}]);
    assert.ok(sap.ui.getCore().getModel(oController.valueModelName).getProperty(oController.valuesPath).length == 0, "Remove multi value correct.");
});
