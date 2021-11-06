namespace System.Net.Http.Xhtml.Api
{
    public class ResourceCollection<T> : Resource
    {
        public Resource<T>[] Data { get; set; }
    }
}
