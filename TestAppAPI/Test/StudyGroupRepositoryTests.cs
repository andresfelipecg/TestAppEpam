//using NUnit.Framework;
//using Moq;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using TestAppAPI.Model;
//using TestAppAPI.Services;
//using System.Collections.Generic;
//using NUnit.Framework.Legacy;


//namespace TestAppAPI.Tests
//{
//    [TestFixture]
//    public class StudyGroupRepositoryTests
//    {
//        private Mock<AppDbContext> _contextMock;
//        private StudyGroupRepository _repository;
//        private DbSet<StudyGroup> _studyGroupDbSetMock; 
//        private DbSet<User> _userDbSetMock;
//        private DbSet<Subject> _subjectDbSetMock;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            var context = new AppDbContext(options);
//            _repository = new StudyGroupRepository(context);

//            SeedDatabase(context);
//        }

//        private void SeedDatabase(AppDbContext context)
//        {
//            var math = new Subject { SubjectId = 1, Name = "Math" };
//            var user = new User { UserId = 1, Name = "Test User" };

//            context.Subjects.Add(math);
//            context.Users.Add(user);
//            context.SaveChanges();
//        }

//        [Test]
//        public async Task TC01_ValidateStudyGroupNameLength()
//        {
//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 1, UserId = 1 };
//            await _repository.CreateStudyGroup(studyGroup);
//            ClassicAssert.AreEqual("Math Group", studyGroup.Name);

//            studyGroup.Name = "VeryLongStudyGroupNameExceedsLimit";
//            Assert.ThrowsAsync<ArgumentException>(async () => await _repository.CreateStudyGroup(studyGroup));
//        }

//        [Test]
//        public async Task TC02_AddUserToStudyGroup()
//        {
//            var studyGroup = new StudyGroup { StudyGroupId = 1, Name = "Math Group", SubjectId = 1, UserId = 1 };
//            _repository.CreateStudyGroup(studyGroup);
//            await _repository.JoinStudyGroup(1, 1);

//            var result = await _repository.GetStudyGroups();
//            ClassicAssert.IsTrue(result.First().StudyGroupUsers.Any(u => u.UserId == 1));
//        }

//        [Test]
//        public async Task TC03_RemoveUserFromStudyGroup()
//        {
//            var studyGroup = new StudyGroup { StudyGroupId = 1, Name = "Math Group", SubjectId = 1, UserId = 1 };
//            _repository.CreateStudyGroup(studyGroup);
//            await _repository.JoinStudyGroup(1, 1);
//            await _repository.LeaveStudyGroup(1, 1);

//            var result = await _repository.GetStudyGroups();
//            ClassicAssert.IsFalse(result.First().StudyGroupUsers.Any(u => u.UserId == 1));
//        }

//        [Test]
//        public async Task TC04_CheckValidSubject()
//        {
//            var subjectExists = await _repository.CanCreateStudyGroupForSubject(1);
//            ClassicAssert.IsTrue(subjectExists);
//        }

//        [Test]
//        public async Task TC05_CheckInvalidSubject()
//        {
//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 2, UserId = 1 };
//            Assert.ThrowsAsync<ArgumentException>(async () => await _repository.CreateStudyGroup(studyGroup));
//        }

//        [Test]
//        public async Task TC06_RecordCreationDate()
//        {
//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 1, UserId = 1 };
//            await _repository.CreateStudyGroup(studyGroup);

//            ClassicAssert.IsNotNull(studyGroup.CreateDate);
//        }

//        [Test]
//        public async Task TC07_JoinStudyGroup()
//        {
//            var studyGroup = new StudyGroup { StudyGroupId = 1, Name = "Math Group", SubjectId = 1, UserId = 1 };
//            _repository.CreateStudyGroup(studyGroup);
//            await _repository.JoinStudyGroup(1, 5);

//            var result = await _repository.GetStudyGroups();
//            ClassicAssert.IsTrue(result.First().StudyGroupUsers.Any(u => u.UserId == 5));
//        }

//        [Test]
//        public async Task TC08_LeaveStudyGroup()
//        {
//            var studyGroup = new StudyGroup { StudyGroupId = 1, Name = "Math Group", SubjectId = 1, UserId = 1 };
//            _repository.CreateStudyGroup(studyGroup);
//            await _repository.JoinStudyGroup(1, 5);
//            await _repository.LeaveStudyGroup(1, 5);

//            var result = await _repository.GetStudyGroups();
//            ClassicAssert.IsFalse(result.First().StudyGroupUsers.Any(u => u.UserId == 5));
//        }

//        [Test]
//        public async Task TC09_CanCreateStudyGroupForSubject()
//        {
//            var canCreate = await _repository.CanCreateStudyGroupForSubject(1);
//            ClassicAssert.IsTrue(canCreate);

//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 1, UserId = 1 };
//            await _repository.CreateStudyGroup(studyGroup);

//            canCreate = await _repository.CanCreateStudyGroupForSubject(1);
//            ClassicAssert.IsFalse(canCreate);
//        }

//        [Test]
//        public async Task TC10_RetrieveAllStudyGroups()
//        {
//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 1, UserId = 1 };
//            await _repository.CreateStudyGroup(studyGroup);

//            var studyGroups = await _repository.GetStudyGroups();
//            ClassicAssert.IsNotEmpty(studyGroups);
//        }

//        [Test]
//        public async Task TC11_SearchStudyGroupsBySubject()
//        {
//            var studyGroup = new StudyGroup { Name = "Math Group", SubjectId = 1, UserId = 1 };
//            await _repository.CreateStudyGroup(studyGroup);

//            var result = await _repository.SearchStudyGroups("Math");
//            ClassicAssert.IsNotEmpty(result);
//            ClassicAssert.AreEqual("Math", result.First().Subject.Name);
//        }
//    }
//}