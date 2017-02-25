using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ResultsModel
{
    public class OneResult<T> : Result
    {
        public T Data { get; set; }       
    }
}
