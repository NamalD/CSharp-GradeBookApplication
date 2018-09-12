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
            var gradeThreshold = Students.Count / 5;
            var currentLetterGrade = 'F';
            var studentIndex = 0;
            var lowerGradeCount = 0;
            while (currentLetterGrade > 'A' && studentIndex < Students.Count)
            {
                // Keep track of how many students have a lower grade, which we use to calculate relative grade
                var comparisonGrade = Students[studentIndex].AverageGrade;
                if (comparisonGrade < averageGrade || (comparisonGrade == averageGrade && currentLetterGrade != 'F'))
                {
                    lowerGradeCount++;
                    if (lowerGradeCount == gradeThreshold)
                    {
                        currentLetterGrade--; // Decrement grade char to get a 'higher' grade (e.g. 'F' to 'E')
                        lowerGradeCount = 0; // Reset count to prepare for next threshold check
                    }
                }
                studentIndex++;
            }

            return currentLetterGrade;
        }
    }
}