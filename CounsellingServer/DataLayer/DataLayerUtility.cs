using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace CounsellingServer.DataLayer
{
    public class DataLayerUtility
    {
        public static Exception LasException { get; private set; }

        protected DataLayerUtility()
        {
        }

        public static SqlDataAdapter NewDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public static SqlCommand NewSqlStatmentCommand(string aStoredProc)
        {
            SqlCommand result = new SqlCommand(aStoredProc);
            result.CommandType = CommandType.Text;
            return result;
        }

        public static SqlCommand NewStoredProcCommand(string aStoredProc)
        {
            SqlCommand result = new SqlCommand(aStoredProc);
            result.CommandType = CommandType.StoredProcedure;
            return result;
        }
        public static string GetAppSetting(string aKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[aKey];
        }

        public static SqlConnection GetConnection(int aSystemUser)
        {
            SqlConnection result = new SqlConnection(GetAppSetting(GetConnectionString()));
            ClaimConnection(result, aSystemUser);
            return result;
        }
        private static string GetConnectionString()
        {
            string ConnectionStringName = "DefaultconnectionString";
            XmlDocument xml = new XmlDocument(); 
            xml.Load(GetAppSetting("SiteFile") + "ConnectionStrings.xml");

            XmlElement xelRoot = xml.DocumentElement;
            XmlNodeList xnlNodes = xelRoot.SelectNodes("/root/Site");

            try
            {
                foreach (XmlNode xndNode in xnlNodes)
                {
                    if (xndNode["SiteId"].InnerText.Trim() == (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session["UniqueSiteId"] == null ? "" : System.Web.HttpContext.Current.Session["UniqueSiteId"].ToString()))
                    {
                        ConnectionStringName = xndNode["ConnectionStringName"].InnerText;
                    }

                }
            }
            catch(Exception ex) {
                ConnectionStringName = "DefaultconnectionString";
                //System.Web.HttpContext.Current.Session["UniqueSiteId"] = null;
                //throw LasException;
            }  
            return ConnectionStringName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aSystemUser"></param>
        private static void ClaimConnection(SqlConnection aConnection, int aSystemUser)
        {
            SqlParameter newParam;
            SqlCommand cmd = DataLayerUtility.NewStoredProcCommand("prConnectionOwnerClaim");
            cmd.Connection = aConnection;
            newParam = cmd.Parameters.Add("@SystemUser", SqlDbType.Int);
            newParam = cmd.Parameters.Add("@retval", SqlDbType.Int);
            newParam.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters["@SystemUser"].Value = aSystemUser;
            if (aConnection.State != ConnectionState.Open)
            {
                aConnection.Open();
            }
            cmd.ExecuteNonQuery();
        }

    }
}
