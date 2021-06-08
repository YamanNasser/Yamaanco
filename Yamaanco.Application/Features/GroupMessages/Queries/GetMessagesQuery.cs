using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Message;

namespace Yamaanco.Application.Features.GroupMessages.Queries
{
    public class GetMessagesQuery : IRequest<PagedResponse<IEnumerable<MessageDto>>>
    {
        public string Target { get; set; }
        public int PageSize { get; set; } = 15;
        public int PageIndex { get; set; } = 0;
    }
}