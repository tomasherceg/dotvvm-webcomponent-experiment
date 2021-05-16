using System.Collections.Generic;
using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmWebComponents.Extensions;

namespace DotvvmWebComponents.Controls
{
    [ControlMarkupOptions(AllowContent = false)]
    public class Select : CompositeControl
    {
        public static DotvvmControl GetContents(
            IValueBinding<IEnumerable<object>> dataSource,

            [ControlPropertyBindingDataContextChange("DataSource")]
            [CollectionElementDataContextChange(1)]
            IValueBinding<string> itemTextBinding,

            [ControlPropertyBindingDataContextChange("DataSource")]
            [CollectionElementDataContextChange(1)]
            IValueBinding<string> itemValueBinding,

            IValueBinding<string> selectedValue
        )
        {
            return new Repeater()
                {
                    WrapperTagName = "fluent-select",
                    ItemTemplate = new DelegateTemplate(_ =>
                        new HtmlGenericControl("fluent-option")
                            .WithAttribute("value", itemValueBinding)
                            .WithProperty(HtmlGenericControl.InnerTextProperty, itemTextBinding))
                }
                .WithProperty(Fast.BindGroupDescriptor.GetDotvvmProperty("value"), selectedValue)
                .SetBinding(r => r.DataSource, dataSource);
        }
    }
}