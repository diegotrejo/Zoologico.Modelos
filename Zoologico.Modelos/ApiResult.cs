using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoologico.Modelos
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ApiResult Ok(object data)
        {
            return new ApiResult
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResult Fail(string message)
        {
            return new ApiResult
            {
                Success = false,
                Message = message
            };
        }
    }
}
