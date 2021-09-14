namespace Microsoft.AspNetCore.Mvc
{
    public class ResourceCollection<T> : Resource
    {
        public Resource<T>[] Data { get; set; }
    }
}
