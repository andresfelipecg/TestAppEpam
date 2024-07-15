using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppAPI.Model;
using TestAppAPI.Services;

namespace TestAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudyGroup(StudyGroup studyGroup)
        {
            // Ignorar los valores innecesarios de Subject
            studyGroup.Subject = null;

            await _studyGroupRepository.CreateStudyGroup(studyGroup);
            return new OkResult();
        }


        [HttpGet]
        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }


        [HttpPost("{studyGroupId}/join")]
        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }


        [HttpPost("{studyGroupId}/leave")]
        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

    }
}