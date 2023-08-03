using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Filters
{
    public class ContactFilterTests
    {
        [Fact(DisplayName = "Filter - Success")]
        [Trait("Domain", "Filter.Contact")]
        public void Filter_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Filters.ContactFilter();

            //Afirmações esperadas
            Assert.NotNull(model);
        }
    }
}
