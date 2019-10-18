using FinoBank.Cola.Repository.Uom;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.UnitTests
{
    [TestClass]
    public class QueryStartupKitRepositoryTest
    {
        private IUnitOfWork _unitOfWork;
        private readonly string _connectionString = "Data Source=192.168.32.128;Initial Catalog=FinoBank-COLA; User=sa; Password=fulcrum#1;Trusted_Connection = False; MultipleActiveResultSets = true";

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWork = new UnitOfWork(_connectionString);
        }

        /// <summary>
        /// Startups the kit domain model get grid summary data with paging success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_GetGridSummaryDataWithPaging_SuccessTest()
        {
            var result = await _unitOfWork.QuerySampleRepository.GetGridSummaryDataWithPaging(1, "CreatedDateTime", "Desc", 1, 10, null).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
        }

        /// <summary>
        /// Startups the kit domain model get by identifier success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_GetById_SuccessTest()
        {
            var result = await _unitOfWork.QuerySampleRepository.GetById(1).ConfigureAwait(false);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        /// <summary>
        /// Startups the kit domain model delete success test.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public async Task StartupKitDomainModel_Delete_SuccessTest()
        {
            var result = await _unitOfWork.QuerySampleRepository.GetAllData(null).ConfigureAwait(false);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }
    }
}