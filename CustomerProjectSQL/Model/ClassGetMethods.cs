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
    internal class ClassGetMethods : IGetMethods
    {
        private readonly ClassDbConfig clsDbConfig = new ClassDbConfig();
        public (DataTable dt, string err) GetCustomerByQuery()
        {
            var dtRes = new DataTable();
            var errRes = string.Empty;
            var query = $"SELECT T.IDN,T.NAME,T.SURNAME,T.BIRTH_PLACE,CONVERT(VARCHAR(10),T.BIRTH_DATE, 104) BIRTH_DATE,G.NAME GENDER_NAME,T.DOC_NO,T.FIN_CODE,T.PHONE_NUMBER,T.EMAIL,T.GENDER FROM CUSTOMERS_E03 T LEFT JOIN GENDER G ON T.GENDER = G.IDN ORDER BY T.IDN DESC";
            try
            {
                (dtRes, errRes) = clsDbConfig.FillDT(query);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }

        public (DataTable dt, string err) GetCustomerByFunction()
        {
            var dtOut =new DataTable();
            var errRes = string.Empty;
            try
            {
                var com = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM GET_CUSTOMER() ORDER BY IDN DESC"
                };

                (dtOut, errRes) = clsDbConfig.FillDT(com);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtOut, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtOut, errRes);
        }

        public (string fullName, string err) GetCustomerFullNameById(int custId)
        {
            var fullNameOut = string.Empty;
            var errRes = string.Empty;
            try
            {
                var com = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "SELECT DBO.GET_CUSTOMER_FULL_NAME(@P_CUST_ID)"
                };
                var P_IDN_IN = new SqlParameter
                {
                    ParameterName = "@P_CUST_ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = custId
                };
                com.Parameters.Add(P_IDN_IN);

                (fullNameOut, errRes) = clsDbConfig.FillValue(com);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (fullNameOut, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (fullNameOut, errRes);
        }

        public (DataTable dt, string err) GetGender()
        {
            var dtOut = new DataTable();
            var errOut = string.Empty;
            var query = "SELECT T.IDN, T.NAME FROM GENDER T";
            try
            {
                (dtOut, errOut) = clsDbConfig.FillDT(query);
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }

            return (dtOut, errOut);
        }

        public (DataTable dt, string err) GetCustomerByFunction(int custId)
        {
            var dtOut = new DataTable();
            var errRes = string.Empty;
            try
            {
                var com = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM GET_CUSTOMER_BY_ID(@P_CUST_ID) ORDER BY IDN DESC"
                };
                var P_IDN_IN = new SqlParameter
                {
                    ParameterName = "@P_CUST_ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = custId
                };
                com.Parameters.Add(P_IDN_IN);

                (dtOut, errRes) = clsDbConfig.FillDT(com);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtOut, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtOut, errRes);
        }

        public (DataTable dt, string err) GetCustomerByView(ClassControls cls)
        {
            var dtRes = new DataTable();
            var errRes = string.Empty;
            var query = $"SELECT * FROM VIEW_CUSTOMERS_E03 T WHERE";
            try
            {
                if (cls.custIdn != 0)
                    query += $" T.IDN={cls.custIdn}";
                if (!string.IsNullOrWhiteSpace(cls.name))
                    query += query.Contains("T.") ? $" AND LOWER(T.NAME) like LOWER('%{cls.name}%')" : $" LOWER(T.NAME) like LOWER('%{cls.name}%')";
                if (!string.IsNullOrWhiteSpace(cls.surname))
                    query += query.Contains("T.") ? $" AND LOWER(T.SURNAME) like LOWER('%{cls.surname}%')" : $" LOWER(T.SURNAME) like LOWER('%{cls.surname}%')";
                if (!string.IsNullOrWhiteSpace(cls.birthPlace))
                    query += query.Contains("T.") ? $" AND LOWER(T.BIRTH_PLACE) like LOWER('%{cls.birthPlace}%')" : $" LOWER(T.BIRTH_PLACE) like LOWER('%{cls.birthPlace}%')";
                if (cls.birthDate != "  .  .")
                    query += query.Contains("T.") ? $" AND T.BIRTH_DATE=TO_DATE('{cls.birthDate:dd.MM.yyyy}','DD.MM.YYYY')" : $" T.BIRTH_DATE=TO_DATE('{cls.birthDate:dd.MM.yyyy}','DD.MM.YYYY')";
                if (cls.gender != -1)
                    query += query.Contains("T.") ? $" AND T.GENDER={cls.gender}" : $" T.GENDER={cls.gender}";
                if (!string.IsNullOrWhiteSpace(cls.docNo))
                    query += query.Contains("T.") ? $" AND LOWER(T.DOC_NO) like LOWER('%{cls.docNo}%')" : $" LOWER(T.DOC_NO) like LOWER('%{cls.docNo}%')";
                if (!string.IsNullOrWhiteSpace(cls.finCode))
                    query += query.Contains("T.") ? $" AND LOWER(T.FIN_CODE) like LOWER('%{cls.finCode}%')" : $" LOWER(T.FIN_CODE) like LOWER('%{cls.finCode}%')";
                if (!string.IsNullOrWhiteSpace(cls.phoneNumber))
                    query += query.Contains("T.") ? $" AND LOWER(T.PHONE_NUMBER) like LOWER('%{cls.phoneNumber}%')" : $" LOWER(T.PHONE_NUMBER) like LOWER('%{cls.phoneNumber}%')";
                if (!string.IsNullOrWhiteSpace(cls.email))
                    query += query.Contains("T.") ? $" AND LOWER(T.EMAIL) like LOWER('%{cls.email}%')" : $" LOWER(T.EMAIL) like LOWER('%{cls.email}%')";
                (dtRes, errRes) = clsDbConfig.FillDT(query);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }
    }
}
