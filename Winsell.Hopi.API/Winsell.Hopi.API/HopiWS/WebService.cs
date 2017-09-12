using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Winsell.Hopi.API.HopiWS
{
    public class WebService
    {
        public string Url { get; set; }

        public string MethodName { get; set; }

        public string MethodTargetName { get; set; }

        public string XmlRequestContent { get; set; }

        public ICredentials Credentials { get; set; }

        public XDocument ResponseSoap { get; set; }

        public XDocument ResultXml { get; set; }

        public string ResultString { get; set; }

        public Dictionary<string, Dictionary<string, string>> Headers { get; set; }

        public Dictionary<string, string> Params = new Dictionary<string, string>();


        public WebServiceResult Invoke(bool encode)
        {
            return Execute(encode);
        }

        private WebServiceResult Execute(bool encode)
        {
            try
            {
                if (string.IsNullOrEmpty(Url))
                    return new WebServiceResult
                    {
                        IsSuccess = false,
                        ResultString = "{'faultcode' : 'Error', 'faultstring' : 'Url boş olamaz.', 'detail' : {'ServiceError' : {'code' : 'ERROR', 'description' : 'Url boş olamaz.'}}}"
                    };

                var xmlContent = new StringBuilder();
                xmlContent.AppendLine(@"<{0}:Envelope xmlns:{0}=""http://schemas.xmlsoap.org/soap/envelope/"" {1}>");
                xmlContent.AppendLine(@"<{0}:Header>");
                xmlContent.AppendLine(@"<wsse:Security soapenv:mustUnderstand=""1""");
                xmlContent.AppendLine(@"xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""");
                xmlContent.AppendLine(@"xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">");
                xmlContent.AppendLine(@"<wsse:UsernameToken>");
                xmlContent.AppendFormat(@"<wsse:Username>{0}</wsse:Username>{1}", Genel.kullaniciKoduWS, Environment.NewLine);
                xmlContent.AppendLine(@"<wsse:Password");
                xmlContent.AppendFormat(@"Type = ""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{0}</wsse:Password>{1}", Genel.sifreWS, Environment.NewLine);
                xmlContent.AppendLine(@"</wsse:UsernameToken>");
                xmlContent.AppendLine(@"</wsse:Security>");
                xmlContent.AppendLine(@"</{0}:Header>");
                xmlContent.AppendLine(@"<{0}:Body>");
                if (!string.IsNullOrEmpty(MethodTargetName))
                    xmlContent.AppendFormat(@"<{0}:{1}>{3}{2}</{0}:{1}>{3}", "urn", "{2}", "{3}", Environment.NewLine);
                else
                    xmlContent.AppendLine(@"<pos:{2}>{3}</pos:{2}>");
                xmlContent.AppendLine(@"</{0}:Body>");
                xmlContent.AppendLine(@"</{0}:Envelope>");

                var body = new StringBuilder();
                foreach (var param in Params)
                {
                    if (encode)
                        body.AppendFormat("<pos:{0}>{1}</pos:{0}>{2}", HttpUtility.UrlEncode(param.Key), HttpUtility.UrlEncode(param.Value.Trim()), Environment.NewLine);
                    else
                        body.AppendFormat("<pos:{0}>{1}</pos:{0}>{2}", param.Key, param.Value.Trim(), Environment.NewLine);
                }

                var content = string.Format(xmlContent.ToString(), "soapenv",
                                                                   @"xmlns:pos=""http://bird.kartaca.com/xmlschema/pos""",
                                                                   MethodName + "Request",
                                                                   Environment.NewLine + body);

                var req = (HttpWebRequest)WebRequest.Create(Url);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                req.Headers.Add("SOAPAction", "\"\"");
                req.ContentType = "text/xml;charset=\"utf-8\"";
                req.Accept = "text/xml";
                req.Method = "POST";
                req.ProtocolVersion = HttpVersion.Version11;
                req.Credentials = Credentials;

                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                        stmw.Write(content);
                }

                using (var response = req.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            using (var responseReader = new StreamReader(stream))
                            {
                                string result = responseReader.ReadToEnd();
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ResponseSoap = XDocument.Parse(result);
                                    ExtractResult();
                                }
                            }
                        }
                    }
                }

                return new WebServiceResult
                {
                    IsSuccess = true,
                    ResultString = ResultString,
                    ResultXml = ResultXml
                };
            }
            catch (WebException ex)
            {
                WebResponse errResp = ex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    ResponseSoap = XDocument.Parse(text);
                    ExtractResult();
                }

                return new WebServiceResult
                {
                    IsSuccess = false,
                    ResultString = ResultString,
                    ResultXml = ResultXml
                };
            }
            catch (Exception ex)
            {
                return new WebServiceResult
                {
                    IsSuccess = false,
                    ResultString = "{'faultcode' : 'Error', 'faultstring' : '" + ex.Message + "', 'detail' : {'ServiceError' : {'code' : 'ERROR', 'description' : '" + ex.Message + "'}}}"
                };
            }
        }

        private void ExtractResult()
        {
            var document = Utils.RemoveNamespaces(ResponseSoap);
            XElement result = document.XPathSelectElement(string.Format("//{0}Response", MethodName));

            if (result == null)
                result = document.XPathSelectElement(string.Format("//Fault"));

            if (result == null)
                return;

            ResultString += XElementToparla(result);

            if (result.FirstNode.NodeType == XmlNodeType.Element)
            {
                ResultXml = XDocument.Parse(result.FirstNode.ToString());
                ResultXml = Utils.RemoveNamespaces(ResultXml);
            }
            else
            {
                ResultString = result.FirstNode.ToString();
                ResultXml = XDocument.Parse("<root>" + ResultString + "</root>");
            }
        }

        private string XElementToparla(XElement xElement)
        {
            string resultString = "{";

            List<XElement> xElements = xElement.Elements().Select(x => x).ToList();
            for (int i = 0; i < xElements.Count; i++)
            {
                XElement xEPrev = i != 0 ? xElement.Elements().Select(x => x).ToList()[i - 1] : null;
                XElement xE = xElements[i];
                XElement xENext = xElements.Count - 1 >= i + 1 ? xElement.Elements().Select(x => x).ToList()[i + 1] : null;
                try
                {
                    if (xE.Elements().Count() > 0)
                    {
                        if (resultString.Trim().Length > 1) resultString = resultString.Trim() + ", ";

                        if (xEPrev == null || (xEPrev != null && xE.Name.LocalName != xEPrev.Name.LocalName))
                            resultString += "'" + xE.Name.LocalName + "' : " + (xENext != null && xE.Name.LocalName == xENext.Name.LocalName ? "[" : "") + XElementToparla(xE);
                        else
                            resultString += XElementToparla(xE);
                    }
                    else
                    {
                        if (resultString.Trim().Length > 1) resultString = resultString.Trim() + ", ";

                        if (xEPrev == null || (xEPrev != null && xE.Name.LocalName != xEPrev.Name.LocalName))
                            resultString += "'" + xE.Name.LocalName + "' : " + (xENext != null && xE.Name.LocalName == xENext.Name.LocalName ? "[" : "") + "'" + xElement.Element(xE.Name.LocalName).Value + "'";
                        else
                            resultString += "'" + xElement.Element(xE.Name.LocalName).Value + "'";
                    }

                    resultString += (xEPrev != null && xE.Name.LocalName == xEPrev.Name.LocalName && (xENext == null || (xENext != null && xE.Name.LocalName != xENext.Name.LocalName)) ? "]" : "");
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            resultString += "}";

            return resultString;
        }

        #region CALL WEB SERVICE ALTERNATIF

        public static void CallWebService(string uri, string action)
        {
            var _url = uri;
            var _action = action;

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><HelloWorld xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><int1 xsi:type=""xsd:integer"">12</int1><int2 xsi:type=""xsd:integer"">32</int2></HelloWorld></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        #endregion

    }
}
