using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grades.DataModel
{
    public partial class Grade
    {
        public bool ValidateAssessmentDate(DateTime _Date)
        {
            if (_Date > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("Assessment Date", "Assessment date must be on or before the current date");
            }
            else
            {
                return true;
            }
        }

        public bool ValidateAssessmentGrade(string assessment)
        {
            Match matchGrade = Regex.Match(assessment, @"^[A-E][+-]?$");
            if (!matchGrade.Success)
            {
                throw new ArgumentOutOfRangeException("Assessment", "Assessment grade must be in the range A+ to E-");
            }
            else
            {
                return true;
            }
        }
    }
}
