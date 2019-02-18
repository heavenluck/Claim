var HTTPRequestHandler = function(servletURL, rptSrcKey, reportState, useSavedData, bridge, viewerName, viewerType) {
    
    this._sServletUrl = servletURL;
    this._sReportSrcKey = rptSrcKey;
    this._sReportState = reportState;
    this._bUseSavedData = useSavedData;
    this.bridge = bridge;
    this.viewerName = viewerName;
    this.viewerType = viewerType;
    
    this.setUseSavedData = function(b) {
        this._bUseSavedData = b;
    }
    
    this.request = function (arg, callBack) {
        arg.savedData = this._bUseSavedData;
        
        if ("dotnet" == this.viewerType) {
            this.bridge.sendAsyncRequest(viewerName, {"crprompt": "PromptingEvent", "EventArgument": encodeURIComponent(JSON.stringify(arg)),
                "token": arg.lovNetworkUUID}, callBack);
        }
        else {
            jQuery.ajax(this._sServletUrl, {
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: jQuery.param({
                    "ServletTask": "PromptingEvent",
                    "ReportSourceKey": this._sReportSrcKey,
                    "ReportStateInfo": encodeURIComponent(this._sReportState),
                    "EventArgument": encodeURIComponent(JSON.stringify(arg))
                }),
                success: function(response) {
                    if (typeof(response) == "object") {
                        callBack(false, response);
                    }
                    else {
                        callBack(false, JSON.parse(response));
                    }
                },
                error: function (response) {
                    callBack(true, JSON.parse(response));
                }
            });
        }
    };
};
