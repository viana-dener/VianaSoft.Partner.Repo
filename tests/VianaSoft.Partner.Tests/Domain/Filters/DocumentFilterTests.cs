using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Filters
{
    public class DocumentFilterTests
    {
        [Fact(DisplayName = "Filter - Success")]
        [Trait("Domain", "Filter.Document")]
        public void Filter_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Filters.DocumentFilter();

            //Afirmações esperadas
            Assert.NotNull(model);
        }
    }
}
