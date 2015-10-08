using Syncurr.Imgur.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.API
{
    [DataContract]
    abstract class Model<T> where T : Model<T>
    {
        public static T GetItem(String id)
        {
            Request request = new Request("https://api.imgur.com/3/" + id);
            request.authorized = true;
            WebResponse response = request.Send();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Response<T>));
            Response<T> item = (Response<T>)serializer.ReadObject(response.GetResponseStream());
            return item.data;
        }
    }
}
