using System.Collections.Generic;
using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "RadioButtons")]
    public class RadioGroup : CompositeControl
    {
        public static DotvvmControl GetContents(
            [MarkupOptions(MappingMode = MappingMode.InnerElement)] List<Radio> radioButtons,
            ValueOrBinding<string> groupName,
            ValueOrBinding<string> value
        )
        {
            return new HtmlGenericControl("fluent-radio-group")
                .WithAttribute("name", groupName)
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), value)
                .WithChildren(radioButtons);
        }
    }

    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = "ContentTemplate")]
    public class Radio : CompositeControl
    {
        public static DotvvmControl GetContents(
            ValueOrBinding<string> value,
            [DefaultValue(null)] ValueOrBinding<string> text,
            [DefaultValue(null)] ITemplate contentTemplate
        )
        {
            return new HtmlGenericControl("fluent-radio")
                .WithAttribute("value", value)
                .WithInnerTextOrContent(text, contentTemplate);
        }
    }
}