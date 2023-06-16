using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Hospital_Web_API.Error
{
    public class ResponseError
    {
        public class Response<T>
        {
            public bool Status { get; set; } // признак выполнения (true - успех, false - провал)
            public T Result { get; set; } // результат выполнения
            public string ErrorMessage { get; set; }
        }
    }
}