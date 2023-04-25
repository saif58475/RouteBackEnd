using System;
using System.Collections.Generic;
using System.Text;

namespace Managment_System.Responce
{
    public interface IResponse<T> where T : class
    {
        public string Message { get; set; } 

        public int MessageCode { get; set; }

        public T Data { get; set; }
        bool Success { get; set; }
    }
}
