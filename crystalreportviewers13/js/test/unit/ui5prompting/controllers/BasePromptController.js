'use strict';

jQuery.sap.require("ui5prompting.controllers.BasePromptController");
QUnit.module("BasePromptController Tests");

var oController = new ui5prompting.controllers.BasePromptController;
mockInitController(oController);

QUnit.test("OnSubmit success", function (assert) {
    assert.expect(5);
    var result = true;
    resetModel();

    oController.updateValues = function(oEvent) {
        assert.ok(oEvent.value == "test", "Value update correct");
        assert.ok(true, "Success invoke update value");
    };

    oController.typeValidation = function() {
        assert.ok(true, "Success invoke type validation");
        hookEventBus(function(arg1, arg2, arg3) {
            assert.ok(arg3.result == result, "Notify dependency successful");
        });
        return result;
    };

    oController.onSubmit({
        result : true,
        value : "test"
    });

    assert.ok(sap.ui.getCore().getModel(oController.viewModelName).getProperty(oController.validationPath) == true,
            "Validation update correct");

    restoreEventBus();
    oController.updateValue = undefined;
    oController.typeValidation = undefined;
});

QUnit.test("OnSubmit faliure", function (assert) {
    assert.expect(2);
    resetModel();

    oController.updateValues = function(oEvent) {
        assert.ok(false, "Failure should not invoke touched");
    };

    oController.typeValidation = function() {
        assert.ok(false, "Failure should not invoke type validation");
    };

    hookEventBus(function(arg1, arg2, arg3) {
        assert.ok(arg3.result == false, "Notify dependency successful");
    });

    oController.onSubmit({
        result : false,
        value : "test"
    });

    assert.ok(sap.ui.getCore().getModel(oController.viewModelName).getProperty(oController.validationPath) == false,
    "Validation update correct");

    restoreEventBus();
    oController.updateValue = undefined;
    oController.typeValidation = undefined;
});
