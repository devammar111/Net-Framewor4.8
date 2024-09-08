using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Repository;
using Microsoft.AspNet.Identity;
using PBASE.Entity.Enum;
using System.Threading.Tasks;
using System.Web;
using PBASE.Domain.Enum;

namespace PBASE.Service
{
    public partial class TestService : BaseService, ITestService
    {
        #region Initialization
        private readonly Ivw_TestGridRepository vw_TestGridRepository;
        private readonly Ivw_LookupTestTypeRepository vw_LookupTestTypeRepository;
        private readonly Ivw_LookupGridTestTypeRepository vw_LookupGridTestTypeRepository;
        private readonly Ivw_TestSubGridRepository vw_TestSubGridRepository;
        private readonly Ivw_TestNoteGridRepository vw_TestNoteGridRepository;

        private readonly ITestRepository TestRepository;
        private readonly ITestNoteRepository TestNoteRepository;
        private readonly ITestNoteAttachmentRepository TestNoteAttachmentRepository;
        private readonly ITestSubRepository TestSubRepository;

        private readonly IUnitOfWork unitOfWork;


        public TestService(
            Ivw_TestGridRepository vw_TestGridRepository,
            Ivw_LookupTestTypeRepository vw_LookupTestTypeRepository,
            Ivw_LookupGridTestTypeRepository vw_LookupGridTestTypeRepository,
            Ivw_TestSubGridRepository vw_TestSubGridRepository,
            Ivw_TestNoteGridRepository vw_TestNoteGridRepository,
            ITestRepository TestRepository,
            ITestNoteRepository TestNoteRepository,
            ITestNoteAttachmentRepository TestNoteAttachmentRepository,
            ITestSubRepository TestSubRepository,

        IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
            this.vw_TestGridRepository = vw_TestGridRepository;
            this.vw_LookupTestTypeRepository = vw_LookupTestTypeRepository;
            this.vw_LookupGridTestTypeRepository = vw_LookupGridTestTypeRepository;
            this.vw_TestSubGridRepository = vw_TestSubGridRepository;
            this.vw_TestNoteGridRepository = vw_TestNoteGridRepository;
            this.TestRepository = TestRepository;
            this.TestNoteRepository = TestNoteRepository;
            this.TestNoteAttachmentRepository = TestNoteAttachmentRepository;
            this.TestSubRepository = TestSubRepository;
        }

        public void Save()
        {
            int userId = HttpContext.Current.User.Identity.GetUserId<int>();
            userId = (userId != 0 ? userId : 1); //if user is not logged-in then pass userId = 1 (for reset-password scenario)
            unitOfWork.Commit(userId);
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        #endregion Initialization

        #region vw_TestGrid

        public List<vw_TestGrid> Selectvw_TestGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_TestGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_TestGrid>> Selectvw_TestGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_TestGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_TestGrid> SelectAllvw_TestGrids()
        {
            return vw_TestGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_TestGrid>> SelectAllvw_TestGridsAsync()
        {
            return vw_TestGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_TestGrid> SelectMany_vw_TestGrid(Expression<Func<vw_TestGrid, bool>> where)
        {
            return vw_TestGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_TestGrid>> SelectMany_vw_TestGridAsync(Expression<Func<vw_TestGrid, bool>> where)
        {
            return vw_TestGridRepository.SelectManyAsync(where);
        }
        public vw_TestGrid SelectSingle_vw_TestGrid(Expression<Func<vw_TestGrid, bool>> where)
        {
            return vw_TestGridRepository.Select(where);
        }
        public Task<vw_TestGrid> SelectSingle_vw_TestGridAsync(Expression<Func<vw_TestGrid, bool>> where)
        {
            return vw_TestGridRepository.SelectAsync(where);
        }

        #endregion vw_TestGrid

        #region vw_TestSubGrid

        public List<vw_TestSubGrid> Selectvw_TestSubGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_TestSubGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_TestSubGrid>> Selectvw_TestSubGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_TestSubGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_TestSubGrid> SelectAllvw_TestSubGrids()
        {
            return vw_TestSubGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_TestSubGrid>> SelectAllvw_TestSubGridsAsync()
        {
            return vw_TestSubGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_TestSubGrid> SelectMany_vw_TestSubGrid(Expression<Func<vw_TestSubGrid, bool>> where)
        {
            return vw_TestSubGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_TestSubGrid>> SelectMany_vw_TestSubGridAsync(Expression<Func<vw_TestSubGrid, bool>> where)
        {
            return vw_TestSubGridRepository.SelectManyAsync(where);
        }
        public vw_TestSubGrid SelectSingle_vw_TestSubGrid(Expression<Func<vw_TestSubGrid, bool>> where)
        {
            return vw_TestSubGridRepository.Select(where);
        }
        public Task<vw_TestSubGrid> SelectSingle_vw_TestSubGridAsync(Expression<Func<vw_TestSubGrid, bool>> where)
        {
            return vw_TestSubGridRepository.SelectAsync(where);
        }

        #endregion vw_TestSubGrid

        #region vw_TestNoteGrid

        public List<vw_TestNoteGrid> Selectvw_TestNoteGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_TestNoteGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_TestNoteGrid>> Selectvw_TestNoteGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_TestNoteGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_TestNoteGrid> SelectAllvw_TestNoteGrids()
        {
            return vw_TestNoteGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_TestNoteGrid>> SelectAllvw_TestNoteGridsAsync()
        {
            return vw_TestNoteGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_TestNoteGrid> SelectMany_vw_TestNoteGrid(Expression<Func<vw_TestNoteGrid, bool>> where)
        {
            return vw_TestNoteGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_TestNoteGrid>> SelectMany_vw_TestNoteGridAsync(Expression<Func<vw_TestNoteGrid, bool>> where)
        {
            return vw_TestNoteGridRepository.SelectManyAsync(where);
        }
        public vw_TestNoteGrid SelectSingle_vw_TestNoteGrid(Expression<Func<vw_TestNoteGrid, bool>> where)
        {
            return vw_TestNoteGridRepository.Select(where);
        }
        public Task<vw_TestNoteGrid> SelectSingle_vw_TestNoteGridAsync(Expression<Func<vw_TestNoteGrid, bool>> where)
        {
            return vw_TestNoteGridRepository.SelectAsync(where);
        }

        #endregion vw_TestNoteGrid

        #region vw_LookupTestType

        public List<vw_LookupTestType> Selectvw_LookupTestTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupTestTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupTestType>> Selectvw_LookupTestTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupTestTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupTestType> SelectAllvw_LookupTestTypes()
        {
            return vw_LookupTestTypeRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupTestTypesAsync()
        {
            var lookup = await vw_LookupTestTypeRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.TestTypeId,
                LookupValue = x.TestType,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "INACTIVE" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupTestType> SelectMany_vw_LookupTestType(Expression<Func<vw_LookupTestType, bool>> where)
        {
            return vw_LookupTestTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupTestType>> SelectMany_vw_LookupTestTypeAsync(Expression<Func<vw_LookupTestType, bool>> where)
        {
            return vw_LookupTestTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupTestType SelectSingle_vw_LookupTestType(Expression<Func<vw_LookupTestType, bool>> where)
        {
            return vw_LookupTestTypeRepository.Select(where);
        }
        public Task<vw_LookupTestType> SelectSingle_vw_LookupTestTypeAsync(Expression<Func<vw_LookupTestType, bool>> where)
        {
            return vw_LookupTestTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupTestType

        #region vw_LookupGridTestType

        public List<vw_LookupGridTestType> Selectvw_LookupGridTestTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridTestTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridTestType>> Selectvw_LookupGridTestTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridTestTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridTestType> SelectAllvw_LookupGridTestTypes()
        {
            return vw_LookupGridTestTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridTestType>> SelectAllvw_LookupGridTestTypesAsync()
        {
            return vw_LookupGridTestTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridTestType> SelectMany_vw_LookupGridTestType(Expression<Func<vw_LookupGridTestType, bool>> where)
        {
            return vw_LookupGridTestTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridTestType>> SelectMany_vw_LookupGridTestTypeAsync(Expression<Func<vw_LookupGridTestType, bool>> where)
        {
            return vw_LookupGridTestTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridTestType SelectSingle_vw_LookupGridTestType(Expression<Func<vw_LookupGridTestType, bool>> where)
        {
            return vw_LookupGridTestTypeRepository.Select(where);
        }
        public Task<vw_LookupGridTestType> SelectSingle_vw_LookupGridTestTypeAsync(Expression<Func<vw_LookupGridTestType, bool>> where)
        {
            return vw_LookupGridTestTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridTestType

        #region Test

        #region Sync Methods

        public Test SelectByTestId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TestRepository.SelectById(lookupTypeId);
            }
            else
            {
                Test lookupType = CacheService.Get<Test>("SelectByTestId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TestRepository.SelectById(lookupTypeId);
                    CacheService.Add<Test>("SelectByTestId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTestId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<Test> SelectMany_Test(Expression<Func<Test, bool>> where)
        {
            return TestRepository.SelectMany(where);
        }

        public Test SelectSingle_Test(Expression<Func<Test, bool>> where)
        {
            return TestRepository.Select(where);
        }

        public IEnumerable<Test> SelectAllTests()
        {
            return TestRepository.SelectAll();
        }

        public bool SaveTestForm(Test lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TestRepository.Add(lookupType);
                }
                else
                {
                    TestRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTestForm(int lookupTypeId)
        {
            try
            {
                Test lookupType = TestRepository.SelectById(lookupTypeId);
                TestRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<Test> SelectByTestIdAsync(int lookupTypeId)
        {
            return TestRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<Test>> SelectMany_TestAsync(Expression<Func<Test, bool>> where)
        {
            return TestRepository.SelectManyAsync(where);
        }

        public Task<Test> SelectSingle_TestAsync(Expression<Func<Test, bool>> where)
        {
            return TestRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Test>> SelectAllTestsAsync()
        {
            return TestRepository.SelectAllAsync();
        }

        public Task<int> SaveTestFormAsync(Test lookupType)
        {
            try
            {
                if (lookupType.TestId == 0)
                {
                    TestRepository.Add(lookupType);
                }
                else
                {
                    TestRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTestFormAsync(int lookupTypeId)
        {
            try
            {
                Test lookupType = TestRepository.SelectById(lookupTypeId);
                TestRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Test

        #region TestNote

        #region Sync Methods

        public TestNote SelectByTestNoteId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TestNoteRepository.SelectById(lookupTypeId);
            }
            else
            {
                TestNote lookupType = CacheService.Get<TestNote>("SelectByTestNoteId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TestNoteRepository.SelectById(lookupTypeId);
                    CacheService.Add<TestNote>("SelectByTestNoteId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTestNoteId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<TestNote> SelectMany_TestNote(Expression<Func<TestNote, bool>> where)
        {
            return TestNoteRepository.SelectMany(where);
        }

        public TestNote SelectSingle_TestNote(Expression<Func<TestNote, bool>> where)
        {
            return TestNoteRepository.Select(where);
        }

        public IEnumerable<TestNote> SelectAllTestNotes()
        {
            return TestNoteRepository.SelectAll();
        }

        public bool SaveTestNoteForm(TestNote lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TestNoteRepository.Add(lookupType);
                }
                else
                {
                    TestNoteRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTestNoteForm(int lookupTypeId)
        {
            try
            {
                TestNote lookupType = TestNoteRepository.SelectById(lookupTypeId);
                TestNoteRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<TestNote> SelectByTestNoteIdAsync(int lookupTypeId)
        {
            return TestNoteRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<TestNote>> SelectMany_TestNoteAsync(Expression<Func<TestNote, bool>> where)
        {
            return TestNoteRepository.SelectManyAsync(where);
        }

        public Task<TestNote> SelectSingle_TestNoteAsync(Expression<Func<TestNote, bool>> where)
        {
            return TestNoteRepository.SelectAsync(where);
        }

        public Task<IEnumerable<TestNote>> SelectAllTestNotesAsync()
        {
            return TestNoteRepository.SelectAllAsync();
        }

        public Task<int> SaveTestNoteFormAsync(TestNote lookupType)
        {
            try
            {
                if (lookupType.TestNoteId == 0)
                {
                    TestNoteRepository.Add(lookupType);
                }
                else
                {
                    TestNoteRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTestNoteFormAsync(int lookupTypeId)
        {
            try
            {
                TestNote lookupType = TestNoteRepository.SelectById(lookupTypeId);
                TestNoteRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion TestNote

        #region TestNoteAttachment

        #region Sync Methods

        public TestNoteAttachment SelectByTestNoteAttachmentId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TestNoteAttachmentRepository.SelectById(lookupTypeId);
            }
            else
            {
                TestNoteAttachment lookupType = CacheService.Get<TestNoteAttachment>("SelectByTestNoteAttachmentId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TestNoteAttachmentRepository.SelectById(lookupTypeId);
                    CacheService.Add<TestNoteAttachment>("SelectByTestNoteAttachmentId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTestNoteAttachmentId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<TestNoteAttachment> SelectMany_TestNoteAttachment(Expression<Func<TestNoteAttachment, bool>> where)
        {
            return TestNoteAttachmentRepository.SelectMany(where);
        }

        public TestNoteAttachment SelectSingle_TestNoteAttachment(Expression<Func<TestNoteAttachment, bool>> where)
        {
            return TestNoteAttachmentRepository.Select(where);
        }

        public IEnumerable<TestNoteAttachment> SelectAllTestNoteAttachments()
        {
            return TestNoteAttachmentRepository.SelectAll();
        }

        public bool SaveTestNoteAttachmentForm(TestNoteAttachment lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TestNoteAttachmentRepository.Add(lookupType);
                }
                else
                {
                    TestNoteAttachmentRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTestNoteAttachmentForm(int lookupTypeId)
        {
            try
            {
                TestNoteAttachment lookupType = TestNoteAttachmentRepository.SelectById(lookupTypeId);
                TestNoteAttachmentRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<TestNoteAttachment> SelectByTestNoteAttachmentIdAsync(int lookupTypeId)
        {
            return TestNoteAttachmentRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<TestNoteAttachment>> SelectMany_TestNoteAttachmentAsync(Expression<Func<TestNoteAttachment, bool>> where)
        {
            return TestNoteAttachmentRepository.SelectManyAsync(where);
        }

        public Task<TestNoteAttachment> SelectSingle_TestNoteAttachmentAsync(Expression<Func<TestNoteAttachment, bool>> where)
        {
            return TestNoteAttachmentRepository.SelectAsync(where);
        }

        public Task<IEnumerable<TestNoteAttachment>> SelectAllTestNoteAttachmentsAsync()
        {
            return TestNoteAttachmentRepository.SelectAllAsync();
        }

        public Task<int> SaveTestNoteAttachmentFormAsync(TestNoteAttachment lookupType)
        {
            try
            {
                if (lookupType.TestNoteAttachmentId == 0)
                {
                    TestNoteAttachmentRepository.Add(lookupType);
                }
                else
                {
                    TestNoteAttachmentRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTestNoteAttachmentFormAsync(int lookupTypeId)
        {
            try
            {
                TestNoteAttachment lookupType = TestNoteAttachmentRepository.SelectById(lookupTypeId);
                TestNoteAttachmentRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion TestNoteAttachment

        #region TestSub

        #region Sync Methods

        public TestSub SelectByTestSubId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TestSubRepository.SelectById(lookupTypeId);
            }
            else
            {
                TestSub lookupType = CacheService.Get<TestSub>("SelectByTestSubId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TestSubRepository.SelectById(lookupTypeId);
                    CacheService.Add<TestSub>("SelectByTestSubId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTestSubId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<TestSub> SelectMany_TestSub(Expression<Func<TestSub, bool>> where)
        {
            return TestSubRepository.SelectMany(where);
        }

        public TestSub SelectSingle_TestSub(Expression<Func<TestSub, bool>> where)
        {
            return TestSubRepository.Select(where);
        }

        public IEnumerable<TestSub> SelectAllTestSubs()
        {
            return TestSubRepository.SelectAll();
        }

        public bool SaveTestSubForm(TestSub lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TestSubRepository.Add(lookupType);
                }
                else
                {
                    TestSubRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTestSubForm(int lookupTypeId)
        {
            try
            {
                TestSub lookupType = TestSubRepository.SelectById(lookupTypeId);
                TestSubRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<TestSub> SelectByTestSubIdAsync(int lookupTypeId)
        {
            return TestSubRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<TestSub>> SelectMany_TestSubAsync(Expression<Func<TestSub, bool>> where)
        {
            return TestSubRepository.SelectManyAsync(where);
        }

        public Task<TestSub> SelectSingle_TestSubAsync(Expression<Func<TestSub, bool>> where)
        {
            return TestSubRepository.SelectAsync(where);
        }

        public Task<IEnumerable<TestSub>> SelectAllTestSubsAsync()
        {
            return TestSubRepository.SelectAllAsync();
        }

        public Task<int> SaveTestSubFormAsync(TestSub lookupType)
        {
            try
            {
                if (lookupType.TestSubId == 0)
                {
                    TestSubRepository.Add(lookupType);
                }
                else
                {
                    TestSubRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTestSubFormAsync(int lookupTypeId)
        {
            try
            {
                TestSub lookupType = TestSubRepository.SelectById(lookupTypeId);
                TestSubRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion TestSub

    }

    public partial interface ITestService : IBaseService
    {

        #region vw_TestGrid
        List<vw_TestGrid> Selectvw_TestGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_TestGrid> SelectAllvw_TestGrids();
        Task<IEnumerable<vw_TestGrid>> SelectAllvw_TestGridsAsync();
        Task<List<vw_TestGrid>> Selectvw_TestGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_TestGrid> SelectMany_vw_TestGrid(Expression<Func<vw_TestGrid, bool>> where);
        Task<IEnumerable<vw_TestGrid>> SelectMany_vw_TestGridAsync(Expression<Func<vw_TestGrid, bool>> where);
        vw_TestGrid SelectSingle_vw_TestGrid(Expression<Func<vw_TestGrid, bool>> where);
        Task<vw_TestGrid> SelectSingle_vw_TestGridAsync(Expression<Func<vw_TestGrid, bool>> where);

        #endregion

        #region vw_TestSubGrid
        List<vw_TestSubGrid> Selectvw_TestSubGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_TestSubGrid> SelectAllvw_TestSubGrids();
        Task<IEnumerable<vw_TestSubGrid>> SelectAllvw_TestSubGridsAsync();
        Task<List<vw_TestSubGrid>> Selectvw_TestSubGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_TestSubGrid> SelectMany_vw_TestSubGrid(Expression<Func<vw_TestSubGrid, bool>> where);
        Task<IEnumerable<vw_TestSubGrid>> SelectMany_vw_TestSubGridAsync(Expression<Func<vw_TestSubGrid, bool>> where);
        vw_TestSubGrid SelectSingle_vw_TestSubGrid(Expression<Func<vw_TestSubGrid, bool>> where);
        Task<vw_TestSubGrid> SelectSingle_vw_TestSubGridAsync(Expression<Func<vw_TestSubGrid, bool>> where);

        #endregion

        #region vw_TestNoteGrid
        List<vw_TestNoteGrid> Selectvw_TestNoteGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_TestNoteGrid> SelectAllvw_TestNoteGrids();
        Task<IEnumerable<vw_TestNoteGrid>> SelectAllvw_TestNoteGridsAsync();
        Task<List<vw_TestNoteGrid>> Selectvw_TestNoteGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_TestNoteGrid> SelectMany_vw_TestNoteGrid(Expression<Func<vw_TestNoteGrid, bool>> where);
        Task<IEnumerable<vw_TestNoteGrid>> SelectMany_vw_TestNoteGridAsync(Expression<Func<vw_TestNoteGrid, bool>> where);
        vw_TestNoteGrid SelectSingle_vw_TestNoteGrid(Expression<Func<vw_TestNoteGrid, bool>> where);
        Task<vw_TestNoteGrid> SelectSingle_vw_TestNoteGridAsync(Expression<Func<vw_TestNoteGrid, bool>> where);

        #endregion

        #region vw_LookupTestType
        List<vw_LookupTestType> Selectvw_LookupTestTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupTestType> SelectAllvw_LookupTestTypes();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupTestTypesAsync();
        Task<List<vw_LookupTestType>> Selectvw_LookupTestTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupTestType> SelectMany_vw_LookupTestType(Expression<Func<vw_LookupTestType, bool>> where);
        Task<IEnumerable<vw_LookupTestType>> SelectMany_vw_LookupTestTypeAsync(Expression<Func<vw_LookupTestType, bool>> where);
        vw_LookupTestType SelectSingle_vw_LookupTestType(Expression<Func<vw_LookupTestType, bool>> where);
        Task<vw_LookupTestType> SelectSingle_vw_LookupTestTypeAsync(Expression<Func<vw_LookupTestType, bool>> where);

        #endregion

        #region vw_LookupGridTestType
        List<vw_LookupGridTestType> Selectvw_LookupGridTestTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridTestType> SelectAllvw_LookupGridTestTypes();
        Task<IEnumerable<vw_LookupGridTestType>> SelectAllvw_LookupGridTestTypesAsync();
        Task<List<vw_LookupGridTestType>> Selectvw_LookupGridTestTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridTestType> SelectMany_vw_LookupGridTestType(Expression<Func<vw_LookupGridTestType, bool>> where);
        Task<IEnumerable<vw_LookupGridTestType>> SelectMany_vw_LookupGridTestTypeAsync(Expression<Func<vw_LookupGridTestType, bool>> where);
        vw_LookupGridTestType SelectSingle_vw_LookupGridTestType(Expression<Func<vw_LookupGridTestType, bool>> where);
        Task<vw_LookupGridTestType> SelectSingle_vw_LookupGridTestTypeAsync(Expression<Func<vw_LookupGridTestType, bool>> where);

        #endregion

        #region Test

        Test SelectByTestId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<Test> SelectMany_Test(Expression<Func<Test, bool>> where);

        Test SelectSingle_Test(Expression<Func<Test, bool>> where);

        IEnumerable<Test> SelectAllTests();

        bool SaveTestForm(Test lookupTypeRepository);

        bool DeleteTestForm(int lookupTypeId);

        Task<Test> SelectByTestIdAsync(int lookupTypeId);

        Task<IEnumerable<Test>> SelectMany_TestAsync(Expression<Func<Test, bool>> where);

        Task<Test> SelectSingle_TestAsync(Expression<Func<Test, bool>> where);

        Task<IEnumerable<Test>> SelectAllTestsAsync();

        Task<int> SaveTestFormAsync(Test lookupTypeRepository);

        Task<int> DeleteTestFormAsync(int lookupTypeId);
        #endregion

        #region TestNote

        TestNote SelectByTestNoteId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<TestNote> SelectMany_TestNote(Expression<Func<TestNote, bool>> where);

        TestNote SelectSingle_TestNote(Expression<Func<TestNote, bool>> where);

        IEnumerable<TestNote> SelectAllTestNotes();

        bool SaveTestNoteForm(TestNote lookupTypeRepository);

        bool DeleteTestNoteForm(int lookupTypeId);

        Task<TestNote> SelectByTestNoteIdAsync(int lookupTypeId);

        Task<IEnumerable<TestNote>> SelectMany_TestNoteAsync(Expression<Func<TestNote, bool>> where);

        Task<TestNote> SelectSingle_TestNoteAsync(Expression<Func<TestNote, bool>> where);

        Task<IEnumerable<TestNote>> SelectAllTestNotesAsync();

        Task<int> SaveTestNoteFormAsync(TestNote lookupTypeRepository);

        Task<int> DeleteTestNoteFormAsync(int lookupTypeId);
        #endregion

        #region TestNoteAttachment

        TestNoteAttachment SelectByTestNoteAttachmentId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<TestNoteAttachment> SelectMany_TestNoteAttachment(Expression<Func<TestNoteAttachment, bool>> where);

        TestNoteAttachment SelectSingle_TestNoteAttachment(Expression<Func<TestNoteAttachment, bool>> where);

        IEnumerable<TestNoteAttachment> SelectAllTestNoteAttachments();

        bool SaveTestNoteAttachmentForm(TestNoteAttachment lookupTypeRepository);

        bool DeleteTestNoteAttachmentForm(int lookupTypeId);

        Task<TestNoteAttachment> SelectByTestNoteAttachmentIdAsync(int lookupTypeId);

        Task<IEnumerable<TestNoteAttachment>> SelectMany_TestNoteAttachmentAsync(Expression<Func<TestNoteAttachment, bool>> where);

        Task<TestNoteAttachment> SelectSingle_TestNoteAttachmentAsync(Expression<Func<TestNoteAttachment, bool>> where);

        Task<IEnumerable<TestNoteAttachment>> SelectAllTestNoteAttachmentsAsync();

        Task<int> SaveTestNoteAttachmentFormAsync(TestNoteAttachment lookupTypeRepository);

        Task<int> DeleteTestNoteAttachmentFormAsync(int lookupTypeId);
        #endregion

        #region TestSub

        TestSub SelectByTestSubId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<TestSub> SelectMany_TestSub(Expression<Func<TestSub, bool>> where);

        TestSub SelectSingle_TestSub(Expression<Func<TestSub, bool>> where);

        IEnumerable<TestSub> SelectAllTestSubs();

        bool SaveTestSubForm(TestSub lookupTypeRepository);

        bool DeleteTestSubForm(int lookupTypeId);

        Task<TestSub> SelectByTestSubIdAsync(int lookupTypeId);

        Task<IEnumerable<TestSub>> SelectMany_TestSubAsync(Expression<Func<TestSub, bool>> where);

        Task<TestSub> SelectSingle_TestSubAsync(Expression<Func<TestSub, bool>> where);

        Task<IEnumerable<TestSub>> SelectAllTestSubsAsync();

        Task<int> SaveTestSubFormAsync(TestSub lookupTypeRepository);

        Task<int> DeleteTestSubFormAsync(int lookupTypeId);
        #endregion



    }
}

