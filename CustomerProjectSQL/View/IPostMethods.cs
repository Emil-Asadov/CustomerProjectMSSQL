using CustomerProjectSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectSQL.View
{
    internal interface IPostMethods
    {
        (string res, string err) FileDelete(int custId);
        (string res, string custIdn, string err) FileOper(ClassControls cls);
    }
}
