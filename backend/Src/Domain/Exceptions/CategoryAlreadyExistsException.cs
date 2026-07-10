using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared.Exceptions.Base;

namespace backend.Src.Domain.Exceptions;

public class CategoryAlreadyExistsException(string categoryName) : BusinessException($"Category With Name '{categoryName}' Already Exists.");