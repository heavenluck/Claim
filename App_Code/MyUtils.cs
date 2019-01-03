using System;
using System.Web;


public class MyUtils
{
    public static string GetWebsiteRoot()
    {
        HttpRequest req = HttpContext.Current.Request;

        string port = (req.Url.Port == 80 || req.Url.Port == 443 ? "" : ":" + req.Url.Port.ToString());

        string wsroot = req.Url.Scheme + "://" + req.Url.Host + port + req.ApplicationPath;   

	    if(wsroot.EndsWith("/"))
		    return wsroot;
	    else
		    return wsroot + "/";     
		
    }

}