using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Dtos
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

         public static ResponseDto<T> Ok(string message , T? data = default) =>
            new() { Success = true, Data = data, Message = message };

        public static ResponseDto<T> Fail(string message) =>
            new() { Success = false, Message = message};
    }
}
