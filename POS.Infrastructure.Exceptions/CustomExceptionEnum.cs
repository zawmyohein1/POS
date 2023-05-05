using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions
{
    public enum CustomExceptionEnum
    {
        Success,
        UnknownException,
        NoUserInfoAvailiable,
        UserEmailAlreadyExists,
        InvalidEmail,
        InvalidPassword,
        NoDepartmentInfoAvailiable,
        InvalidDepartmentName,
        DepartmentNameAlreadyExist,
        InvalidOccupationName,
        InvalidDepartmentID,
        NoOccupationInfoAvailiable,
        NoSupplierInfoAvailiable,
        InvalidSupplierName,
        SupplierNameAlreadyExist,
        SupplierEmailAlreadyExist,
    }
    public enum EntityTypes
    {
        User,
        None
    }
}
