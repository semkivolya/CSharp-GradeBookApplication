using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    internal class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException();

            var sortedGrades = (from student in Students
                                select student.AverageGrade into grade
                                orderby grade descending
                                select grade).ToList(); 

            var percentIndexStep = (int)Math.Round(0.2 * sortedGrades.Count);

            int categoryIndexEnd = percentIndexStep - 1;
            int categoryIndexStart = 0;
            //in case we have same grades at the end of the range
            categoryIndexEnd = Math.Max(sortedGrades.LastIndexOf(sortedGrades[categoryIndexEnd]), categoryIndexEnd);

            if (averageGrade >= sortedGrades[categoryIndexEnd])
                return 'A';

            categoryIndexStart = categoryIndexEnd + 1;
            categoryIndexEnd += percentIndexStep;
            if(categoryIndexEnd < sortedGrades.Count)
                categoryIndexEnd = Math.Max(sortedGrades.LastIndexOf(sortedGrades[categoryIndexEnd]), categoryIndexEnd);

            if(averageGrade >= sortedGrades[categoryIndexEnd] && averageGrade <= sortedGrades[categoryIndexStart])
                return 'B';

            categoryIndexStart = categoryIndexEnd + 1;
            categoryIndexEnd += percentIndexStep;
            if (categoryIndexEnd < sortedGrades.Count)
                categoryIndexEnd = Math.Max(sortedGrades.LastIndexOf(sortedGrades[categoryIndexEnd]), categoryIndexEnd);

            if (averageGrade >= sortedGrades[categoryIndexEnd] && averageGrade <= sortedGrades[categoryIndexStart])
                return 'C';

            categoryIndexStart = categoryIndexEnd + 1;
            categoryIndexEnd += percentIndexStep;
            if (categoryIndexEnd < sortedGrades.Count)
                categoryIndexEnd = Math.Max(sortedGrades.LastIndexOf(sortedGrades[categoryIndexEnd]), categoryIndexEnd);

            if (averageGrade >= sortedGrades[categoryIndexEnd] && averageGrade <= sortedGrades[categoryIndexStart])
                return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students?.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students?.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
