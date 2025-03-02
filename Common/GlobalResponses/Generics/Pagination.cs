using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GlobalResponses.Generics;

public  class Pagination<T>
{
    public List<T> Data { get; set; }
    public int TotalCount { get; set; }

    public Pagination()
    {
            
    }
}
