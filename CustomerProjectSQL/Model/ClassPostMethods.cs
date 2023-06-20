using CustomerProjectSQL.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectSQL.Model
{
    internal class ClassPostMethods : IPostMethods
    {
        private readonly ClassDbConfig clsDbConfig = new ClassDbConfig();
        public (string res, string err) FileDelete(int custId)
        {
            var resOut = string.Empty;
            var errOut = string.Empty;
            var resCom = 0;
            try
            {
                var com = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DELETE_CUSTOMERS_E03"
                };
                var P_IDN = new SqlParameter
                {
                    ParameterName = "@P_IDN_IN",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = custId
                };
                com.Parameters.Add(P_IDN);
                var procRes = new SqlParameter
                {
                    ParameterName = "RES",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Output,
                    Size = 100
                };
                com.Parameters.Add(procRes);

                (resCom, errOut) = clsDbConfig.InsertDb(com);
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, errOut);
                resOut = procRes.Value.ToString();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            return (resOut, errOut);
        }

        public (string res, string custIdn, string err) FileOper(ClassControls cls)
        {
            var resOut = string.Empty;
            var errOut = string.Empty;
            var custIdnOut = string.Empty;
            var resOcm = 0;
            try
            {
                var com = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "OPER_CUSTOMERS_E03"
                };
                var P_IDN_IN = new SqlParameter
                {
                    ParameterName = "@P_IDN_IN",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = DBNull.Value
                };
                if (cls.custIdn != 0)
                    P_IDN_IN.Value = cls.custIdn;
                com.Parameters.Add(P_IDN_IN);

                var P_NAME = new SqlParameter
                {
                    ParameterName = "@P_NAME",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.name))
                    P_NAME.Value = cls.name;
                com.Parameters.Add(P_NAME);
                var P_SURNAME = new SqlParameter
                {
                    ParameterName = "@P_SURNAME",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.surname))
                    P_SURNAME.Value = cls.surname;
                com.Parameters.Add(P_SURNAME);
                var P_BIRTH_PLACE = new SqlParameter
                {
                    ParameterName = "@P_BIRTH_PLACE",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.birthPlace))
                    P_BIRTH_PLACE.Value = cls.birthPlace;
                com.Parameters.Add(P_BIRTH_PLACE);
                var P_BIRTH_DATE = new SqlParameter
                {
                    ParameterName = "@P_BIRTH_DATE",
                    SqlDbType = SqlDbType.Date,
                    Value = DBNull.Value
                };
                if (cls.birthDate != "  .  .")
                    P_BIRTH_DATE.Value = DateTime.Parse(cls.birthDate);
                com.Parameters.Add(P_BIRTH_DATE);
                var P_GENDER = new SqlParameter
                {
                    ParameterName = "@P_GENDER",
                    SqlDbType = SqlDbType.Int,
                    Value = DBNull.Value
                };
                if (cls.gender != -1)
                    P_GENDER.Value = cls.gender;
                com.Parameters.Add(P_GENDER);
                var P_DOC_NO = new SqlParameter
                {
                    ParameterName = "@P_DOC_NO",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.docNo))
                    P_DOC_NO.Value = cls.docNo;
                com.Parameters.Add(P_DOC_NO);
                var P_FIN_CODE = new SqlParameter
                {
                    ParameterName = "@P_FIN_CODE",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.finCode))
                    P_FIN_CODE.Value = cls.finCode;
                com.Parameters.Add(P_FIN_CODE);
                var P_PHONE_NUMBER = new SqlParameter
                {
                    ParameterName = "@P_PHONE_NUMBER",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.phoneNumber))
                    P_PHONE_NUMBER.Value = cls.phoneNumber;
                com.Parameters.Add(P_PHONE_NUMBER);
                var P_EMAIL = new SqlParameter
                {
                    ParameterName = "@P_EMAIL",
                    SqlDbType = SqlDbType.VarChar,
                    Value = DBNull.Value
                };
                if (!string.IsNullOrWhiteSpace(cls.email))
                    P_EMAIL.Value = cls.email;
                com.Parameters.Add(P_EMAIL);
                var procRes = new SqlParameter
                {
                    ParameterName = "@RES",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Output,
                    Size = 100
                };
                com.Parameters.Add(procRes);
                var P_IDN_OUT = new SqlParameter
                {
                    ParameterName = "@P_IDN_OUT",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                com.Parameters.Add(P_IDN_OUT);
                (resOcm, errOut) = clsDbConfig.InsertDb(com);
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, custIdnOut, errOut);
                resOut = procRes.Value.ToString();
                custIdnOut = P_IDN_OUT.Value.ToString();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            return (resOut, custIdnOut, errOut);
        }
    }
}
