using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GlobalResponses.Generics;

public class ResponseModel<T>:BaseResponseModel
{

    public T? Data { get; set; }
    public ResponseModel()
    {
        
    }
    public ResponseModel(List<string> messages):base(messages) 
    {
        
    }
}
