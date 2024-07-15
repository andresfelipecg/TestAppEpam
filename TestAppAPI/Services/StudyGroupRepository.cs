using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAPI.Model;

namespace TestAppAPI.Services
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly AppDbContext _context;

        public StudyGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStudyGroup(StudyGroup studyGroup)
        {
            if (studyGroup.Name.Length < 5 || studyGroup.Name.Length > 30)
            {
                throw new ArgumentException("Study group name must be between 5 and 30 characters.");
            }

            var subjectExists = await _context.Subjects.AnyAsync(s => s.SubjectId == studyGroup.SubjectId);
            if (!subjectExists)
            {
                throw new ArgumentException("Invalid subject.");
            }

            var userExists = await _context.Users.AnyAsync(u => u.UserId == studyGroup.UserId);
            if (!userExists)
            {
                throw new ArgumentException("Invalid user.");
            }

            studyGroup.CreateDate = DateTime.UtcNow;
            _context.StudyGroups.Add(studyGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudyGroup>> GetStudyGroups()
        {
            return await _context.StudyGroups
                .Include(sg => sg.Subject) // Incluimos la propiedad de navegación Subject
                .Include(sg => sg.StudyGroupUsers)
                    .ThenInclude(sgu => sgu.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject)
        {
            return await _context.StudyGroups
                .Include(sg => sg.Subject) // Incluimos la propiedad de navegación Subject
                .Include(sg => sg.StudyGroupUsers)
                    .ThenInclude(sgu => sgu.User)
                .Where(sg => sg.Subject.Name.ToLower() == subject.ToLower())
                .ToListAsync();
        }

        public async Task<bool> CanCreateStudyGroupForSubject(int subjectId)
        {
            var existingGroup = await _context.StudyGroups
                .Where(sg => sg.SubjectId == subjectId)
                .FirstOrDefaultAsync();

            return existingGroup == null;
        }

        public async Task JoinStudyGroup(int studyGroupId, int userId)
        {
            var studyGroup = await _context.StudyGroups
                .Include(sg => sg.StudyGroupUsers)
                .FirstOrDefaultAsync(sg => sg.StudyGroupId == studyGroupId);
            var user = await _context.Users.FindAsync(userId);

            if (studyGroup != null && user != null)
            {
                if (studyGroup.StudyGroupUsers == null)
                {
                    studyGroup.StudyGroupUsers = new List<StudyGroupUser>();
                }

                studyGroup.StudyGroupUsers.Add(new StudyGroupUser { StudyGroupId = studyGroupId, UserId = userId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task LeaveStudyGroup(int studyGroupId, int userId)
        {
            var studyGroup = await _context.StudyGroups
                .Include(sg => sg.StudyGroupUsers)
                .FirstOrDefaultAsync(sg => sg.StudyGroupId == studyGroupId);
            var user = await _context.Users.FindAsync(userId);

            if (studyGroup != null && user != null)
            {
                var studyGroupUser = studyGroup.StudyGroupUsers.FirstOrDefault(sgu => sgu.UserId == userId);
                if (studyGroupUser != null)
                {
                    studyGroup.StudyGroupUsers.Remove(studyGroupUser);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

