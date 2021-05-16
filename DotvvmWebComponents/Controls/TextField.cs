using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false)]
    public class TextField : CompositeControl
    {
        public static DotvvmControl GetContents(
            ValueOrBinding<string> text
        )
        {
            return new HtmlGenericControl("fluent-text-field")
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), text);
        }
    }
}