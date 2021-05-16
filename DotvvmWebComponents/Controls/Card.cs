using DotVVM.Framework.Controls;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class Card : CompositeControl
    {
        public static DotvvmControl GetContents(
            ITemplate contentTemplate
        )
        {
            return new HtmlGenericControl("fluent-card")
                .WithContent(contentTemplate);
        }
    }
}