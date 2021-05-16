using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class Switch : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate,
            IValueBinding<bool> @checked
        )
        {
            return new HtmlGenericControl("fluent-switch")
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("checked"), @checked)
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }
}