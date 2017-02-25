using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ResultsModel
{
    public abstract class Result
    {
        public bool IsOk { set; get; }
        public string Message { set; get; }
    }
}
