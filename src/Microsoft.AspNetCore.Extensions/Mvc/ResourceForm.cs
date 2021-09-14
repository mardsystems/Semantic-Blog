﻿namespace Microsoft.AspNetCore.Mvc
{
    public class ResourceForm<T> : Resource<T>
    {
        public string Method { get; set; }

        public string Action { get; set; }
    }
}
