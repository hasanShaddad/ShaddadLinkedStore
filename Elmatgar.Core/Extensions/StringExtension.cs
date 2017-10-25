using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.Core.Extensions
{
   public static class StringExtension
    {
        /// <summary>
        /// extentsion for access token to renew app secret proof
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GenerateAppSecretProof(this string accessToken)
        {
            //create a AppsecretProof value to be used for each graphApi call
            //AppsecretProof is sha256 incrypted string for the current facebook accessToken
            using (HMACSHA256 algorethm = new HMACSHA256(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Facebook_AppSecret"])))
            {
                byte[] Hash = algorethm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
                StringBuilder Builder = new StringBuilder();
                for (int i = 0; i < Hash.Length; i++)
                {
                    Builder.Append(Hash[i].ToString("x2", CultureInfo.InvariantCulture));
                }
                return Builder.ToString();
            }
        }

        public static string GraphApiCall(this string baseGraphApiCall,params object[] args)
        {
            //return a formatted Graph api call with a version prifex and appends a query string parameters
            if (!string.IsNullOrEmpty(baseGraphApiCall))
            {
                if (args!=null && args.Count()>0)
                {
                    //deteremine if we need a concatenate app secret proof query string parameter or inject it
                    string _graphApiCall = string.Empty;
                    if (baseGraphApiCall.Contains("?"))
                    {
                        _graphApiCall =
                            string.Format(baseGraphApiCall + "&appsecret_proof={" + (args.Count() - 1).ToString());
                        return string.Format("v2.1/{0}", _graphApiCall);
                    }
                    else
                    {
                        string.Format(baseGraphApiCall + "?appsecret_proof={" + (args.Count() - 1).ToString());
                        //prefix with api graph version
                        return string.Format("v2.1/{0}", _graphApiCall);
                    }
                    
                }
                else
               
                    throw new Exception("graph api need at least one parameter");
                
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
