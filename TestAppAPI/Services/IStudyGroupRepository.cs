using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAppAPI.Model;

namespace TestAppAPI.Services
{
    public interface IStudyGroupRepository
    {
        Task CreateStudyGroup(StudyGroup studyGroup);
        Task<IEnumerable<StudyGroup>> GetStudyGroups();
        Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject);
        Task<bool> CanCreateStudyGroupForSubject(int subjectId);
        Task JoinStudyGroup(int studyGroupId, int userId);
        Task LeaveStudyGroup(int studyGroupId, int userId);
    }
}