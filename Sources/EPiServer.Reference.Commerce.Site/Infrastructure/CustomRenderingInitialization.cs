using EPiBootstrapArea;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class CustomizedRenderingInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {

            ConfigurationContext.Setup(ctx =>
            {
                ctx.RowSupportEnabled = false;
                ctx.AutoAddRow = false;
                ctx.DisableBuiltinDisplayOptions = false;
                ctx.CustomDisplayOptions.AddRange(new[]
                {
                    new DisplayModeFallback
                    {
                        Name = "One 12th (1/12)",
                        Tag = "displaymode-one-twelfth",
                        LargeScreenWidth = 1,
                        MediumScreenWidth = 1,
                        SmallScreenWidth = 1,
                        ExtraSmallScreenWidth = 1
                    },
                    new DisplayModeFallback
                    {
                        Name = "One 6th (1/6)",
                        Tag = "displaymode-one-sixth",
                        LargeScreenWidth = 2,
                        MediumScreenWidth = 2,
                        SmallScreenWidth = 2,
                        ExtraSmallScreenWidth = 2
                    }
                });
            });
        }

        public void Uninitialize(InitializationEngine context) { }
    }

    public static class HtmlHelpers
    {
        public static IHtmlString RenderBlockByContentReference<T>(this HtmlHelper htmlHelper, ContentReference blockContentReference) where T : IContentData
        {
            if (blockContentReference == ContentReference.EmptyReference)
            {
                return null;
            }


            var block = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>().Get<T>(blockContentReference);

            if (block == null)
            {
                return null;
            }

            var templateResolver = ServiceLocator.Current.GetInstance<TemplateResolver>();
            var template = templateResolver.ResolveMvcTemplate(HttpContext.Current.ContextBaseOrNull(), block);
            return htmlHelper.Partial(template.Name, block);
        }
    }
}