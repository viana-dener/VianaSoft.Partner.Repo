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

            CreateMap<Domain.Entities.Contact, ContactResponseViewModel>();
            CreateMap<Domain.Entities.Contact, ContactRequestViewModel>().ReverseMap()
                .ForMember(x => x.IsEnable, o => o.MapFrom(y => true));

            CreateMap<Domain.Entities.Phone, PhoneResponseViewModel>();
            CreateMap<Domain.Entities.Phone, PhoneRequestViewModel>().ReverseMap()
                .ForMember(x => x.IsEnable, o => o.MapFrom(y => true));

            CreateMap<PartnerFilterViewModel, PartnerFilter>();
            CreateMap<ContactFilterViewModel, ContactFilter>();
            CreateMap<PhoneFilterViewModel, PhoneFilter>();
            CreateMap<DocumentFilterViewModel, DocumentFilter>();

            CreateMap<ListPage<PartnerResponseViewModel>, ListPage<Domain.Entities.Partner>>();
            CreateMap<ListPage<ContactResponseViewModel>, ListPage<Domain.Entities.Contact>>();
            CreateMap<ListPage<PhoneResponseViewModel>, ListPage<Domain.Entities.Phone>>();

            CreateMap<ListPage<Domain.Entities.Partner>, ListPage<PartnerResponseViewModel>>();
            CreateMap<ListPage<Domain.Entities.Contact>, ListPage<ContactResponseViewModel>>();
            CreateMap<ListPage<Domain.Entities.Phone>, ListPage<PhoneResponseViewModel>>();
        }
    }
}
