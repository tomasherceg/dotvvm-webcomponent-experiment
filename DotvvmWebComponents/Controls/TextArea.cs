using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false)]
    public class TextArea : CompositeControl
    {
        public static DotvvmControl GetContents(
            ValueOrBinding<string> text
        )
        {
            return new HtmlGenericControl("fluent-text-area")
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), text);
        }
    }
}