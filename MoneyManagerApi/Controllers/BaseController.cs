using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Infrastructure.Helpers;
using MoneyManagerApi.Models.Paging;

namespace MoneyManagerApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        private readonly IUriService uriService;

        protected BaseController(IMapper mapper, IUriService uriService)
        {
            Mapper = mapper;
            this.uriService = uriService;
        }

        protected int GetUserId()
        {
            return Int32.Parse(HttpContext.User.Identity.Name);
        }

        protected PagedResponse<TModel> CreatePagedResponse<TModel>(PaginationQuery paginationQuery, 
            IEnumerable<TModel> response)
        {
            var requestedResourcePath = ControllerContext.HttpContext.Request.Path.Value;
            
            return ResponseHelper<TModel>.CreatePagedResponse(uriService, paginationQuery, response.ToList(),
                requestedResourcePath);
        }

        protected bool IsPagingSpecified(PaginationQuery paginationQuery)
        {
            return paginationQuery.PageNumber != 0 || paginationQuery.PageSize != 0;
        }
    }
}
