using System;
using System.Data;
using System.Data.SqlClient;
namespace CounsellingServer.DataLayer
{
    public class DataLayerBase
    {
        protected SqlEntity _sqlEntity;
        protected const string ReturnValueParamName = "@retval";
        //****************************************************
        //****************************************************
        public DataLayerBase()
        {
        }

        //****************************************************
        //****************************************************
        public virtual SqlEntity SqlEntity
        {
            get { return _sqlEntity; }
        }
        //****************************************************
        //****************************************************
        protected virtual SqlCommand GetCommand(SqlConnection aSqlConnection, SqlAction aSqlAction)
        {
            return null;
        }
        //****************************************************
        //****************************************************
        public void Execute(DataLayerMessage aMessage)
        {
            switch (aMessage.SqlAction)
            {
                case SqlAction.Save:
                    Save(aMessage);
                    break;
                default:
                    dbExecute(aMessage);
                    break;
            }
        }
        protected virtual void AddReturnValueParameter(SqlParameterCollection aParameters)
        {
            SqlParameter newParam = aParameters.Add(ReturnValueParamName, SqlDbType.Int);
            newParam.Direction = ParameterDirection.ReturnValue;
        }

        //****************************************************
        //****************************************************
        public virtual string SqlEntityX
        {
            get { return SqlEntity.ToString(); }
        }

        //****************************************************
        //****************************************************
        public virtual string ShortSqlEntityX
        {
            get
            {
                string result = SqlEntityX;
                if (result.Substring(0, 2).ToLower().Equals("tb"))
                {
                    result = result.Substring(2);
                }
                return result;
            }
        }
        protected virtual SqlDataAdapter GetDataAdapter(SqlConnection aSqlConnection)
        {
            SqlDataAdapter result = DataLayerUtility.NewDataAdapter();
            result.InsertCommand = GetCommand(aSqlConnection, SqlAction.Insert);
            result.UpdateCommand = GetCommand(aSqlConnection, SqlAction.Update);
            result.DeleteCommand = GetCommand(aSqlConnection, SqlAction.Delete);
            return result;
        }
        //****************************************************
        //****************************************************
        protected virtual void Save(DataLayerMessage aMessage)
        {
            SqlConnection aSqlConnection = DataLayerUtility.GetConnection(aMessage.SystemUser);
            SqlDataAdapter da = GetDataAdapter(aSqlConnection);
            AddCommonParams(SqlAction.Insert);
            AddCommonParams(SqlAction.Update);
            da.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);
            da.Update(aMessage.DataSet.Tables[SqlEntityX]);
            da.RowUpdated -= new SqlRowUpdatedEventHandler(OnRowUpdated);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aMessage"></param>
        PoolConnection poolCon { get; set; }
        SqlConnection myConnection { get; set; }

        protected virtual void dbExecute(DataLayerMessage aMessage)
        {
            SqlCommand cmd;
            SqlConnection aSqlConnection = DataLayerUtility.GetConnection(aMessage.SystemUser);

            SqlDataAdapter da = DataLayerUtility.NewDataAdapter();
            try
            {
                if (aMessage.DataSet == null)
                {

                    cmd = GetCommand(aSqlConnection, aMessage.SqlAction);
                    AddCommonParams(aMessage.SqlAction);
                    aMessage.ApplyParams(cmd.Parameters);

                    if (aSqlConnection == null)
                    {
                        poolCon = DBConnectionPool.GetAvailableConnectionFromPool(1);
                        myConnection = poolCon.dbCon;
                    }

                    if (aSqlConnection.State == ConnectionState.Closed)
                        aSqlConnection.Open();
                     
                    cmd.ExecuteNonQuery();
                   // aSqlConnection.Close();
                }
                else
                {
                    da.SelectCommand = GetCommand(aSqlConnection, aMessage.SqlAction);
                    AddCommonParams(aMessage.SqlAction);
                    aMessage.ApplyParams(da.SelectCommand.Parameters);

                    if (aSqlConnection == null)
                    {
                        poolCon = DBConnectionPool.GetAvailableConnectionFromPool(1);
                        myConnection = poolCon.dbCon;
                    }

                    if (aSqlConnection.State == ConnectionState.Closed)
                        aSqlConnection.Open();

                    da.Fill(aMessage.DataSet, SqlEntityX);
                    aSqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw LastException;
            }
            finally
            {
                DBConnectionPool.SendConBackToPool(poolCon);
            }


        }
        private void AddCommonParams(SqlAction aSqlAction)
        {
            switch (aSqlAction)
            {
                case SqlAction.Insert:
                case SqlAction.Update:
                case SqlAction.Upsert:

                    break;
            }
        }
        protected void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.ErrorsOccurred)
            {
                e.Status = UpdateStatus.ErrorsOccurred;
            }
            else
            {
                if (e.Row.RowState != DataRowState.Deleted)
                    if (e.Command.Parameters.Contains(ReturnValueParamName))
                        if (((int)e.Command.Parameters[ReturnValueParamName].Value) != 0)
                            if (e.Row.Table.Columns.Contains(ShortSqlEntityX))
                            {
                                e.Row[ShortSqlEntityX] = e.Command.Parameters[ReturnValueParamName].Value;
                                e.Row.AcceptChanges();
                            }
            }
        }
        //****************************************************
        //****************************************************
        public virtual string SqlActionX(SqlAction aSqlAction)
        {
            return Enum.GetName(typeof(SqlAction), aSqlAction);
        }
        public virtual Exception LastException
        {
            get;
            set;
        }
    }
}
