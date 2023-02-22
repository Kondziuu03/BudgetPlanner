using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BudgetPlanner.Persistance.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal model)
        {
            return model.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
