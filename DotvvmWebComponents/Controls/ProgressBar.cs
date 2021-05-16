using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false)]
    public class Progress : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(0)] int min,
            [DefaultValue(100)] int max,
            [DefaultValue(1)] int step,
            ValueOrBinding<int> value
        )
        {
            return new HtmlGenericControl("fluent-progress")
                .WithAttribute("min", min.ToString())
                .WithAttribute("max", max.ToString())
                .WithAttribute("step", step.ToString())
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), value);
        }
    }
}