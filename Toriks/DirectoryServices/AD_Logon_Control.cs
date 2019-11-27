using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace Toriks.DirectoryServices
{
    ///<summary>
    ///After that, fill attributeList with the attributes you will use
    ///</summary>
    ///<param>
    ///<param name="attributeList">Attributes you will use        
    ///</param>
    public class AD_Logon_Control
    {
        /// <summary>
        /// Attributes you will use
        /// </summary>
        private List<string> attributeList_;

        ///<summary>
        ///After that, fill attributeList with the attributes you will use
        ///</summary>
        ///<param>
        ///<param name="attributeList">Attributes you will use        
        ///</param>
        public AD_Logon_Control(List<string> attributeList) 
        {
            attributeList_ = attributeList;
        }

        ///<summary>
        ///Try to logon Active Directory Service
        ///</summary>
        ///<param name="path">Active Directory Service path.
        ///<example>LDAP://mayservice.net</example>
        /// </param>
        ///<param name="userName">Active Directory Service user name. Generally sAMAccountName        
        ///</param>
        ///<param>
        ///<param name="password">Active Directory Service user password.        
        ///</param>
        public tryLogonResult tryLogon(string path, string userName, string password)
        {

            if (attributeList_.Count==0) 
            {
                tryLogonResult res = new tryLogonResult();
                res.Exception = "Attribute list is emty";
                res.isSuccess = false;
                res.propertyList = null;

                return res;
            }
            if (path.Trim()=="")
            {
                tryLogonResult res = new tryLogonResult();
                res.Exception = "Directory Service path is not defined";
                res.isSuccess = false;
                res.propertyList = null;

                return res;
            }
            if (userName.Trim() == "")
            {
                tryLogonResult res = new tryLogonResult();
                res.Exception = "User name is not defined";
                res.isSuccess = false;
                res.propertyList = null;

                return res;
            }
            if (password.Trim() == "")
            {
                tryLogonResult res = new tryLogonResult();
                res.Exception = "Password is not defined";
                res.isSuccess = false;
                res.propertyList = null;

                return res;
            }

            try
            {
                DirectoryEntry DEntry = new DirectoryEntry(@path, userName.Trim(), password.Trim());
                DirectorySearcher DSearch = new DirectorySearcher(DEntry);
                DSearch.Filter = string.Format("(sAMAccountName={0})", userName.Trim());

                SearchResult SResult = DSearch.FindOne();


                tryLogonResult res = new tryLogonResult();
                res.Exception = "";
                res.isSuccess = true;
                res.propertyList = getProperyt(SResult);

                return res;

            }
            catch (Exception ex)
            {

                tryLogonResult res = new tryLogonResult();
                res.Exception = ex.ToString();
                res.isSuccess = false;
                res.propertyList = null;

                return res;
            }



        }

        private Dictionary<string, string> getProperyt(SearchResult sResult)
        {
            Dictionary<string, string> propKeyVal = new Dictionary<string, string>();
            foreach (string item in attributeList_)
            {
                if (sResult.Properties[item].Count>0) 
                {
                    propKeyVal.Add(item, sResult.Properties[item][0].ToString());
                }
            }

            return propKeyVal;
        }
    }
}
