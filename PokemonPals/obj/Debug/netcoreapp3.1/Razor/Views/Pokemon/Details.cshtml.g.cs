#pragma checksum "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76498d0b8b507831901d9bbba29cf5469d2d1b17"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pokemon_Details), @"mvc.1.0.view", @"/Views/Pokemon/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76498d0b8b507831901d9bbba29cf5469d2d1b17", @"/Views/Pokemon/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3566d9a2a9cdcf2b710bc5e60a4d29a388dafdbe", @"/Views/_ViewImports.cshtml")]
    public class Views_Pokemon_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PokemonPals.Models.ViewModels.CaughtPokemonDexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "CaughtPokemon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Dex", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
  
    string capitalName = char.ToUpper(Model.SelectedPokemon.Name[0]) + Model.SelectedPokemon.Name.Substring(1);
    string threeDigitId = "";
    if (Model.SelectedPokemon.PokedexNumber < 10)
    {
        threeDigitId = $"00{Model.SelectedPokemon.PokedexNumber}";
    }
    else if (Model.SelectedPokemon.PokedexNumber < 100)
    {
        threeDigitId = $"0{Model.SelectedPokemon.PokedexNumber}";
    }
    else
    {
        threeDigitId = $"{Model.SelectedPokemon.PokedexNumber}";
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"global-header\">\r\n        <h1>");
#nullable restore
#line 23 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
       Write(threeDigitId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 23 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(capitalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "76498d0b8b507831901d9bbba29cf5469d2d1b175548", async() => {
                WriteLiteral("<img class=\"pokeball-img\"");
                BeginWriteAttribute("src", " src=\"", 829, "\"", 872, 1);
#nullable restore
#line 24 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
WriteAttributeValue("", 835, Url.Content("~/images/pokeball.png"), 835, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 24 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                                                                WriteLiteral(Model.SelectedPokemon.Id);

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
            WriteLiteral("\r\n    </div>\r\n");
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col\">\r\n        <img class=\"dex-details-official-img\"");
            BeginWriteAttribute("src", " src=", 986, "", 1028, 1);
#nullable restore
#line 30 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
WriteAttributeValue("", 991, Model.SelectedPokemon.OfficialArtURL, 991, 37, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    </div>\r\n\r\n    <div class=\"col dex-details-info\">\r\n");
            WriteLiteral("        <div class=\"dex-details-large-card col\">\r\n            <h4 class=\"dex-details-red-text\">Info</h4>\r\n            <div class=\"row\">\r\n");
            WriteLiteral(@"                <div class=""dex-details-small-card col"">
                    <dl class=""row"">
                        <dt class=""dex-details-red-text col-sm-8"">
                            Type 1
                        </dt>
                        <dd class=""col-sm-4"">
");
#nullable restore
#line 45 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                              string capitalType1 = char.ToUpper(Model.SelectedPokemon.Type1[0]) + Model.SelectedPokemon.Type1.Substring(1); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
#nullable restore
#line 46 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(capitalType1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n");
#nullable restore
#line 49 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                      
                        if (Model.SelectedPokemon.Type2 != null)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <dl class=""row"">
                                <dt class=""dex-details-red-text col-sm-8"">
                                    Type 2
                                </dt>
                                <dd class=""col-sm-4"">
");
#nullable restore
#line 57 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                                      string capitalType2 = char.ToUpper(Model.SelectedPokemon.Type2[0]) + Model.SelectedPokemon.Type2.Substring(1); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    ");
#nullable restore
#line 58 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                               Write(capitalType2);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dd>\r\n                            </dl>\r\n");
#nullable restore
#line 61 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n");
            WriteLiteral("                <div class=\"dex-details-small-card col\">\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 68 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.Height));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-4\">\r\n");
#nullable restore
#line 71 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                              
                                double realHeight = Model.SelectedPokemon.Height * 0.1;
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
#nullable restore
#line 74 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(realHeight.ToString("0.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" m\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 79 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.Weight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-4\">\r\n");
#nullable restore
#line 82 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                              
                                double realWeight = Model.SelectedPokemon.Weight * 0.1;
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
#nullable restore
#line 85 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(realWeight.ToString("0.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" kg\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("        <div class=\"dex-details-large-card col\">\r\n            <h4 class=\"dex-details-red-text\">Stats</h4>\r\n            <div class=\"row\">\r\n");
            WriteLiteral("                <div class=\"dex-details-small-card col\">\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 102 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.HP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-2\">\r\n                            ");
#nullable restore
#line 105 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.HP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 110 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.Attack));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-2\">\r\n                            ");
#nullable restore
#line 113 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.Attack));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 118 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.Defense));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-2\">\r\n                            ");
#nullable restore
#line 121 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.Defense));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n");
            WriteLiteral(@"                <div class=""dex-details-small-card col"">
                    <dl class=""row"">
                        <dt class=""dex-details-red-text col-sm-8"">
                            Special Attack
                        </dt>
                        <dd class=""col-sm-2"">
                            ");
#nullable restore
#line 132 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.SpecialAttack));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </dd>
                    </dl>
                    <dl class=""row"">
                        <dt class=""dex-details-red-text col-sm-8"">
                            Special Defense
                        </dt>
                        <dd class=""col-sm-2"">
                            ");
#nullable restore
#line 140 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.SpecialDefense));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"row\">\r\n                        <dt class=\"dex-details-red-text col-sm-8\">\r\n                            ");
#nullable restore
#line 145 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayNameFor(model => Model.SelectedPokemon.Speed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dt>\r\n                        <dd class=\"col-sm-2\">\r\n                            ");
#nullable restore
#line 148 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(Html.DisplayFor(model => Model.SelectedPokemon.Speed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n\r\n\r\n");
            WriteLiteral("    </div>\r\n");
            WriteLiteral("</div>\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 163 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
  
    if (Model.UserCollection != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <hr />\r\n        <div class=\"dex-details-collection\">\r\n");
#nullable restore
#line 168 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
             foreach (CaughtPokemon PokemonInCollection in Model.UserCollection)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"pokemon-card row\">\r\n                <div class=\"col\">\r\n                    <img class=\"sprite-img\"");
            BeginWriteAttribute("src", " src=", 7368, "", 7413, 1);
#nullable restore
#line 172 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
WriteAttributeValue("", 7373, PokemonInCollection.Pokemon.RBSpriteURL, 7373, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                </div>\r\n\r\n\r\n                <div class=\"col-sm-10\">\r\n");
#nullable restore
#line 177 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                     if (PokemonInCollection.Nickname != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <h4 class=\"dex-details-red-text row\">\r\n                            ");
#nullable restore
#line 180 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(PokemonInCollection.Nickname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - Level ");
#nullable restore
#line 180 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                                                             Write(PokemonInCollection.Level);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                            ");
#nullable restore
#line 181 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(PokemonInCollection.CP);

#line default
#line hidden
#nullable disable
            WriteLiteral(" CP\r\n                        </h4>\r\n");
#nullable restore
#line 183 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <h4 class=\"dex-details-red-text row\">\r\n                            ");
#nullable restore
#line 187 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(capitalName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - Level ");
#nullable restore
#line 187 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                                            Write(PokemonInCollection.Level);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                            ");
#nullable restore
#line 188 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                       Write(PokemonInCollection.CP);

#line default
#line hidden
#nullable disable
            WriteLiteral(" CP\r\n                        </h4>\r\n");
#nullable restore
#line 190 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <div class=\"row\">\r\n                        <div class=\"col-sm-7\">\r\n");
#nullable restore
#line 194 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                             if (PokemonInCollection.isFavorite == true)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <p>Favorite</p>\r\n");
#nullable restore
#line 197 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 199 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                             if (PokemonInCollection.isTradeOpen == true)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <p>Open to Trade</p>\r\n");
#nullable restore
#line 202 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                        <div class=\"dex-details-right-align col-sm-5\">\r\n                            <p>Caught on ");
#nullable restore
#line 205 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
                                    Write(PokemonInCollection.DateCaught.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"row\">\r\n");
            WriteLiteral("                    </div>\r\n\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 215 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 217 "C:\Users\NewForce\workspace\bangazon\capstone\PokemonPals\Views\Pokemon\Details.cshtml"
    }


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "76498d0b8b507831901d9bbba29cf5469d2d1b1726241", async() => {
                WriteLiteral("Back to Dex");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PokemonPals.Models.ViewModels.CaughtPokemonDexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
