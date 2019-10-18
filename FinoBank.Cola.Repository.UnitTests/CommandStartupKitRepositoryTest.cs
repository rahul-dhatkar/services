using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.UnitTests
{
    [TestClass]
    public class CommandStartupKitRepositoryTest
    {
        private IUnitOfWork _unitOfWork;
        private readonly string _connectionString = "Data Source=192.168.32.128;Initial Catalog=FinoBank-COLA; User=sa; Password=fulcrum#1;Trusted_Connection = False; MultipleActiveResultSets = true";

        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWork = new UnitOfWork(_connectionString);
        }

        /// <summary>
        /// Startups the kit domain model create success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_Create_SuccessTest()
        {
            var model = new SampleDomainModel()
            {
                Name = "Name",
                Description = "Description",
                TypeId = 1,
                CreatedBy = "IntegrationTestUser",
            };

            var result = await _unitOfWork.CommandSampleRepository.Create(model).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }

        /// <summary>
        /// Startups the kit domain model update success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_Update_SuccessTest()
        {
            var model = new SampleDomainModel()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                TypeId = 1,
                CreatedBy = "IntegrationTestUser",
            };

            var result = await _unitOfWork.CommandSampleRepository.Update(model).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }

        /// <summary>
        /// Startups the kit domain model delete success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_Delete_SuccessTest()
        {
            var result = await _unitOfWork.CommandSampleRepository.Delete(1).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}