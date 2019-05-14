using FP2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP2.Pages.Admin.AddUser
{
    public class ProfessorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(FP2Context context, Professor professor)
        {
            var allCourses = context.Course;
            var professorCourses = new HashSet<string>(professor.CourseAssignment.Select(c => c.CourseId));

            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach(var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.CourseId,
                    CourseName = course.CourseName,
                    Assigned = professorCourses.Contains(course.CourseId)
                });
            }
        }

        public void UpdateProfessorCourses(FP2Context context, string[] selectedCourses, Professor professorToUpdate)
        {
            if (selectedCourses == null)
            {
                professorToUpdate.CourseAssignment = new List<CourseAssignment>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var professorCourses = new HashSet<string>(professorToUpdate.CourseAssignment.Select(c => c.Course.CourseId));

            foreach(var course in context.Course)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!professorCourses.Contains(course.CourseId))
                    {
                        professorToUpdate.CourseAssignment.Add(
                            new CourseAssignment
                            {
                                ProfessorId = professorToUpdate.ProfessorId,
                                CourseId = course.CourseId
                            });
                    }
                }
                else
                {
                    if (professorCourses.Contains(course.CourseId))
                    {
                        CourseAssignment courseToRemove = professorToUpdate
                            .CourseAssignment
                            .SingleOrDefault(i => i.CourseId == course.CourseId);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
