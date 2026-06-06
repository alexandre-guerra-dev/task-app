using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared;

namespace Api.Src.Application.Exceptions;

public class SubTaskItemNotFoundException(Guid id) : NotFoundException($"Sub Task {id} Not Found.");