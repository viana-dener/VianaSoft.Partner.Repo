using Moq;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Entities
{
    public class ContactTests
    {
        #region Properties

        private readonly Guid _partnerId = Guid.NewGuid();
        private readonly string _name = "Dener Viana";
        private readonly string _businessEmail = "viana.dener@vianasoft.com.br";
        private readonly string _personalEmail = "viana.dener@gmail.com";
        private readonly Mock<IAspNetUser> _aspNetUser;

        #endregion

        #region Builders

        public ContactTests()
        {
            _aspNetUser = new Mock<IAspNetUser>();
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Builder - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void Builder_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("Dener Viana", model.Name);
            Assert.Equal("viana.dener@vianasoft.com.br", model.BusinessEmail);
            Assert.Equal("viana.dener@gmail.com", model.PersonalEmail);
        }

        [Fact(DisplayName = "AddCreateBy - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void AddCreateBy_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);
            model.AddCreateBy(_aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("Dener Viana", model.Name);
            Assert.Equal("viana.dener@vianasoft.com.br", model.BusinessEmail);
            Assert.Equal("viana.dener@gmail.com", model.PersonalEmail);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void Update_Sucesso()
        {
            //Cenário
            var businessEmail = "viana2.dener@vianasoft.com.br"; ;
            var personalEmail = "viana2.dener@gmail.com";

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);
            model.Update(businessEmail, personalEmail, "Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("Dener Viana", model.Name);
            Assert.Equal("viana2.dener@vianasoft.com.br", model.BusinessEmail);
            Assert.Equal("viana2.dener@gmail.com", model.PersonalEmail);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void Enable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);
            model.Enable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.True(model.IsEnable);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void Disable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);
            model.Disable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.False(model.IsEnable);
        }

        [Fact(DisplayName = "Delete - Success")]
        [Trait("Domain", "Entities.Contact")]
        public void Delete_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Contact(_partnerId, _name, _businessEmail, _personalEmail, _aspNetUser.Name);
            model.Delete("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.True(model.IsExclude);
        }

        #endregion
    }
}
