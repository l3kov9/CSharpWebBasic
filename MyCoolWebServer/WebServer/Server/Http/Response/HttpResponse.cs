namespace WebServer.Server.Http.Response
{
    using System.Text;
    using WebServer.Server.Enums;

    public abstract class HttpResponse
    {
        private string statusCodeMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
        }
        
        public HttpHeaderCollection Headers { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        public override string ToString()
        {
            var response = new StringBuilder();

            var statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.statusCodeMessage}");

            response.AppendLine(this.Headers.ToString());
            response.AppendLine();
            
            return response.ToString();
        }
    }
}
