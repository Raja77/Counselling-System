using System;
using System.Data.SqlClient;
using System.Xml;

namespace CounsellingServer.BusinessLayer
{
    public class BusinessLayerUtility
	{
		protected BusinessLayerUtility()
		{
		}
        
        public static string GetConnectionSetting()
        {
            return GetAppSetting(GetConnectionString());
        }


		public static string GetAppSetting(string aKey)
		{
            return  System.Configuration.ConfigurationManager.AppSettings[aKey];
		}

		public static SqlConnection GetConnection()
		{
            SqlConnection result = new SqlConnection(GetAppSetting(GetConnectionString()));
			return result;
		}

        private static string GetConnectionString()
        {
            string ConnectionStringName = "DefaultconnectionString";
            XmlDocument xml = new XmlDocument();
            xml.Load(GetAppSetting("SiteFile") + "ConnectionStrings.xml");

            XmlElement xelRoot = xml.DocumentElement;
            XmlNodeList xnlNodes = xelRoot.SelectNodes("/root/Site");

            foreach (XmlNode xndNode in xnlNodes)
            {
                if (xndNode["SiteId"].InnerText.Trim() == (System.Web.HttpContext.Current.Session["UniqueSiteId"] == null ? "" : System.Web.HttpContext.Current.Session["UniqueSiteId"].ToString()))
                {
                    ConnectionStringName = xndNode["ConnectionStringName"].InnerText;
                }

            }

            return ConnectionStringName;
        } 
       
		public static int Cast(Object aValue, int aOnError)
		{
			int result;
			try
			{
				result = int.Parse(aValue.ToString());
			}
			catch
			{
				result = aOnError;
			}
			return result;
		}

		public static double Cast(Object aValue, double aOnError)
		{
			double result;
			try
			{
				result = double.Parse(aValue.ToString());
			}
			catch
			{
				result = aOnError;
			}
			return result;
		}


		public static DateTime Cast(Object aValue, DateTime aOnError)
		{
            DateTime result;
			try
			{
				result = DateTime.Parse(aValue.ToString());
			}
			catch
			{
				result = aOnError;
			}
			return result;
		}

		public static bool Cast(Object aValue, bool aOnError)
		{
			bool result;
			try
			{
				result = ((bool)aValue);
			}
			catch
			{
				result = aOnError;
			}
			return result;
		}
	}
}
