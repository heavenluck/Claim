'ues strick';

var oI18nModel = new sap.ui.model.resource.ResourceModel({
    bundleUrl: bobj.crvUri("../ui5prompting/resources/prompting.properties"),
    bundleLocale: "en"
});
var oResourceBundle = oI18nModel.getResourceBundle();
sap.ui.getCore().setModel(oResourceBundle, "ui5ResourceBundle");

var sViewMoelName = "TestingViewModel";
var sValueModelNmae = "TestingModel";
var sValuesPath = "/values";
var sValidationPath = "/validation";

var oMockInput = {
    value: undefined,
    inc: undefined,

    setValue: function(v) {
        this.value = v;
    },

    setInclusive: function(i) {
        this.inc = i;
    },

    getIncluding: function() {
        return this.inc;
    },

    isEmpty: function() {
        this.value != undefined && this.value != '';
    }
};

function getMockPromptData() {
    return {
        id: "mockPrompt",
        isOptionalPrompt: {
            allowNullValue: false
        }
    };
}

function mockInitController(oController) {
    oController.valuesPath = sValuesPath;
    oController.validationPath = sValidationPath;

    oController.valueModelName = sValueModelNmae;

    oController.viewModelName = sViewMoelName;

    oController.promptData = getMockPromptData();

    oController.getView = function() {
        return {
            getInput: function() {
                return oMockInput;
            },
            getStartInput: function() {
                return oMockInput;
            },
            getEndInput: function() {
                return oMockInput;
            }
        };
    };

    oMockInput.fireValueChange = function() {
        var valResult = {
            result: true
        };
        if (this.value != undefined) {
            valResult.value = this.value;
        }
        oController.onSubmit(valResult);
    };
}

function resetModel() {
    sap.ui.getCore().setModel(new sap.ui.model.json.JSONModel({}), sValueModelNmae);
    sap.ui.getCore().getModel(sValueModelNmae).setProperty(sValuesPath, []);
    sap.ui.getCore().setModel(new sap.ui.model.json.JSONModel({}), sViewMoelName);
}

function hookModelPathBuilder() {
    ModelPathBuilder.BuildFullValuePath = function(key ,oPromptData, sViewerName) {
        return  ModelPathBuilder.GetValueModleName(sViewerName) + '>/' + key;
    };

    ModelPathBuilder.BuildFullViewPath = function(key ,oPromptData, sViewerName) {
        return  ModelPathBuilder.GetViewModleName(sViewerName) + '>/' + key;
    };

    ModelPathBuilder.BuildPath = function(key, oPromptData) {
        return  '/' + key;
    };

    ModelPathBuilder.GetValueModleName = function(sViewerName) {
        return sValueModelNmae;
    };

    ModelPathBuilder.GetViewModleName = function(sViewerName) {
        return sViewMoelName;
    };
}

var eventBus = sap.ui.getCore().getEventBus;

function hookEventBus(cb) {
    sap.ui.getCore().getEventBus = function() {
        return {
            publish: function(arg1, arg2, arg3) {
                cb(arg1, arg2, arg3);
            }
        };
    };
}

function restoreEventBus() {
    sap.ui.getCore().getEventBus = eventBus;
}
