"use strict";

jQuery.sap.require("ui5prompting.inputs.LOVDialogCreator");
hookModelPathBuilder();

QUnit.module("FlatLov Tests");

function getLov(size) {
    var lov = new Array(size);
    for (var i = 0; i < lov.length; i++) {
        lov[i] = { value : i };
    }
    return lov;
}
QUnit.test("Search when large lov", function (assert) {
    assert.expect(3);
    resetModel();
    hookEventBus(function(sChannel, sEvent, value) {
        if (sEvent == bobj.crv.fiori.event.SEARCH_LOV) {
            assert.ok(true, "Search event fired correctly");
            assert.ok(value.text == 1, "Search value correctly");
        }
    });

    var lovDialog = new ui5prompting.inputs.FlatLovDialog({
        promptData: getMockPromptData(),
        options: {}
    });
    
    sap.ui.getCore().getModel(sViewMoelName).setProperty(ModelPathBuilder.BuildPath(ModelPathBuilder.VIEW_LOVGROWING), true);
    assert.ok(lovDialog.getLazyLoad() == true, "Set to large lov correctly");
    lovDialog.fireSearch({
        value : "1"
    });
});

QUnit.test("_setSelectedValue interface", function (assert) {
    resetModel();

    var lovDialog = new ui5prompting.inputs.FlatLovDialog({
        promptData: getMockPromptData(),
        options: {maxNumParameterDefaultValues : 200}
    });
    lovDialog.selectedValues = [{value: 1}, {value: 2}];
    
    lovDialog._setMultiSelectedValue([{value: 1}, {value: 2}], false);
    assert.ok(lovDialog.selectedValues.length == 2, "_setSelectedValue([values], add) correct");
    
    lovDialog._setMultiSelectedValue([{value: 2}, {value: 3}], false);
    assert.ok(lovDialog.selectedValues.length == 3, "_setSelectedValue([values], add) correct");
    
    lovDialog._setMultiSelectedValue([{value: 2}, {value: 3}], true);
    assert.ok(lovDialog.selectedValues.length == 1, "_setSelectedValue([values], del) correct");
});


