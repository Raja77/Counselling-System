 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;

namespace CounsellingServer.DataLayer
{

    public class PoolConnection
    {
        public PoolConnection()
        {
            ID = Guid.NewGuid();
            IssueTime = DateTime.Now;
            Remove = false;
        }
        public SqlConnection dbCon { get; set; }
        public Guid ID { get; set; }
        public DateTime IssueTime { get; set; }
        public bool Remove { get; set; }
        public static int TimeOutInSeconds { get { return 480; } }
    }
    public class DBConnectionPool
    {
        protected DBConnectionPool()
        {
        }
        public static Dictionary<string, List<PoolConnection>> poolConnections { get; set; }

        public static void SendConBackToPool(PoolConnection con)
        {
            if (con != null && con.dbCon.State == ConnectionState.Open)
            {
                con.dbCon.Close();
            }
        }
        public static void GetConFromPool(PoolConnection con)
        {
            // called from data adapter places
        }
        public static PoolConnection GetAvailableConnectionFromPool(int aSystemUser)
        {
            string tenant = string.Empty;
            if (HttpContext.Current.Request.Headers.Get("Tenant") != null)
            {
                tenant = (HttpContext.Current.Request.Headers["Tenant"] + "").ToUpper().Trim();
            }
            else
            {
                tenant = HttpContext.Current.Request.Cookies.Get("TENANT") != null
              ? (HttpContext.Current.Request.Cookies.Get("TENANT")["ID"] + "").ToUpper().Trim()
              : "";
            }
            if (poolConnections == null)
            {
                poolConnections = new Dictionary<string, List<PoolConnection>>();
            }
            if (!poolConnections.ContainsKey(tenant))
            {
                poolConnections.Add(tenant, new List<PoolConnection>());
            }
            RemoveTimedOutConnections(false, tenant);

            PoolConnection con = null;
            if (tenant != "" && poolConnections.ContainsKey(tenant))
            {
                con = poolConnections[tenant].Find(o => o.dbCon.State == ConnectionState.Closed && o.IssueTime.AddSeconds(10).CompareTo(DateTime.Now) <= 0);
            }
            if (con != null)
            {
                con.IssueTime = DateTime.Now;
                return con;
            }
            else
            {
                con = new PoolConnection();
                SqlConnection aSqlConnection = DataLayerUtility.GetConnection(aSystemUser);
                con.dbCon = aSqlConnection;
                poolConnections[tenant].Add(con);
                return con;
            }
        }

        public static void RemoveTimedOutConnections(bool closeAll, string tenantID)
        {

            if (poolConnections.ContainsKey(tenantID))
                if (closeAll)
                {
                    foreach (string tenant in poolConnections.Keys)
                    {
                        foreach (PoolConnection con in poolConnections[tenant])
                        {
                            if (con.dbCon.State == ConnectionState.Open)
                                con.dbCon.Close();
                        }
                    }
                    poolConnections = null;
                }
                else
                {
                    try
                    {
                        foreach (PoolConnection con in poolConnections[tenantID])
                        {
                            int timeOut = con.IssueTime.AddSeconds(PoolConnection.TimeOutInSeconds).CompareTo(DateTime.Now);
                            if (timeOut < 0)
                            {
                                if (con.dbCon.State == ConnectionState.Open)
                                    con.dbCon.Close();
                                con.Remove = true;
                            }
                        }
                        List<PoolConnection> temp = poolConnections[tenantID].FindAll(o => !o.Remove);
                        poolConnections[tenantID] = temp;
                    }
                    catch //(Exception ex)
                    {
                       // EFDbContext.exceptionLogger(ex);
                    }
                }
        }
    }
}
