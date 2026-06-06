using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared;

namespace Api.Src.Api.Exceptions;

public class UserUnauthorizedException() : UnauthorizedException("User Not Authorized.");
