using AutoMapper;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.App.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Partner, PartnerResponseViewModel>();
            CreateMap<Domain.Entities.Partner, PartnerRequestViewModel>().ReverseMap()
                .ForMember(x => x.IsEnable, o => o.MapFrom(y => true));

            CreateMap<FilterIViewModel, FilterBase>();
            CreateMap<DocumentFilterIViewModel, DocumentFilter>();

            CreateMap<ListPage<PartnerResponseViewModel>, ListPage<Domain.Entities.Partner>>();
            CreateMap<ListPage<Domain.Entities.Partner>, ListPage<PartnerResponseViewModel>>();
        }
    }
}
