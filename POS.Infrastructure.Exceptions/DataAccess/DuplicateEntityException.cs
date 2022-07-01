﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions
{
    public class DuplicateEntityException:Exception
    {
        public DuplicateEntityException() : base() { }
        public DuplicateEntityException(string message) : base(message) { }
        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException){ }
    }
}
