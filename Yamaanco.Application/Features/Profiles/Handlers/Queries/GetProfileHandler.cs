using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.Profiles.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.Profiles.Handlers.Queries
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, PagedResponse<IEnumerable<ProfileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProfileDto>>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork
                .ProfileRepository
                .GetProfileList(request.Filter, request.SortColumn, request.SortColumnDirection, request.PageIndex, request.PageSize);
            var result = _mapper.Map<IEnumerable<ProfileDto>>(response).ToList();
            return new PagedResponse<IEnumerable<ProfileDto>>(result, request.PageIndex, request.PageSize, result.Count);
        }
    }
}