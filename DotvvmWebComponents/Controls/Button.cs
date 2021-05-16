using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class Button : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate,
            ICommandBinding click,
            [DefaultValue(ButtonAppearance.Neutral)] ButtonAppearance appearance
        )
        {
            return new HtmlGenericControl("fluent-button")
                .WithAttribute("appearance", appearance.ToString().ToLower())
                .WithProperty(Events.ClickProperty, click)
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }

    public enum ButtonAppearance
    {
        Neutral,
        Accent,
        Lightweight,
        Outline,
        Stealth
    }
}
