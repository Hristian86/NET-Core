#pragma checksum "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "523f3a917152e9b6fdb878e4fd3183cc95099495"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\_ViewImports.cshtml"
using MBshop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\_ViewImports.cshtml"
using MBshop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"523f3a917152e9b6fdb878e4fd3183cc95099495", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1f561337f3c5c860a6e37915e661ede1f9108dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<div class=\"text-center\">\r\n\r\n");
#nullable restore
#line 9 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
      
        if (User.Identity.IsAuthenticated)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1>");
#nullable restore
#line 12 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
       Write(ViewData.Model.firstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 12 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
                                 Write(ViewData.Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 12 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
                                                          Write(ViewData.Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
#nullable restore
#line 13 "C:\Users\Pencho\Desktop\testoveteweba\WebAppProjectCurrent\WebAppProject\WebAppProject\WebAppProject\WebAppProject\Views\Home\Index.cshtml"
        }

    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>
</div>

<button class=""btn btn-lg btn-primary"" onclick=""hello()"">Click me</button>
<div id=""Output""></div>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
