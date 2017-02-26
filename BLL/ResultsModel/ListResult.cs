using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ResultsModel
{
    public class ListResult<T> : Result
    {
        public ListResult()
        {
            ListData = new List<T>();
        }
        public IList<T> ListData { get; set; }
    }
}
