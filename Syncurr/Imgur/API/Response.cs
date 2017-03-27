using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.API
{
    [DataContract]
    class Response<T>
    {
        [DataMember]
        public T data { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public bool success { get; set; }

        public Response() { }
    }
}
