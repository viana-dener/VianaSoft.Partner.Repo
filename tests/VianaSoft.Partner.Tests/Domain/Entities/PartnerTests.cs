using FizzWare.NBuilder.Extensions;
using Moq;
using System;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Entities
{
    public class PartnerTests
    {
        #region Properties

        private readonly string _document = "16410455000115";
        private readonly string _name = "VianaSoft Ltda.";
        private readonly string _description = "Desenvolvimento de sistemas";
        private readonly Mock<IAspNetUser> _aspNetUser;

        #endregion

        #region Builders

        public PartnerTests()
        {
            _aspNetUser = new Mock<IAspNetUser>();
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Builder - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void Builder_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("16410455000115", model.Document);
            Assert.Equal("VianaSoft Ltda.", model.Name);
            Assert.Equal("Desenvolvimento de sistemas", model.Description);
        }

        [Fact(DisplayName = "GetByDocument - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void GetByDocument_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("16410455000115", model.GetByDocument());
        }

        [Fact(DisplayName = "AddCreateBy - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void AddCreateBy_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);
            model.AddCreateBy(_aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("16410455000115", model.Document);
            Assert.Equal("VianaSoft Ltda.", model.Name);
            Assert.Equal("Desenvolvimento de sistemas", model.Description);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void Update_Sucesso()
        {
            //Cenário
            var name = "Casafy Ltda.";
            var description = "Imóveis perfeitos para você!";

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);
            model.Update(name, description, "Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("16410455000115", model.Document);
            Assert.Equal("Casafy Ltda.", model.Name);
            Assert.Equal("Imóveis perfeitos para você!", model.Description);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void Enable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);
            model.Enable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.True(model.IsEnable);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void Disable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);
            model.Disable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.False(model.IsEnable);
        }

        [Fact(DisplayName = "Delete - Success")]
        [Trait("Domain", "Entities.Partner")]
        public void Delete_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Partner(_document, _name, _description, _aspNetUser.Name);
            model.Delete("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.True(model.IsExclude);
        }

        #endregion
    }
}
