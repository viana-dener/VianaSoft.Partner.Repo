using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Filters
{
    public class PhoneFilterTests
    {
        [Fact(DisplayName = "Filter - Success")]
        [Trait("Domain", "Filter.Phone")]
        public void Filter_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Filters.PhoneFilter();

            //Afirmações esperadas
            Assert.NotNull(model);
        }
    }
}
