using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class Badge : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate,
            [DefaultValue(BadgeAppearance.Neutral)] BadgeAppearance appearance
        )
        {
            return new HtmlGenericControl("fluent-badge")
                .WithAttribute("appearance", appearance.ToString().ToLower())
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }

    public enum BadgeAppearance
    {
        Neutral,
        Accent,
        Lightweight
    }
}