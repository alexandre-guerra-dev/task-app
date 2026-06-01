using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Api.Src.Infraestructure.Identity;

public class AppUser : IdentityUser<Guid> {}
