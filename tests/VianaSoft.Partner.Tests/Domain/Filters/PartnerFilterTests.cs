using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Filters
{
    public class PartnerFilterTests
    {
        [Fact(DisplayName = "Filter - Success")]
        [Trait("Domain", "Filter.Partner")]
        public void Filter_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Filters.PartnerFilter();

            //Afirmações esperadas
            Assert.NotNull(model);
        }
    }
}
