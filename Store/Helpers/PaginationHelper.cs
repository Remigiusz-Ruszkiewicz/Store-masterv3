using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Store.Contracts.V1;
using Store.Contracts.V1.Responses;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Helpers
{
    public class PaginationHelper
    {
        public static PageResponse<T> CreateResponse<T>(PaginationFilter paginationFilter,IEnumerable<T> data,HttpContext httpContext,string url)
        {
            var response = new PageResponse<T>(data);
            var absoluteUri = string.Concat(httpContext.Request.Scheme, "://", httpContext.Request.Host.ToUriComponent(), "/");
            var previosPage = QueryHelpers.AddQueryString(absoluteUri + url, "pageNumber", (paginationFilter.PageNumber - 1).ToString());
            previosPage = QueryHelpers.AddQueryString(previosPage, "pageSize", (paginationFilter.PageSize).ToString());
            var nextPage = QueryHelpers.AddQueryString(absoluteUri + url, "pageNumber", (paginationFilter.PageNumber + 1).ToString());
            nextPage = QueryHelpers.AddQueryString(nextPage, "pageSize", (paginationFilter.PageSize).ToString());
            response.PreviousPage = paginationFilter.PageNumber > 1 ? previosPage : null;
            response.NextPage = data.Any() ? nextPage : null;
            return response;
        }
    }
}
