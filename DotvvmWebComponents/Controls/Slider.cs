using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "Labels")]
    public class Slider : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(0)] int min,
            [DefaultValue(100)] int max,
            [DefaultValue(1)] int step,
            ValueOrBinding<int> value,
            [DefaultValue(null)] List<SliderLabel> labels
        )
        {
            return new HtmlGenericControl("fluent-slider")
                .WithAttribute("min", min.ToString())
                .WithAttribute("max", max.ToString())
                .WithAttribute("step", step.ToString())
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), value)
                .WithChildren(labels ?? Enumerable.Empty<DotvvmControl>());
        }
    }

    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class SliderLabel : CompositeControl
    {
        public static DotvvmControl GetContents(
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate,
            int position
        )
        {
            return new HtmlGenericControl("fluent-slider-label")
                .WithAttribute("position", position.ToString())
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }
}