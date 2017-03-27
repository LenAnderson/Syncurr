using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.API
{
    [DataContract]
    class Album : Model<Album>
    {
        [DataMember]
        public String id { get; set; }
        [DataMember]
        public String title { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public String link { get; set; }
        [DataMember]
        public Image[] images { get; set; }


        public static Album Get(String id)
        {
            return GetItem("album/" + id);
        }
        public static String GetLink(String id)
        {
            return "http://imgur.com/a/" + id;
        }


        public Album() { }
    }
}
