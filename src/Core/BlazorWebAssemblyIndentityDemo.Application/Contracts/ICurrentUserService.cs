﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Contracts
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}