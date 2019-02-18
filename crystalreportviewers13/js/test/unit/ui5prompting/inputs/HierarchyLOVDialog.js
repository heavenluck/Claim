"use strict";

jQuery.sap.require("ui5prompting.inputs.HierarchyLOVDialog");
hookModelPathBuilder();

QUnit.module("HierarchyLOVDialog Tests");

function getDummyLovValue() {
    var dummyLovValue = [{"hasChildren":true,"labels":["0HIER_NODE/WORLD","WORLD"],"value":"0HIER_NODE/WORLD","path":{"values":[],"indices":[]}},{"hasChildren":true,"labels":["1HIER_REST/REST_H","Not Assigned Country (s)"],"value":"1HIER_REST/REST_H","path":{"values":[],"indices":[]}}];
    return dummyLovValue;
};


QUnit.test("Fetch Lov", function (assert) {
    resetModel();

    var hierarchyLovDialog = new ui5prompting.inputs.HierarchyLOVDialog({
        promptData: getMockPromptData(),
        baseInputId : "UT1",
        options: {}
    });
    
    sap.ui.getCore().getModel(sViewMoelName).setProperty(hierarchyLovDialog.lovPath, getDummyLovValue());
    assert.ok(hierarchyLovDialog.getItems().length == 2, "Lov binding success");

});

QUnit.test("Lov setSelectedValues", function (assert) {
    resetModel();

    var hierarchyLovDialog = new ui5prompting.inputs.HierarchyLOVDialog({
        promptData: getMockPromptData(),
        baseInputId : "UT2",
        options: {}
    });
    
    sap.ui.getCore().getModel(sViewMoelName).setProperty(hierarchyLovDialog.lovPath, getDummyLovValue());
    assert.ok(hierarchyLovDialog.getItems().length == 2, "Lov binding success");
    
    hierarchyLovDialog.allowMultiValue = true;

    var values = [{"hasChildren":true,"labels":["0HIER_NODE/WORLD","WORLD"],"value":"0HIER_NODE/WORLD","path":{"values":[],"indices":[]}}];

    hierarchyLovDialog.setSelectedValues(values, true);
    assert.ok(hierarchyLovDialog.getNSelectedValues() == 1 && hierarchyLovDialog.getNRemovedValues() == 0, "setSelectedValues(values, true) correct");
    
    hierarchyLovDialog.setSelectedValues(values, false);
    assert.ok(hierarchyLovDialog.getNRemovedValues() == 1 && hierarchyLovDialog.getNSelectedValues() == 0, "setSelectedValues(values, false) correct");
});