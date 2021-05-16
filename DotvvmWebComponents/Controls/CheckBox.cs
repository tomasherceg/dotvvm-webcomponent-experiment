#nullable enable
using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class CheckBox : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate,
            IValueBinding<bool> @checked
        )
        {
            return new HtmlGenericControl("fluent-checkbox")
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("checked"), @checked)
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }
}