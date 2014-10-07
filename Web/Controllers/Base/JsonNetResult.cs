using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers.Base
{
    /*
     * http://wingkaiwan.com/2012/12/28/replacing-mvc-javascriptserializer-with-json-net-jsonserializer/
     */
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            Data = data;
            ContentType = contentType;
            ContentEncoding = contentEncoding;
            JsonRequestBehavior = behavior;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, Data);
                response.Write(sw.ToString());
            }
        }

        private JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Error };
            }
        }
    }
}