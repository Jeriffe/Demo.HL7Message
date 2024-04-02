using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Demo.EnterpriseLibrarySamples
{
    /// <summary>
    /// http://aspalliance.com/articleViewer.aspx?aId=688&pId=-1#
    /// </summary>
    public class SampleDal
    {
        private static Database _db;

        static SampleDal()
        {
            _db = DatabaseFactory.CreateDatabase("DB");
        }
        public static int CheckReturnValueByReturnCaluse()
        {

            //https://stackoverflow.com/questions/6210027/calling-stored-procedure-with-return-value
            try
            {
                DbCommand cmd = _db.GetStoredProcCommand("dbo.[UP_CheckReturnValueByReturnCaluse]");
                _db.AddInParameter(cmd, "InputValue", DbType.Int32, 123);


                SqlParameter retval = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                retval.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retval);

                _db.ExecuteNonQuery(cmd);
                object o = cmd.Parameters["@ReturnValue"].Value;

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int CheckReturnValueBySelectCaluse()
        {
            try
            {
                using (DbCommand cmd = _db.GetStoredProcCommand("dbo.[usp_GetAuditNumberByStationIDAndReportNumber]"))
                {
                    _db.AddInParameter(cmd, "@StationID", DbType.Int32, 1);
                    _db.AddInParameter(cmd, "@CareUnitCode", DbType.String, "TPH");
                    _db.AddInParameter(cmd, "@ReportNumber", DbType.String, "277");

                    var result= (string)_db.ExecuteScalar(cmd);

                    return 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int CheckSingleValueByADONET()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[UP_CheckReturnValueByReturnCaluse]");
                    cmd.Parameters.Add(new SqlParameter("@InputValue", 5));
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = conn;

                    var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    var result = returnParameter.Value;

                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }
    }
}
