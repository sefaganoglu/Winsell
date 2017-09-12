using System;
using System.Xml.Linq;

namespace Winsell.Hopi.API.HopiWS
{
    public class WebServiceResult
    {
        public bool IsSuccess { get; set; }
        public string ResultString { get; set; }
        public XDocument ResultXml { get; set; }

        public T Result<T>()
        {
            return default(T);
        }
    }
}
