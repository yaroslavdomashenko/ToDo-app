using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Data
{
    public class ControllerResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
