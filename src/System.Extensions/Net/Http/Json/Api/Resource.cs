using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http.Json.Api
{
    /// <summary>
    /// Document's "top level".
    /// </summary>
    public class Document
    {
        public Meta Meta { get; set; }

        public PrimaryData Data { get; set; }

        public Error[] Errors { get; set; }

        //

        public JsonApi JsonApi { get; set; }

        public Link[] Links { get; set; }

        public Resource[] Included { get; set; }
    }

    public class PrimaryData
    {

    }

    public class Meta
    {

    }

    public class Error
    {

    }

    //

    public class JsonApi
    {

    }

    public class Link
    {

    }

    public class ResourceIdentifier
    {
        public string Type { get; set; }

        public string Id { get; set; }
    }

    public class Resource
    {
        public string Type { get; set; }

        public string Id { get; set; }

        //

        public Attribute[] Attributes { get; set; }

        public Relationshp[] Relationships { get; set; }

        //

        public Link[] Links { get; set; }

        public Meta Meta { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        
        public string Value { get; set; }
    }

    public class Relationshp
    {
        public object Content { get; set; }
    }
}
