#pragma checksum "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76f42345228b55934bc7aa6fdf20f3dc496e6543"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pokemon_Dex), @"mvc.1.0.view", @"/Views/Pokemon/Dex.cshtml")]
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
#line 1 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\_ViewImports.cshtml"
using PokemonPals;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\_ViewImports.cshtml"
using PokemonPals.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76f42345228b55934bc7aa6fdf20f3dc496e6543", @"/Views/Pokemon/Dex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3566d9a2a9cdcf2b710bc5e60a4d29a388dafdbe", @"/Views/_ViewImports.cshtml")]
    public class Views_Pokemon_Dex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PokemonPals.Models.ViewModels.PokemonAndCaughtPokemonViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"dex-header\">\r\n    <h1 class=\"dex-header\">Pokedex</h1>\r\n");
#nullable restore
#line 9 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
      
        decimal totalCount = 80;
        if (Model.PokemonCaught != null)
        {
            totalCount = Model.PokemonCaught.Count();
        }
        decimal totalPercent = (totalCount / Model.AllPokemon.Count()) * 100;
        string percentString = totalPercent.ToString("0.##");


#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"dex-progress-bar\">\r\n            <div class=\"dex-progress-completion\"");
            BeginWriteAttribute("style", " style=\"", 576, "\"", 605, 3);
            WriteAttributeValue("", 584, "width:", 584, 6, true);
#nullable restore
#line 19 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
WriteAttributeValue("", 590, percentString, 590, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 604, "%", 604, 1, true);
            EndWriteAttribute();
            WriteLiteral("></div>\r\n            <p class=\"dex-progress-text\">");
#nullable restore
#line 20 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                    Write(totalCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" of ");
#nullable restore
#line 20 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                                   Write(Model.AllPokemon.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" caught // ");
#nullable restore
#line 20 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                                                                       Write(percentString);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</p>\r\n        </div>\r\n");
            WriteLiteral("</div>\r\n\r\n<div class=\"dex-container\">\r\n");
#nullable restore
#line 26 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
     foreach (var item in Model.AllPokemon)
    {
        string caughtClass = "";
        if (Model.PokemonCaught != null)
        {
            if (Model.PokemonCaught.Contains(item.Id))
            {
                caughtClass = "dex-caught";
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <container");
            BeginWriteAttribute("class", " class=\"", 1089, "\"", 1118, 2);
            WriteAttributeValue("", 1097, "dex-card", 1097, 8, true);
#nullable restore
#line 36 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
WriteAttributeValue(" ", 1105, caughtClass, 1106, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "76f42345228b55934bc7aa6fdf20f3dc496e65436797", async() => {
                WriteLiteral("<img class=\"dex-img\"");
                BeginWriteAttribute("src", " src=", 1202, "", 1224, 1);
#nullable restore
#line 37 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
WriteAttributeValue("", 1207, item.RBSpriteURL, 1207, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 37 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                      WriteLiteral(item.Id);

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
            WriteLiteral("\r\n");
#nullable restore
#line 38 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
              
                string capitalName = char.ToUpper(item.Name[0]) + item.Name.Substring(1);

                string threeDigitId = "";
                if (item.PokedexNumber < 10)
                {
                    threeDigitId = $"00{item.PokedexNumber}";
                }
                else if (item.PokedexNumber < 100)
                {
                    threeDigitId = $"0{item.PokedexNumber}";
                }
                else
                {
                    threeDigitId = $"{item.PokedexNumber}";
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "76f42345228b55934bc7aa6fdf20f3dc496e654310162", async() => {
#nullable restore
#line 56 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                                           Write(threeDigitId);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br /> ");
#nullable restore
#line 56 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                                                                Write(capitalName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
                                          WriteLiteral(item.Id);

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
            WriteLiteral(" <br />\r\n                <img class=\"pokeball-img\" src=\"../wwwroot/pokeball.png\" />\r\n            </div>\r\n        </container>\r\n");
#nullable restore
#line 60 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Dex.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PokemonPals.Models.ViewModels.PokemonAndCaughtPokemonViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591