using Moq;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Entities
{
    public class PhoneTests
    {
        #region Properties

        private readonly Guid _contactId = Guid.NewGuid();
        private readonly string _ddiCode = "+55";
        private readonly string _dddCode = "31";
        private readonly string _number = "997411315";
        private readonly bool _isCellPhone = true;
        private readonly bool _isWhatsapp = true;
        private readonly bool _isTelegram = false;
        private readonly Mock<IAspNetUser> _aspNetUser;

        #endregion

        #region Builders

        public PhoneTests()
        {
            _aspNetUser = new Mock<IAspNetUser>();
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Builder - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void Builder_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("+55", model.DDICode);
            Assert.Equal("31", model.DDDCode);
            Assert.Equal("997411315", model.Number);
            Assert.True(model.IsCellPhone);
            Assert.True(model.IsWhatsapp);
            Assert.False(model.IsTelegram);
            Assert.True(model.IsEnable);
            Assert.False(model.IsExclude);
        }

        [Fact(DisplayName = "AddCreateBy - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void AddCreateBy_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);
            model.AddCreateBy(_aspNetUser.Name);

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("+55", model.DDICode);
            Assert.Equal("31", model.DDDCode);
            Assert.Equal("997411315", model.Number);
            Assert.True(model.IsCellPhone);
            Assert.True(model.IsWhatsapp);
            Assert.False(model.IsTelegram);
            Assert.True(model.IsEnable);
            Assert.False(model.IsExclude);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void Update_Sucesso()
        {
            //Cenário
            var ddiCode = "350";
            var dddCode = "21";
            var number = "992078901";
            var isCellPhone = false;
            var isWhatsapp = false;
            var isTelegram = true;

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);
            model.Update(ddiCode, dddCode, number, isCellPhone, isWhatsapp, isTelegram, "Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("350", model.DDICode);
            Assert.Equal("21", model.DDDCode);
            Assert.Equal("992078901", model.Number);
            Assert.False(model.IsCellPhone);
            Assert.False(model.IsWhatsapp);
            Assert.True(model.IsTelegram);
            Assert.True(model.IsEnable);
            Assert.False(model.IsExclude);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void Enable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);
            model.Enable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("+55", model.DDICode);
            Assert.Equal("31", model.DDDCode);
            Assert.Equal("997411315", model.Number);
            Assert.True(model.IsCellPhone);
            Assert.True(model.IsWhatsapp);
            Assert.False(model.IsTelegram);
            Assert.True(model.IsEnable);
            Assert.False(model.IsExclude);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void Disable_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);
            model.Disable("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("+55", model.DDICode);
            Assert.Equal("31", model.DDDCode);
            Assert.Equal("997411315", model.Number);
            Assert.True(model.IsCellPhone);
            Assert.True(model.IsWhatsapp);
            Assert.False(model.IsTelegram);
            Assert.False(model.IsEnable);
            Assert.False(model.IsExclude);
        }

        [Fact(DisplayName = "Delete - Success")]
        [Trait("Domain", "Entities.Phone")]
        public void Delete_Sucesso()
        {
            //Cenário

            //Ação
            var model = new Partner.Domain.Entities.Phone(_contactId, _ddiCode, _dddCode, _number, _isCellPhone, _isWhatsapp, _isTelegram, _aspNetUser.Name);
            model.Delete("Testing");

            //Afirmações esperadas
            Assert.NotNull(model);
            Assert.Equal("+55", model.DDICode);
            Assert.Equal("31", model.DDDCode);
            Assert.Equal("997411315", model.Number);
            Assert.True(model.IsCellPhone);
            Assert.True(model.IsWhatsapp);
            Assert.False(model.IsTelegram);
            Assert.True(model.IsEnable);
            Assert.True(model.IsExclude);
        }

        #endregion
    }
}
