"use strict";
jQuery.sap.require("sap.ui.base.Object");
jQuery.sap.require("sap.m.Input");
jQuery.sap.require("sap.m.Select");
jQuery.sap.require("sap.m.DatePicker");
jQuery.sap.require("sap.m.DateTimePicker");
jQuery.sap.require("sap.m.TimePicker");
jQuery.sap.require("sap.m.DateTimePickerRenderer");
jQuery.sap.require("sap.m.DatePickerRenderer");
jQuery.sap.require("sap.m.TimePickerRenderer");

jQuery.sap.require("ui5prompting.utils.ModelPathBuilder");
jQuery.sap.require("ui5prompting.inputs.LOVDialogCreator");

//Need this to construct parameter controller (type validation strings)
var panelObj = {
     setToolbarCallBacks: function() {
         // do nothing
     }
};

function buildBaseInput(sapControlInput) {
    return {
        constructor: function(mSettings) {
            this.listeners = [];
            this.promptData = mSettings.promptData;
            this.viewerName = mSettings.viewerName;
            this.options = mSettings.options;
            this.editMask = this.promptData.editMask && this.promptData.editMask.length > 0 ? RegExp(this.promptData.editMask) : undefined;
            this.basePromptController = mSettings.basePromptController;

            mSettings.enabled = '{' + ModelPathBuilder.BuildFullViewPath(ModelPathBuilder.VIEW_ENABLED, this.promptData, this.viewerName) + '}';
            // setup input hints
            if (this.promptData.fetchLOV && this.promptData.fetchLOV.toLowerCase() == "true") {
                mSettings.showValueHelp = (mSettings.allowShowValueHelp == undefined) ? true : mSettings.allowShowValueHelp;
            }
            // Super call
            sapControlInput.call(this, undefined, mSettings);

            if (mSettings.showValueHelp && this.attachValueHelpRequest) {
                this.attachValueHelpRequest(function() {
                    if (undefined != this.getSubmitValue()) {
                        if (this.promptData.isHierarchy) {
                            this.getLOVDialog().open(this.basePromptController.getModelValues());
                        }
                        else {
                            this.getLOVDialog().open([{ value : this.getSubmitValue() }]);
                        }
                    }
                    else
                        this.getLOVDialog().open();
                }.bind(this));
            }

            //TODO: See above for panelObj defintion (allows us to construct paramcontroller). This is extremely hacky. I have done this to get
            //the functionality, but really we should refactor ParamController, to remove methods from prototype that do not require object state.
            //Perhaps just a sub object to retreive strings (requires prompt options)?
            this.paramCtrlr = new bobj.crv.params.ParameterController(panelObj, {}, mSettings.options);
            if (this.attachLiveChange) {
                this.attachLiveChange(this.notifyAll.bind(this));
            }
            else if (this.attachChange) {
                this.attachChange(this.notifyAll.bind(this));
            }

            this.attachBrowserEvent('click', function() {
                sap.ui.getCore().getEventBus().publish(bobj.crv.fiori.event.UI_CHANNEL, bobj.crv.fiori.event.ON_FOCUS, { unitID: this.promptData.lovNetworkID , promptID: this.promptData.id,  updateSummary: true });
            }.bind(this));
        },

        setInputFail: function(sReason) {
            this.setValueState(sap.ui.core.ValueState.Error);
            this.setValueStateText(sReason);
        },

        setInputOK: function() {
            this.setValueState(sap.ui.core.ValueState.Success);
        },

        getWarningText: function(nCrStatus) {
            return this.paramCtrlr._getWarningText(this.promptData, nCrStatus);
        },

        getLabel: function(crValidationValue) {
            return this.paramCtrlr._getDisplayText(this.promptData, crValidationValue);
        },

        setInclusive: function(bInc) {
            if (bInc == undefined) {
                this.setValue(undefined);
            }
            this.inc = bInc;
        },

        correctInclusive: function() {
            // Adjust inclusive value
            if (this.isEmpty()) {
                this.inc = undefined;
            } else if (this.inc == undefined) {
                this.inc = true;
            }
        },

        isEmpty: function() {
            if (!this.getValue || (this.getValue() != undefined && this.getValue() != '')) {
                return false;
            } else {
                return true;
            }
        },

        validate: function() {
            var result = true;
            var crStatus = bobj.crv.params.Validator.ValueStatus.OK;
            if (!this.isEmpty()) {
                crStatus = bobj.crv.params.Validator.getInstance().validateValue(this.promptData, this.getCRValidationValue());
                result = crStatus == bobj.crv.params.Validator.ValueStatus.OK;
                if (result) {
                    if (!this.editMask || this.editMask.test(this.getCRValidationValue())) {
                        this.setInputOK();
                    } else {
                        result = false;
                        this.setInputFail(sap.ui.getCore().getModel("ui5ResourceBundle").getText("EditMaskWrong", this.promptData.editMask));
                    }
                } else {
                    this.setInputFail(this.getWarningText(crStatus));
                }
            } else {
                this.setInputOK();
            }

            this.correctInclusive();

            var oEvent = {
                result: result
            };

            if (!this.isEmpty()) {
                oEvent.value = this.getSubmitValue();
                oEvent.crValidationValue = this.getCRValidationValue();
                oEvent.currentValue = this.getCurrentValue();
                oEvent.labels = [this.getLabel(this.getCRValidationValue())];
                oEvent.inc = this.inc;
            }

            return oEvent;
        },

        getLOVDialog: function () {
            if (!this._oLOVDialog) {
                this._oLOVDialog = CreateLOVDialog({
                    promptData: this.promptData,
                    viewerName: this.viewerName,
                    baseInputId: this.sId,
                    options: this.options,
                    allowMultiSelect: false
                });

                this._oLOVDialog.attachSelected(function(values) {
                    if (values && values.length > 0) {
                        this.setValue(values[0].value);
                        this.correctInclusive();
                        var oEvent = {
                            result: true
                        };
                        if (!this.isEmpty()) {
                            oEvent.value = values[0].value;
                            oEvent.crValidationValue = this.getCRValidationValue();
                            oEvent.currentValue = this.getCurrentValue();
                            oEvent.labels = values[0].labels;
                            oEvent.inc = this.inc;
                        }
                        this.notifyAll(oEvent);
                    }
                }.bind(this));

                this.addDependent(this._oLOVDialog);
            }
            return this._oLOVDialog;
        },

        attachValueChange: function(cb) {
            this.listeners.push(cb);
        },

        fireValueChange: function() {
            this.notifyAll();
        },

        notifyAll: function(oEvent) {
            if (!oEvent || oEvent.result == undefined) {
                var oEvent = this.validate();
            }
            for (var i = 0; i < this.listeners.length; i++) {
                this.listeners[i](oEvent);
            }
        },

        onAfterRendering: function() {
            sapControlInput.prototype.onAfterRendering.call(this);
            if (!this.promptData.allowCustomValue) {
                this.$().find("input").attr("readonly", "true");
            }
        }
    };
}

function buildBaseDateInput(sapControlInput, disableMask) {

    // First build a base input object
    var oBaseInput = buildBaseInput(sapControlInput);
    // Get the constructor, so we can override and call in our constructor
    var baseConstructor = oBaseInput.constructor;

    // Set up new object
    var oBaseDateInput = oBaseInput;

    // Create constructor and call the base constructor first
    oBaseDateInput.constructor = function(mSettings) {
        baseConstructor.call(this, mSettings);

        // If this is a single input, we need to render an LOV button as part of this Control
        if (this.promptData.fetchLOV && this.promptData.fetchLOV.toLowerCase() == "true") {
            this.needValueHelp = (mSettings.allowShowValueHelp == undefined) ? true : mSettings.allowShowValueHelp;
        } else {
            this.needValueHelp = false;
        }

        // Validation only works with CR JSON date format
        if (typeof this.promptData.maxValue == "string") {
            this.promptData.maxValue = bobj.crv.params.dateToJson(this.parseSubmitValue(this.promptData.maxValue));
        }

        // Validation only works with CR JSON date format
        if (typeof this.promptData.minValue == "string") {
            this.promptData.minValue = bobj.crv.params.dateToJson(this.parseSubmitValue(this.promptData.minValue));
        }

        if (disableMask) {
            var disableEvents = [
                "onfocusin",
                "onfocusout",
                "oninput",
                "onkeypress",
                "onkeydown",
                "oncut"
            ];
            disableEvents.forEach(function(func) {
                this[func] = function() {
                    if (!this.promptData.allowCustomValue) {
                        return;
                    }
                    sapControlInput.prototype[func].apply(this, arguments);
                };
            }.bind(this));
        }
    };

    // Add common functions for all date types
    oBaseDateInput.getCurrentValue = function() {
        return this.getDateValue();
    };

    oBaseDateInput.getCRValidationValue = function() {
        if (this.getCurrentValue() instanceof Date)
            return bobj.crv.params.dateToJson(this.getCurrentValue());
    };

    oBaseDateInput.setValue = function(value) {
        if (value == undefined || value instanceof Date) {
            this.setDateValue(value);
        } else {
            this.setDateValue(this.parseSubmitValue(value));
        }
    };

    oBaseDateInput.onclick = function(oEvent) {
        if (jQuery(oEvent.target).hasClass("ui5Lov")) {
            this.getLOVDialog().open();
        } else {
            if (sapControlInput.prototype.onclick) {
                sapControlInput.prototype.onclick.call(this, oEvent);
            }
        }
    };

    oBaseDateInput.ontap = function(oEvent) {
        if (!jQuery(oEvent.target).hasClass("ui5Lov") && sapControlInput.prototype.ontap) {
            sapControlInput.prototype.ontap.call(this, oEvent);
        }
    };

    oBaseDateInput.onBeforeOpen = function(oEvent) {
        if (sapControlInput.prototype.onBeforeOpen) {
            sapControlInput.prototype.onBeforeOpen.call(this, oEvent);
            this.$().removeClass("sapMTPInputActive");
        }
    };

    return oBaseDateInput;
};

// BaseInput definitions
sap.m.Input.extend("ui5prompting.inputs.BaseInput", buildBaseInput(sap.m.Input));

sap.m.Select.extend("ui5prompting.inputs.BaseSelectInput", buildBaseInput(sap.m.Select));

sap.m.DatePicker.extend("ui5prompting.inputs.BaseDateInput", buildBaseDateInput(sap.m.DatePicker));

sap.m.DateTimePicker.extend("ui5prompting.inputs.BaseDateTimeInput", buildBaseDateInput(sap.m.DateTimePicker));

sap.m.TimePicker.extend("ui5prompting.inputs.BaseTimeInput", buildBaseDateInput(sap.m.TimePicker, true));

function buildDateRenderer(oSapRenderer) {
    return {
        writeDecorations: function(oRm, oDP) {
            if (oDP.needValueHelp) {
                if (oDP.getEnabled() && oDP.promptData.allowCustomValue) {
                    var aClasses = ["sapMInputValHelpInner"];
                    var mAttributes = {};

                    mAttributes["id"] = oDP.getId() + "-icon";
                    mAttributes["tabindex"] = "-1"; // to get focus events on it, needed for popup autoclose handling
                    mAttributes["title"] = null;
                    if (sap.ui.getCore().getConfiguration().getRTL())
                        oRm.write('<div class="sapMInputValHelp promptDatePickerRTL">');
                    else
                        oRm.write('<div class="sapMInputValHelp promptDatePicker">');
                    oRm.writeIcon(this._getIcon(), aClasses, mAttributes);
                    oRm.write("</div>");
                }

                if (oDP.getEnabled()) {
                    var aClasses = ["sapMInputValHelpInner", "ui5Lov"];
                    var mAttributes = {};

                    mAttributes["id"] = oDP.getId() + "-help-icon";
                    mAttributes["title"] = null;
                    oRm.write('<div class="sapMInputValHelp">');
                    oRm.writeIcon("sap-icon://value-help", aClasses, mAttributes);
                    oRm.write("</div>");
                }
            } else {
                oSapRenderer.writeDecorations(oRm, oDP);
            }
        }
    }
}

function buildTimeRenderer(oSapRenderer) {
    oSapRenderer._getIcon = function() {
        return "sap-icon://time-entry-request";
    }
    return oSapRenderer;
}

sap.m.DatePickerRenderer.extend("DateInputRenderer", buildDateRenderer(sap.m.DatePickerRenderer));

sap.m.DateTimePickerRenderer.extend("DateTimeInputRenderer", buildDateRenderer(sap.m.DateTimePickerRenderer));

sap.m.TimePickerRenderer.extend("TimeInputRenderer", buildTimeRenderer(buildDateRenderer(sap.m.TimePickerRenderer)));
