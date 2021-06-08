using System;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Common.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int ReceivedItemsCount { get; set; }

        public PagedResponse(T result, int pageIndex, int pageSize, int totalItemsCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Result = result;
            Succeeded = true;
            Message = totalItemsCount == 0 ? "No Result found." : $"{totalItemsCount} items found.";
            ErrorMessages = null;
            ReceivedItemsCount = totalItemsCount;
        }
    }
}