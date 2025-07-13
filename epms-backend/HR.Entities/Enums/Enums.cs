using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Enums
{
    public class Enums
    {
        public enum KPYCycle
        {
            Yearly = 1,
            Midterm = 2,
            Quarterly = 3
        }

        public enum KPYType
        {
            Accumulative = 1,
            Average = 2,
            Last = 3,
            Maximum = 4,
            Minimum = 5
        }

        public enum ResultUnit
        {
            Percentage = 1,
            Value = 2,
        }


        public enum ResultPeriods
        {
            Q1 = 1,
            Q2 = 2,
            Q3 = 3,
            Q4 = 4
        }

        public enum UnitType
        {
            Sector = 1,
            Unit = 2
        }

        public enum QuotaType
        {
            None = 0,
            PlannedQuota = 1,
            RemainingEmployee = 2,
        }


        public enum EmployeeAssessmentStatus
        {
            Planning = 0,
            Review = 1,
            Final = 2,
            Completed = 3
        }

        public enum EducationType
        {
            None = 0,
            Preventive = 1,
            Corrective = 2,
        }

        public enum Priority
        {
            None = 0,
            Low = 1,
            Normal = 2,
            High = 3
        }


        public enum EducationStatus
        {
            NotExecuted = 1,
            Executed = 2
        }

        enum MajorCode
        {
            UnitTypes = 1,
            Countries = 2,
            KPYCycle = 3,
            KPYType = 4,
            ResultUnit = 5,
            ResultPeriods = 6,
            CompetenceLevel = 7,
            QuotaType = 9,
            Currency = 10,
            EmployeeAssessmentStatus = 11,
            EducationType = 12,
            EduationMethod = 13,
            Priority = 14,
            EducationStatus = 15
        }
    }
}