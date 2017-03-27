using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.Net
{
    class DELETERequest : Request
    {
        public DELETERequest() : base() { }
        public DELETERequest(String url) : base(url) { }

        public override WebRequest Prepare()
        {
            WebRequest request = base.Prepare();
            request.Method = "DELETE";
            return request;
        }
    }
}
