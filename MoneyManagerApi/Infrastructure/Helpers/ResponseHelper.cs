using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Models.Paging;

namespace MoneyManagerApi.Infrastructure.Helpers
{
    public class ResponseHelper<T>
    {
        public static PagedResponse<T> CreatePagedResponse(IUriService uriService, PaginationQuery pagination,
            List<T> response, string controllerName)
        {
            var nextPage = pagination.PageNumber >= 1
                ? GetPageUri(uriService, pagination, pagination.PageNumber + 1, controllerName)
                : null;
            
            var previousPage = pagination.PageNumber -1 >= 1
                ? GetPageUri(uriService, pagination, pagination.PageNumber - 1, controllerName)
                : null;

            return new PagedResponse<T>
            {
                Data = response.Count <= pagination.PageSize? response : response.SkipLast(1),
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                NextPage = response.Count > pagination.PageSize ? nextPage : null,
                PreviousPage = previousPage
            };
        }

        private static string GetPageUri(IUriService uriService, PaginationQuery pagination,
            int currentPage, string controllerName)
        {
            return uriService.GetAllResources(new PaginationFilter(currentPage, pagination.PageSize), controllerName)
                .ToString();
        }
    }
}