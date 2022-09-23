using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Infrastructure.Data.Helper
{
    public enum AuditAction
    {
        Add = 1,
        EditBefore,
        EditAfter,
        Delete,
    }

    public enum AuditModule
    {
        User = 1,
        Department = 2,
    }
}
