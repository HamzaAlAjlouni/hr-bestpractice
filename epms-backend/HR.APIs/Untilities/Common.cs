using HR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.APIs.Untilities
{
    public static class Common
    {
        public static string getResourceByKey(string key, List<tbl_resources> resources)
        {
            string value = key;
            if (resources != null && resources.Count > 0)
            {
                tbl_resources res = resources.Where(x => x.resource_key == key).FirstOrDefault();
                if (res != null)
                {
                    value = res.resource_value;
                }
            }

            return value;
        }
    }
}