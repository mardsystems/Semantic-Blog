using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Text.Json;

namespace Microsoft.AspNetCore.Mvc.Formatters
{
    public class JsonApiOutputFormatter : SystemTextJsonOutputFormatter
    {
        public JsonApiOutputFormatter(JsonSerializerOptions jsonSerializerOptions)
            : base(jsonSerializerOptions)
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/vnd.api+json"));

            SupportedEncodings.Add(Encoding.UTF8);

            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return true;

            //var result = type == typeof(Resource) ||
            //    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ResourceCollection<>) ||
            //    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ResourceForm<>) ||
            //    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Resource<>);

            //return result;
        }
    }
}
