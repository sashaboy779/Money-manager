using System;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace BusinessLogicLayer.Services
{
    
    public class UriService : IUriService
    {
        private readonly string baseUri;

        public UriService(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri GetResource(int resourceId, string controllerName)
        {
            return new Uri($"{baseUri}{controllerName}/{resourceId}");
        }

        public Uri GetAllResources(PaginationFilter filter, string requestedResourceUri)
        {
            var uri = baseUri + requestedResourceUri.Substring(1);
            uri = QueryHelpers.AddQueryString(uri, nameof(filter.PageNumber), filter.PageNumber.ToString());
            uri = QueryHelpers.AddQueryString(uri, nameof(filter.PageSize), filter.PageSize.ToString());
            
            return new Uri(uri);
        }
    }
}