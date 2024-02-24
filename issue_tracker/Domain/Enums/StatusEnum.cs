using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PriorityStatusEnum
    {
        Low = 0, 
        Medium = 1,
        High = 2,
        Critical = 3
    }

    public enum IssueStatusEnum
    {
        Open = 0,
        Close = 1
    }

    public enum StatusEnum
    {
        All = 0,
        Open = 1,
        Close = 2
    }

    public enum IssueAssigneUserType
    {
        IssueCreator = 0,
        IssueEditor = 1,
        IssueViewer = 2
    }
}
