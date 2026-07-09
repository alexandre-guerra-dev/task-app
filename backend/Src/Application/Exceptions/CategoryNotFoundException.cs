using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared.Exceptions.Base;

namespace backend.Src.Application.Exceptions;

public class CategoryNotFoundException(Guid id) : NotFoundException($"Category {id} Not Found.");
