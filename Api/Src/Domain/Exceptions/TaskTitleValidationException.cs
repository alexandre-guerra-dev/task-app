using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared;

namespace Api.Src.Domain.Exceptions;

public class TaskTitleValidationException() : ValidationException("The title length must be greather than 2 characters.");