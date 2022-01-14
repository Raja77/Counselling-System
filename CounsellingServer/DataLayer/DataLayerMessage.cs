using System.Collections;
using System.Data;
using System.Data.Common;

namespace CounsellingServer.DataLayer
{
    /// <summary>
    /// Description for DataLayerMessage
    /// </summary>
    public class DataLayerMessage
    {
        private const string PARAMPREFIX = "auto";
        private Hashtable _params;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aSystemUser"></param>
        /// <param name="aSqlAction"></param>
        /// <param name="aDataSet"></param>
        /// <param name="aKey"></param>
        /// <param name="aOutParams"></param>
        /// DataLayerMessage message = new DataLayerMessage(SqlAction.Insert, BusinessLayerUtility.GetConnection(), ds, aParams);
        public DataLayerMessage(int aSystemUser, SqlAction aSqlAction, DataSet aDataSet, int aKey)
        {
            SqlAction = aSqlAction; 
            DataSet = aDataSet;
            Key = aKey;
            SystemUser = aSystemUser;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="aSystemUser"></param>
        /// <param name="aSqlAction"></param>
        /// <param name="aDataSet"></param>
        /// <param name="aOutParams"></param>
        /// <param name="aParams"></param>
        public DataLayerMessage(int aSystemUser, SqlAction aSqlAction, DataSet aDataSet, params object[] aParams)
        {
            SqlAction = aSqlAction;
            DataSet = aDataSet;
            if (aParams != null)
            {
                for (int i = 0; i < aParams.Length; i++)
                {
                    Params.Add(PARAMPREFIX + i.ToString(), aParams[i]);
                }
            }
            SystemUser = aSystemUser;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="aSystemUser"></param>
        /// <param name="aSqlAction"></param>
        /// <param name="aDataSet"></param>
        public DataLayerMessage(int aSystemUser, SqlAction aSqlAction, DataSet aDataSet)
        {
            SqlAction = aSqlAction;
            DataSet = aDataSet;
            Key = 0;
            SystemUser = aSystemUser;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="aSystemUser"></param>
        /// <param name="aDataRow"></param>
        public DataLayerMessage(int aSystemUser, DataRow aDataRow)
        {
            DataRow = aDataRow;
            Key = 0;
            SystemUser = aSystemUser;
        }
        /// <summary>
        ///  
        /// </summary>
        public int SystemUser { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public DataRow DataRow { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public SqlAction SqlAction { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public DataSet DataSet { set; get; }
        /// <summary>
        ///  
        /// </summary>
        public int Key { set; get; }
        /// <summary>
        ///  
        /// </summary>
        public Hashtable Params
        {
            get
            {
                if (_params == null)
                {
                    _params = new Hashtable();
                }
                return _params;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="aParamName"></param>
        /// <param name="aParamValue"></param>
        public void AddParam(string aParamName, object aParamValue)
        {
            Params[aParamName] = aParamValue;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="aCommandParams"></param>
        public void ApplyParams(DbParameterCollection aCommandParams)
        {
            for (int i = 0; i < aCommandParams.Count; i++)
            {
                if (i < Params.Count)
                    aCommandParams[i].Value = Params[PARAMPREFIX + i.ToString()];
            }
        }
    }
}
