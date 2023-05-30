using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Models
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public bool isSuccess { get; set; }
        public string[]? Messages { get; set; }
        public object? Data { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
