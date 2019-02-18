//Define testing to avoid unecessary viewer js
jQuery.sap.require("crviewer.crv");
bobj.ui5Testing = {};

sap.ui.define([
    // Mock
    "test/unit/ui5prompting/CommonMock",

    //Controllers
    "test/unit/ui5prompting/controllers/BasePromptController",
    "test/unit/ui5prompting/controllers/DiscreteInput.controller",
    "test/unit/ui5prompting/controllers/MultiInput.controller",
    "test/unit/ui5prompting/controllers/RangeInput.controller",
    "test/unit/ui5prompting/controllers/MultiRange.controller",

    //Inputs
    "test/unit/ui5prompting/inputs/BaseInput",
    "test/unit/ui5prompting/inputs/DerivedInputs",
    "test/unit/ui5prompting/inputs/InputRow",
    "test/unit/ui5prompting/inputs/LovDialog",
    "test/unit/ui5prompting/inputs/HierarchyLOVDialog",

], function() { "use strict"; });

//Hide JS runtime console object, we want to intercept errors and fail tests (//TODO: could probably handle function arguments more intelligently)
var CONSOLE_ERROR_LOGGED = function(msg) {
    this.msg = msg;
};
var runtimeConsole = console;
var console = {
        error: function(msg) {
            throw new CONSOLE_ERROR_LOGGED(msg); //throw for testing purposes
        },

        log: function(msg) {
            runtimeConsole.log(msg);
        },

        warn: function(msg) {
            runtimeConsole.warn(msg);
        }
};

// create dummy resource bundle
sap.ui.getCore().setModel("ui5ResourceBundle", {});
