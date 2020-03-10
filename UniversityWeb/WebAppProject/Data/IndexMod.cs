using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppProject.Data
{
    public class IndexMod : RazorPageBase, IIndexMOdl
    {
        public string UserId { get; private set; }

        public override void BeginContext(int position, int length, bool isLiteral)
        {
            
        }

        public override void EndContext()
        {
            
        }

        public override void EnsureRenderedBodyOrSections()
        {
            
        }

        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public void GetUserId()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public string ReturnUserId()
        {
            return this.UserId;
        }
    }
}
