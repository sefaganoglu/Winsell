using System;
using System.Xml.Linq;

namespace Winsell.Hopi.API.HopiWS
{
    public class WebServiceResult
    {
        public bool IsSuccess { get; set; }
        public string ResultString { get; set; }
        public XDocument ResultXml { get; set; }

        public object ResultAttachment { get; set; }

        public T Result<T>()
        {
            return default(T);
        }
    }

    public class Param
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
