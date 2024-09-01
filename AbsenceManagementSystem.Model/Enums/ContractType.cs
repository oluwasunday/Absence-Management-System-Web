﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceManagementSystem.Model.Enums
{
    public enum ContractType
    {
        PartTime,
        FullTime
    }
    public enum LeaveStatus
    {
        Pending = 0,
        Approved,
        Cancelled
    }
    public enum LeaveTypes
    {
        Unpaid = 0,
        CasualLeave,
        SickLeave,
        Vacation,
        Maternity,
        Paternity
    }
}
