#pragma checksum "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "895fb0c5dc7737c3413887f915438d748089dc8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Books_Details), @"mvc.1.0.view", @"/Views/Books/Details.cshtml")]
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
#line 1 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\_ViewImports.cshtml"
using MBshop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\_ViewImports.cshtml"
using MBshop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\_ViewImports.cshtml"
using MBshop.Service.WebConstants;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\_ViewImports.cshtml"
using MBshop.Service.OutputModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\_ViewImports.cshtml"
using MBshop.Service.StaticProperyes;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"895fb0c5dc7737c3413887f915438d748089dc8e", @"/Views/Books/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8579fc7af4c90e53a0565a581932c1274a5b5cc0", @"/Views/_ViewImports.cshtml")]
    public class Views_Books_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MBshop.Models.Books>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container\">\n        <h1>Details</h1>\n\n        <div>\n            <h4>Books</h4>\n            <hr />\n            <dl class=\"row\">\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 14 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 17 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 20 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Author));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 23 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Author));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 26 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Genre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 29 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Genre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 32 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.RealeseDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 35 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.RealeseDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 38 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Created));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 41 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Created));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 44 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Picture));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 47 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Picture));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 50 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 53 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 56 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 59 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    Rating\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 65 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Raiting));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n                <dt class=\"col-sm-2\">\n                    ");
#nullable restore
#line 68 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dt>\n                <dd class=\"col-sm-10\">\n                    ");
#nullable restore
#line 71 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
               Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </dd>\n            </dl>\n        </div>\n        <div>\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "895fb0c5dc7737c3413887f915438d748089dc8e12073", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 76 "C:\Users\Pencho\Desktop\GitHGub\UniversityWeb\MBshop\Views\Books\Details.cshtml"
                                                                  WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "895fb0c5dc7737c3413887f915438d748089dc8e14344", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MBshop.Models.Books> Html { get; private set; }
    }
}
#pragma warning restore 1591
