using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared;

namespace Api.Src.Domain.Exceptions;

public sealed class UserForbiddenException() : ForbiddenException("User not allowed.");