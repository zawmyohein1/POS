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
    }
    public enum EntityTypes
    {
        User,
        None
    }
}
