using System;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Standard;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            // For every 20% of students that have a lower average, the grade rises by 1 position
            var studentsPerGradeIncrease = Students.Count / 5;
            var currentLetterGrade = 'F';
            var studentIndex = 0;
            var studentsWithLowerGrade = 0;
            while (currentLetterGrade > 'A' && studentIndex < Students.Count)
            {
                // Keep track of how many students have a lower grade, which we use to calculate relative grade
                if (Students[studentIndex].AverageGrade < averageGrade)
                {
                    studentsWithLowerGrade++;
                    if (studentsWithLowerGrade >= studentsPerGradeIncrease)
                    {
                        currentLetterGrade--; // Decrement grade char to get a 'higher' grade (e.g. 'F' to 'E')
                        studentsWithLowerGrade = 0; // Reset count to prepare for next grade
                    }
                }
                studentIndex++;
            }

            return currentLetterGrade;
        }
    }
}