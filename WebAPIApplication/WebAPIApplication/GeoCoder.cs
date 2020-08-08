using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WebAPIApplication
{
    public class GeoCoder
    {
        [DataContract]
        public class GeocoderResponseContainer
        {
            [DataMember(Name = "response")]
            public GeocoderResponse Response { get; set; }
        }

        [DataContract]
        public class GeocoderResponse
        {
            [DataMember(Name = "GeoObjectCollection")]
            public GeoObjectCollection GeoObjectCollection { get; set; }
        }

        [DataContract]
        public class GeoObjectCollection
        {
            [DataMember(Name = "featureMember")]
            public FeatureMember[] FeatureMember { get; set; }
        }

        [DataContract]
        public class FeatureMember
        {
            [DataMember(Name = "GeoObject")]
            public GeoObject GeoObject { get; set; }
        }

        [DataContract]
        public class GeoObject
        {
            [DataMember(Name = "Point")]
            public Point Point { get; set; }
        }

        [DataContract]
        public class Point
        {
            [DataMember(Name = "pos")]
            public string GeoPoint { get; set; }
        }
    }
}
