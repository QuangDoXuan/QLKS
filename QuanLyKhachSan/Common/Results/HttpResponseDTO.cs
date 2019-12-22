using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpResponseDTO<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public HttpResponseDTO()
        {

        }

        public HttpResponseDTO(int code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
