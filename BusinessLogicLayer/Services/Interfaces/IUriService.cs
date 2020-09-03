using System;
using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUriService
    {
        Uri GetResource(int resourceId, string controllerName);
        Uri GetAllResources(PaginationFilter filter, string controllerName);
    }
}