using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Helper
{
    public class HelperMethod
    {
        //this method only for the user jobseeker photo do add any other extension
        public static string GetFileExtension(string base64String)
        {
            try
            {
                var data = base64String?.Substring(0, 5);

                switch (data.ToUpper())
                {
                    case "IVBOR":
                        return "png";
                    case "/9J/4":
                        return "jpg";
                    default:
                        return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
