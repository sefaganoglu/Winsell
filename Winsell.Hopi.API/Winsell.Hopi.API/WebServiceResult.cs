using System;
using System.Xml.Linq;

namespace Winsell.Hopi.API
{
    public class WebServiceResult
    {
        public bool IsSuccess { get; set; }
        public Exception Error { get; set; }
        public string ResultString { get; set; }
        public XDocument ResultXml { get; set; }

        public T Result<T>()
        {
            return default(T);
        }
    }
}
