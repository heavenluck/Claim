'use strict';

sap.m.Dialog.extend("ui5prompting.views.PromptDialog", {
    renderer: "sap.m.DialogRenderer",

    constructor: function(id, mSettings) {
        mSettings.title = sap.ui.getCore().getModel("ui5ResourceBundle").getText("PromptingTitle")
        mSettings.draggable = true;
        mSettings.verticalScrolling = false;
        mSettings.horizontalScrollin = false;
        mSettings.resizable = true;
        mSettings.escapeHandler = function (oPromise){
            oPromise.reject(); // Don't close dialog with escape key. 
        };

        sap.m.Dialog.call(this, id, mSettings);
        
        this.addStyleClass("sapUiSizeCompact");
        
        this.attachBeforeOpen(function() {
            this.addContent(sap.ui.view({
                viewName : "ui5prompting.views.Prompting",
                type : sap.ui.core.mvc.ViewType.JS,
                height : "100%",
                viewData : {
                    getPromptData : function() {
                        return mSettings.adaptor.getPromptData(mSettings.viewerName)
                    },
                    getReportState : function() {
                        return mSettings.adaptor.getReportStateInfo(mSettings.viewerName)
                    },
                    getUseSavedData : function() {
                        return mSettings.adaptor.getUseSavedData ? mSettings.adaptor.getUseSavedData(mSettings.viewerName) : false;
                    },
                    viewerName : mSettings.viewerName,
                    viewerType : mSettings.viewerType,
                    rptSrcKey : mSettings.rptSrcKey,
                    servletURL : mSettings.servletURL
                }
            }));
            
            var confirmBtn = undefined;
            var rejectBtn = undefined;
            if (mSettings.adaptor.getUseOKCancelButtons(mSettings.viewerName)) {
                confirmBtn = new sap.m.Button({ text:sap.ui.getCore().getModel("ui5ResourceBundle").getText("OK") });;
                if (mSettings.adaptor.getIsDialog(mSettings.viewerName))
                    rejectBtn = new sap.m.Button({ text:sap.ui.getCore().getModel("ui5ResourceBundle").getText("Cancel") });;
            } else {
                confirmBtn = new sap.m.Button({ text:sap.ui.getCore().getModel("ui5ResourceBundle").getText("Run") });;
            }
            
            confirmBtn.attachPress(function() { 
                var contents = this.getContent();
                var closeDialog = true;
                if (contents && contents.length > 0) {
                    closeDialog = contents[0].getController().run();
                }
                
                if (closeDialog)
                    this.close();
            }.bind(this));
            
            this.addButton(confirmBtn);
            
            if (rejectBtn) {
                rejectBtn.attachPress(function() {
                    this.close();
                }.bind(this));
                this.addButton(rejectBtn);
            }
        }.bind(this));
        
        this.attachAfterClose(function() {
            this.destroyButtons();
            this.destroyContent();
            mSettings.afterClose();
        }.bind(this));
    },
    
    setHeight: function(height) {
        if (height) {
            height += "px";
            this.setContentHeight(height);
        }
    },
    
    setWidth: function (width) {
        if (width) {
            width += "px";
            this.setContentWidth(width);
        }
    }
});