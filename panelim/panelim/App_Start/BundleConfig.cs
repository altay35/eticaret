using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace mvcpanel.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scriptsler").Include(
                    "~/Scripts/jquery.min.js", 
                     "~/Scripts/bootstrap.min.js", 
                    "~/Scripts/metisMenu.min.js", 
                      "~/Scripts/sb-admin-2.js", 
                    "~/Scripts/jquery.validate.js", 
                    "~/Scripts/jquery.validate.unobtrusive.min.js", 
                     "~/Scripts/bootbox.min.js", 
                    "~/Scripts/jquery.dataTables.min.js", 
                    "~/Scripts/dataTables.bootstrap.min.js" 



                ));
        }
    }
}