using CustomerProjectSQL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectSQL.View
{
    internal interface IGetMethods
    {
        (DataTable dt, string err) GetGender();
        (DataTable dt, string err) GetCustomerByQuery();
        (DataTable dt, string err) GetCustomerByFunction();
        (DataTable dt, string err) GetCustomerByFunction(int custId);
        (string fullName, string err) GetCustomerFullNameById(int custId);
        (DataTable dt, string err) GetCustomerByView(ClassControls cls);
    }
}
