using System;
using System.Collections.Generic;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }
            
            if (averageGrade == 0)
            {
                return 'F';
            }
            
            // Build list of sorted grades
            var sortedGrades = new List<double>();
            foreach (Student student in Students)
            {
                sortedGrades.Add(student.AverageGrade);
            }
            sortedGrades.Sort();

            // Find out where given grade fits in range	
            var comparisonGrade = sortedGrades[0];
            var position = 1;
            while (averageGrade > comparisonGrade && position < sortedGrades.Count)
            {
                position++;
                comparisonGrade = sortedGrades[position - 1];
            }

            // Calculate how many letter grades higher the given value is
            var gradeIncrease = (position * 5) / sortedGrades.Count; // Value is effectively floored
            return (char)(Convert.ToInt16('F') - gradeIncrease);
        }
    }
}