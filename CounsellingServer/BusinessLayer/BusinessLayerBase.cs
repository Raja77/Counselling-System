using System;
using System.Data;
using CounsellingServer.DataLayer;
using System.Configuration;

namespace CounsellingServer.BusinessLayer
{
    public class BusinessLayerBase
    {
        private static bool isTraceEnabled = false;
        //****************************************************
        //****************************************************
        public BusinessLayerBase()
        {
            
        }
        static BusinessLayerBase()
        {
            bool.TryParse(ConfigurationManager.AppSettings["EnableSqlServerTrace"], out isTraceEnabled);
        }
        //****************************************************
        //****************************************************
        public virtual IDataLayer DataLayer
        {
            get;
            set;
        }
        //****************************************************
        //****************************************************
        public virtual void ClearLastException()
        {
            LastException = null;
        }
        //****************************************************
        //****************************************************
        public virtual int SystemUser
        {
            get;
            set;
        }
        //****************************************************
        //****************************************************
        public virtual Exception LastException
        {
            get;
            set;
        }
        //****************************************************
        //****************************************************
        public virtual bool Save(DataSet aDataSet)
        {
            return Execute(SqlAction.Save, aDataSet);
        }
        //****************************************************
        //****************************************************
        public virtual bool Delete(DataSet aDataSet, int aKey)
        {
            return Execute(SqlAction.Delete, aDataSet, aKey);
        }
        //****************************************************
        //****************************************************
        public virtual bool Fetch(DataSet aDataSet, int aKey)
        {
            return Execute(SqlAction.Fetch, aDataSet, aKey);
        }
        //****************************************************
        //****************************************************
        public virtual bool FetchAll(DataSet aDataSet)
        {
            return Execute(SqlAction.FetchAll, aDataSet);
        }
        //****************************************************
        //****************************************************
        protected virtual bool Execute(SqlAction aSqlAction, DataSet aDataSet)
        {
            return Execute(aSqlAction, aDataSet, null);
        }
        //****************************************************
        //****************************************************
        protected virtual bool Execute(SqlAction aSqlAction, params Object[] aParams)
        {
            return Execute(aSqlAction, null, aParams);
        }
        //****************************************************
        //****************************************************
        //Execute a SQL query (aSqlAction)
        protected virtual bool Execute(SqlAction aSqlAction, DataSet aDataSet, params Object[] aParams)
        {
            bool result = true;
            try
            { 
                DataLayerMessage message = new DataLayerMessage(SystemUser, aSqlAction, aDataSet, aParams);
                //DateTime startTime = DateTime.Now;
                DataLayer.Execute(message);
                //if (isTraceEnabled)
                //{
                //    try
                //    {
                //        TraceListenerBL aTraceListenerBL = new TraceListenerBL(0);
                //        string Param = "";
                //        if (aParams != null)
                //        {
                //            foreach (object obj in aParams)
                //            {
                //                if (obj == null)
                //                    Param += "Null,";
                //                else
                //                    Param += obj.ToString() + ",";
                //            }
                //        }
                //        aTraceListenerBL.TraceInformation(startTime, DataLayer.SqlEntityX, aSqlAction.ToString(), Param);
                //    }
                //    catch
                //    {
                //        // For Debugging only 
                //    }
                //}
            }
            catch (Exception Ex)
            {
                LastException = Ex;

                // For Debugging only 
                throw LastException;
            }

            return result;
        }
        //****************************************************
        //****************************************************
        public virtual SqlEntity SqlEntity
        {
            get { return DataLayer.SqlEntity; }
        }
        //****************************************************
        //****************************************************
        public virtual string SqlEntityX
        {
            get { return DataLayer.SqlEntityX; }
        }
        //****************************************************
        //****************************************************
        public virtual string ShortSqlEntityX
        {
            get { return DataLayer.ShortSqlEntityX; }
        }
        //****************************************************
        //****************************************************
        public virtual bool Clear(DataSet ds)
        {
            return Execute(SqlAction.Clear, ds);
        }
        //****************************************************
        //****************************************************
        public virtual DataRow GetRow(int aKey)
        {
            DataRow result = null;
            DataSet ds = new DataSet();
            Fetch(ds, aKey);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0];
            }

            return result;
        }


    }
}
