using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.DataManagement
{
    public class DataManager
    {
#if DEVELOPMENT
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Development"].ConnectionString);
#else
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Development"].ConnectionString);
#endif

        public IDataParameter GetParameter()
        {
            return new SqlParameter();
        }

        public IDataRecord[] GetData(string storedProcedureName, params IDataParameter[] Parameters)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;

            cmd.Parameters.AddRange(Parameters);

            try
            {
                conn.Open();
                return cmd.ExecuteReader()
                   .Cast<IDataRecord>()
                   .ToArray();
            }
            finally
            {
                conn.Close();
            }
        }

        public int Execute(string storedProcedureName, params IDataParameter[] Parameters)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;

            cmd.Parameters.AddRange(Parameters);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public object Scalar(string storedProcedureName, params IDataParameter[] Parameters)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;

            cmd.Parameters.AddRange(Parameters);

            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}