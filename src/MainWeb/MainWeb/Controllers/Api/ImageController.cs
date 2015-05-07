using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.IO;
using System.Net.Http.Headers;
using System.Web.Hosting;

namespace MainWeb.Controllers.Api
{

    [RoutePrefix("api/images")]
    public class ImageController : ApiController
    {
        
        [Route("file/{name}")]
        public HttpResponseMessage Get(string name)
        {
            var appData = HostingEnvironment.MapPath("~/App_Data");

            string fileName = Path.Combine(appData, string.Format("{0}", name));

            if (!File.Exists(fileName))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StreamContent(fileStream)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
        }
    }


}
