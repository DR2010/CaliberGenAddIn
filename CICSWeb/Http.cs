using System;
using System.IO;
using System.Net;
using System.Text;

namespace CICSWeb.Net
{
    public static class Http
    {
        public static string GetCicsWebResponse(WrapperConfig data, Uri url)
        {
            string httpRequestString = Request.Build(data);
            return GetCicsWebResponse(httpRequestString, url,data.Wname);
        }
        public static string GetCicsWebResponse(
            string httpRequestString
            ,Uri url
            ,string service)
        {
            String httpResponseString;
            string proxy = string.Empty;
            var httpUrl = new Uri(url, service);

            DateTime startTime = DateTime.Now;
            var cicsWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
            
            //ServicePoint servicePoint = ServicePointManager.FindServicePoint(httpUrl);

            if (string.IsNullOrEmpty(proxy))
                cicsWebRequest.Proxy = null;
            else
            {
                cicsWebRequest.Proxy = new WebProxy(proxy, true);
//                servicePoint = ServicePointManager.FindServicePoint(((WebProxy)cicsWebRequest.Proxy).Address);
            }


            cicsWebRequest.Method = "POST";
            cicsWebRequest.ProtocolVersion = HttpVersion.Version10;
            cicsWebRequest.Headers.Add("UserID", Environment.UserName);
            cicsWebRequest.Headers.Add("Pragma", "No-cache");
            cicsWebRequest.Headers.Add("Application", "Avanade Prototype");
            cicsWebRequest.KeepAlive = false;

            cicsWebRequest.Timeout = 30 * 1000;


            var cicsWebStream = cicsWebRequest.GetRequestStream();
            var cicsWebRequestWriter = new StreamWriter(cicsWebStream);
            cicsWebRequestWriter.Write(httpRequestString);
            cicsWebRequestWriter.Close();

            HttpWebResponse cicsResponse = null;
            HttpWebResponse response = null;
            try
            {
                cicsResponse = (HttpWebResponse)cicsWebRequest.GetResponse();
                var cicsResponseStream = cicsResponse.GetResponseStream();
                var cicsReader = new StreamReader(cicsResponseStream);
                httpResponseString = cicsReader.ReadToEnd();
                cicsReader.Close();
            }
            catch (WebException webException)
            {
                response = webException.Response as HttpWebResponse;

                switch (webException.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        {
                            if (response == null)
                                throw new Exception("Communication with CICS failed due to a protocol error (server response was null)");

                            // Protocol error (we can still get a response from CICS).
                            string errorResponse = String.Empty;

                            if ((response.StatusCode == HttpStatusCode.Forbidden) ||
                                (response.StatusCode == HttpStatusCode.Unauthorized))
                            {
                                // Token has expired, or access is denied.
                                errorResponse =
                                    "CICS refused to perform the requested transaction (Forbidden / Unauthorized).\n";
                            }
                            else if (response.StatusCode == HttpStatusCode.BadGateway)
                            {
                                // Indicates proxy server cannot contact CICS.
                                errorResponse = "CICS proxy server reported a connection failure.\n";
                            }
                            else if (response.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                // Indicates proxy server cannot contact CICS.
                                errorResponse = "CICS has reported a system error.\n";
                            }

                            try
                            {
                                var responseStream = response.GetResponseStream();
                                if (responseStream != null)
                                {
                                    var responseReader = new StreamReader(responseStream);
                                    errorResponse += responseReader.ReadToEnd();
                                }
                            }
                            catch (ProtocolViolationException)
                            {
                                // There is no response stream.
                            }
                            catch (EndOfStreamException)
                            {
                                // Do nothing - error response will be filled out in the following statement.
                            }

                            // Grab status code in case this property throws after Close() is called.
                            HttpStatusCode statusCode = response.StatusCode;

                            if (errorResponse == String.Empty)
                                errorResponse = "No additional information is available.";

                            throw new Exception(
                                String.Format("Communication with CICS failed due to a protocol error (server response was '{0}').\n{1}",
                                              statusCode, errorResponse));
                        }
                    case WebExceptionStatus.Timeout:
                        {
                            // The operation timed out.
                            throw new Exception(
                                String.Format("No response was received from CICS after {0}ms. The operation has timed out.",
                                              cicsWebRequest.Timeout));
                        }
                    default:
                        {
                            // Other error (no response available).
                            throw new Exception(
                                String.Format("Communication with CICS failed.\n{0}\n{1}", webException.Status, webException.Message));
                        }
                }
            }
            finally
            {
                if (response != null)
                    response.Close();

                if (cicsResponse != null)
                    cicsResponse.Close();
            }

            // Remove null-terminator characters, if they are present.
            const string nullTerminator = "\x00";
            if (httpResponseString.IndexOf(nullTerminator) != -1)
            {
                var responseCleaner = new StringBuilder(httpResponseString);
                responseCleaner.Replace(nullTerminator, String.Empty);
                httpResponseString = responseCleaner.ToString();
            }


            if (httpResponseString.StartsWith("ABEND"))
                throw new Exception("The CICS request was terminated abnormally.");

            if (!httpResponseString.StartsWith("\t"))
            {
                throw new Exception(string.Format("Host error: {0}", httpResponseString));
            }

            return httpResponseString;
        }
    }
}
