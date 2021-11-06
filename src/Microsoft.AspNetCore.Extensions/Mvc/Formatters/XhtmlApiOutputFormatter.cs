using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Xhtml.Api;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml;

namespace Microsoft.AspNetCore.Mvc.Formatters
{
    public class XhtmlApiOutputFormatter : XmlSerializerOutputFormatter
    {
        public XhtmlApiOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/vnd.api+xhtml"));

            SupportedEncodings.Add(Encoding.UTF8);

            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            var result = type == typeof(Resource) ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ResourceCollection<>) ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ResourceForm<>) ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Resource<>);

            return result;
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            try
            {
                
                var enconding = base.SelectCharacterEncoding(context);

                var settings = new XmlWriterSettings();

                settings.Encoding = enconding;

                settings.Async = true;

                var response = context.HttpContext.Response;

                var xmlWriter = CreateXmlWriter(context, new StreamWriter(response.Body), settings);

                var resource = context.Object as Resource;

                //

                var html = new TagBuilder("html");

                html.Attributes.Add("lang", "pt-br");

                //

                var head = new TagBuilder("head");

                html.InnerHtml.AppendHtml(head);

                //

                var meta1 = new TagBuilder("meta");

                head.InnerHtml.AppendHtml(meta1);

                meta1.Attributes.Add("charset", "utf-8");

                //

                var meta2 = new TagBuilder("meta");

                head.InnerHtml.AppendHtml(meta2);

                meta2.Attributes.Add("name", "viewport");

                meta2.Attributes.Add("content", "width=device-width, initial-scale=1.0");

                //

                var title = new TagBuilder("title");

                head.InnerHtml.AppendHtml(title);

                title.InnerHtml.Append(resource.Title);

                //

                var link1 = new TagBuilder("link");

                head.InnerHtml.AppendHtml(link1);

                link1.Attributes.Add("rel", "stylesheet");

                link1.Attributes.Add("href", "https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.4.1/semantic.min.css");

                //

                var link2 = new TagBuilder("link");

                head.InnerHtml.AppendHtml(link2);

                link2.Attributes.Add("rel", "stylesheet");

                link2.Attributes.Add("href", "/css/api.css");

                //

                var body = new TagBuilder("body");

                html.InnerHtml.AppendHtml(body);

                body.AddCssClass("ui basic segment");

                //

                var div = new TagBuilder("div");

                body.InnerHtml.AppendHtml(div);

                div.AddCssClass("ui menu");

                //div.Attributes.Add("href", resource.HRef);

                SerializeResource(div, resource);

                //

                var resourceType = resource.GetType();

                if (resourceType.IsGenericType && resourceType.GetGenericTypeDefinition() == typeof(ResourceCollection<>))
                {
                    var data = resourceType.GetProperty("Data").GetValue(resource, null) as IEnumerable;

                    var dataItemType = resourceType.GetGenericArguments()[0];

                    var table = new TagBuilder("table");

                    body.InnerHtml.AppendHtml(table);

                    table.AddCssClass("ui striped table");

                    table.Attributes.Add("href", resource.HRef);


                    var thead = new TagBuilder("thead");

                    table.InnerHtml.AppendHtml(thead);


                    var itemProperties = dataItemType.GetProperties();

                    var tr = new TagBuilder("tr");

                    thead.InnerHtml.AppendHtml(tr);

                    foreach (var itemProperty in itemProperties)
                    {
                        if (itemProperty.PropertyType.IsArray || itemProperty.PropertyType.IsGenericType)
                        {
                            continue;
                        }


                        var infrastructureAttribute = itemProperty.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(InfrastructureAttribute)) as InfrastructureAttribute;

                        if (infrastructureAttribute != default(InfrastructureAttribute))
                        {
                            continue;
                        }


                        var th = new TagBuilder("th");

                        tr.InnerHtml.AppendHtml(th);

                        th.AddCssClass(itemProperty.Name);

                        var descriptionAttribute = itemProperty.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

                        if (descriptionAttribute == default(DescriptionAttribute))
                        {
                            th.InnerHtml.AppendHtml(itemProperty.Name);
                        }
                        else
                        {
                            th.InnerHtml.AppendHtml(descriptionAttribute.Description);
                        }
                    }

                    var thActions = new TagBuilder("th");

                    tr.InnerHtml.AppendHtml(thActions);

                    thActions.InnerHtml.Append("Ações");

                    foreach (Resource innerResource in data)
                    {
                        SerializeResourceCollection(table, innerResource, dataItemType);
                    }
                }
                else if (resourceType.IsGenericType && resourceType.GetGenericTypeDefinition() == typeof(ResourceForm<>))
                {
                    var action = resourceType.GetProperty("Action").GetValue(resource, null).ToString();

                    var method = resourceType.GetProperty("Method").GetValue(resource, null).ToString();

                    var data = resourceType.GetProperty("Data").GetValue(resource, null);

                    var dataType = resourceType.GetGenericArguments()[0];

                    var form = new TagBuilder("form");

                    body.InnerHtml.AppendHtml(form);

                    form.AddCssClass("ui form");

                    //form.Attributes.Add("href", resource.HRef);

                    form.Attributes.Add("method", method);

                    form.Attributes.Add("action", action);

                    SerializeResourceForm(form, resource, data, dataType);

                    var submit = new TagBuilder("input");

                    form.InnerHtml.AppendHtml(submit);

                    submit.AddCssClass("ui button");

                    submit.Attributes.Add("type", "submit");

                    submit.Attributes.Add("value", "Submit");
                }
                else if (resourceType.IsGenericType && resourceType.GetGenericTypeDefinition() == typeof(Resource<>))
                {
                    var data = resourceType.GetProperty("Data").GetValue(resource, null);

                    var dataType = resourceType.GetGenericArguments()[0];

                    var list = new TagBuilder("div");

                    body.InnerHtml.AppendHtml(list);

                    list.AddCssClass("ui list");

                    //list.Attributes.Add("href", resource.HRef);

                    SerializeResource(list, resource, data, dataType);
                }

                //

                var stringWriter = new StringWriter();

                html.WriteTo(stringWriter, HtmlEncoder.Default);

                await xmlWriter.WriteRawAsync(stringWriter.ToString());

                //

                await xmlWriter.FlushAsync();

                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                var _ = ex.Message;

                throw;
            }
        }

        private void SerializeResourceCollection(TagBuilder parent, Resource innerResource, Type dataItemType)
        {
            var tr = new TagBuilder("tr");

            parent.InnerHtml.AppendHtml(tr);

            tr.AddCssClass("resource");

            var innerResourceType = innerResource.GetType();

            var dataItem = innerResourceType.GetProperty("Data").GetValue(innerResource, null);

            //var dataItemType = innerResourceType.GetGenericArguments()[0];

            var itemProperties = dataItemType.GetProperties();

            foreach (var itemProperty in itemProperties)
            {
                if (itemProperty.PropertyType.IsArray || itemProperty.PropertyType.IsGenericType)
                {
                    continue;
                }


                var infrastructureAttribute = itemProperty.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(InfrastructureAttribute)) as InfrastructureAttribute;

                if (infrastructureAttribute != default(InfrastructureAttribute))
                {
                    continue;
                }


                var td = new TagBuilder("td");

                tr.InnerHtml.AppendHtml(td);

                td.AddCssClass(itemProperty.Name);

                var value = itemProperty.GetValue(dataItem);

                td.InnerHtml.AppendHtml(value?.ToString());
            }

            var tdActions = new TagBuilder("td");

            tr.InnerHtml.AppendHtml(tdActions);

            foreach (var link in innerResource.Links)
            {
                var item = new TagBuilder("a");

                tdActions.InnerHtml.AppendHtml(item);

                item.AddCssClass("item");

                item.Attributes.Add("rel", link.Rel);

                item.Attributes.Add("href", link.HRef);

                item.Attributes.Add("type", "text/xhtml");

                item.InnerHtml.Append(link.Text);
            }
        }

        private void SerializeResourceForm(TagBuilder parent, Resource resource, object data, Type dataType)
        {
            var properties = dataType.GetProperties();

            foreach (var property in properties)
            {
                var infrastructureAttribute = property.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(InfrastructureAttribute)) as InfrastructureAttribute;

                if (infrastructureAttribute != default(InfrastructureAttribute))
                {
                    continue;
                }


                var div = new TagBuilder("div");

                parent.InnerHtml.AppendHtml(div);

                div.AddCssClass("field");

                //

                var label = new TagBuilder("label");

                div.InnerHtml.AppendHtml(label);

                label.InnerHtml.AppendHtml(property.Name);

                //

                var input = new TagBuilder("input");

                div.InnerHtml.AppendHtml(input);

                input.Attributes.Add("type", "text");

                input.Attributes.Add("name", property.Name);

                input.Attributes.Add("placeholder", property.Name);

                var value = property.GetValue(data);

                input.Attributes.Add("value", value?.ToString());
            }

            //foreach (var link in resource.Links)
            //{
            //    var item = new TagBuilder("a");

            //    parent.InnerHtml.AppendHtml(item);

            //    item.AddCssClass("item");

            //    item.Attributes.Add("rel", link.Rel);

            //    item.Attributes.Add("href", link.HRef);

            //    item.Attributes.Add("type", "text/xhtml");

            //    item.InnerHtml.Append(link.Text);
            //}
        }

        private void SerializeResource(TagBuilder parent, Resource resource, object data, Type dataType)
        {
            SerializeData(parent, data, dataType);
        }

        private static void SerializeData(TagBuilder parent, object data, Type dataType)
        {
            var properties = dataType.GetProperties();

            foreach (var property in properties)
            {
                var infrastructureAttribute = property.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(InfrastructureAttribute)) as InfrastructureAttribute;

                if (infrastructureAttribute != default(InfrastructureAttribute))
                {
                    continue;
                }


                var value = property.GetValue(data);

                var item = new TagBuilder("div");

                parent.InnerHtml.AppendHtml(item);

                item.AddCssClass("item");

                item.AddCssClass(property.Name);


                var header = new TagBuilder("div");

                item.InnerHtml.AppendHtml(header);

                header.AddCssClass("header");

                var descriptionAttribute = property.GetCustomAttributes().FirstOrDefault(p => p.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (descriptionAttribute == default(DescriptionAttribute))
                {
                    header.InnerHtml.AppendHtml(property.Name);
                }
                else
                {
                    header.InnerHtml.AppendHtml(descriptionAttribute.Description);
                }

                if (value is IEnumerable && property.PropertyType.IsGenericType)
                {
                    var itemType = property.PropertyType.GenericTypeArguments[0];


                    var list = new TagBuilder("div");

                    item.InnerHtml.AppendHtml(list);

                    list.AddCssClass("list");


                    foreach (var item2 in value as IEnumerable)
                    {
                        var header2 = new TagBuilder("div");

                        list.InnerHtml.AppendHtml(header2);

                        header2.AddCssClass("header");

                        header2.InnerHtml.AppendHtml($"{itemType.Name} #");


                        var item3 = new TagBuilder("div");

                        list.InnerHtml.AppendHtml(item3);

                        item3.AddCssClass("item");

                        item3.AddCssClass(itemType.Name);


                        var list2 = new TagBuilder("div");

                        item3.InnerHtml.AppendHtml(list2);

                        list2.AddCssClass("list");

                        SerializeData(list2, item2, itemType);
                    }
                }
                else
                {
                    item.InnerHtml.AppendHtml(value?.ToString());
                }
            }
        }

        private void SerializeResource(TagBuilder parent, Resource resource)
        {
            parent.Attributes.Add("href", resource.HRef);

            foreach (var link in resource.Links)
            {
                var item = new TagBuilder("a");

                parent.InnerHtml.AppendHtml(item);

                item.AddCssClass("item");

                item.Attributes.Add("rel", link.Rel);

                item.Attributes.Add("href", link.HRef);

                item.Attributes.Add("type", "text/xhtml");

                item.InnerHtml.Append(link.Text);
            }
        }
    }
}
