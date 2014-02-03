using System;
using System.Collections.Specialized;
using System.Net.Http;
using Dev2.Runtime.WebServer.Responses;

namespace Dev2.Runtime.WebServer
{
    public class WebServerContext : ICommunicationContext, IDisposable
    {
        readonly HttpRequestMessage _request;

        public WebServerContext(HttpRequestMessage request, NameValueCollection requestPaths)
        {
            _request = request;
            ResponseMessage = request.CreateResponse();
            Request = new WebServerRequest(request, requestPaths);
            Response = new WebServerResponse(ResponseMessage);
        }

        public HttpResponseMessage ResponseMessage { get; private set; }

        public ICommunicationRequest Request { get; private set; }
        public ICommunicationResponse Response { get; private set; }

        public void Send(IResponseWriter response)
        {
            VerifyArgument.IsNotNull("response", response);
            response.Write(this);
        }

        public NameValueCollection FetchHeaders()
        {
            var result = new NameValueCollection();
            foreach(var header in _request.Headers)
            {
                result.Add(header.Key, string.Join("; ", header.Value));
            }
            return result;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if(Request != null && Request.InputStream != null)
                {
                    Request.InputStream.Close();
                    Request.InputStream.Dispose();
                }
                if(Response.Response != null)
                {
                    ResponseMessage.Dispose();
                    Response.Response.Dispose();
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                // best effort to clean up ;)
            }
        }

        #endregion
    }
}