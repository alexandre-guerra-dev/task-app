using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Src.Shared;

public abstract class ConflictException(string message) : Exception(message);