jQuery.sap.registerModulePath("test.unit.ui5prompting", "./ui5prompting");
jQuery.sap.registerModulePath("ui5prompting", "../../ui5prompting");
jQuery.sap.registerModulePath("crviewer", "../../crviewer");
jQuery.sap.registerModulePath("crjs", "../..");
jQuery.sap.registerModulePath("external", "../../external");
jQuery.sap.registerModulePath("MochiKit", "../../MochiKit");

jQuery.sap.require("sap.ui.qunit.qunit-css");
jQuery.sap.require("sap.ui.thirdparty.qunit");
jQuery.sap.require("sap.ui.qunit.qunit-junit");
jQuery.sap.require("sap.ui.qunit.qunit-coverage");

jQuery.sap.require("crjs.ParameterBridge");
jQuery.sap.require("crviewer.crv");
jQuery.sap.require("crviewer.common");
jQuery.sap.require("ui5prompting.utils.UI5Polyfill");

jQuery.sap.require("external.date");
jQuery.sap.require("crviewer.ParameterController");
jQuery.sap.require("crviewer.Parameter");

QUnit.config.autostart = false;

sap.ui.require([ "test/unit/ui5prompting/allTests" ], function() {
    QUnit.start();
});