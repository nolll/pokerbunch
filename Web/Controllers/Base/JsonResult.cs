using System;
using System.Text;
using System.Web.Mvc;
using JetBrains.Annotations;
using Web.Common.Services;

namespace Web.Controllers.Base
{
    public class JsonResult : ActionResult
    {
        public JsonResult(object data, JsonRequestBehavior jsonRequestBehavior)
        {
            Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
        }

        [UsedImplicitly]
        public Encoding ContentEncoding { get; set; }

        [UsedImplicitly]
        public string ContentType { get; set; }

        [UsedImplicitly]
        public object Data { get; set; }

        [UsedImplicitly]
        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data == null)
                return;

            response.Write(JsonHelper.Serialize(Data));
        }
    }
}