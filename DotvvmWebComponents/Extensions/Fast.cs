using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmWebComponents.Extensions
{
    [ContainsDotvvmProperties]
    public class Fast
    {

        [AttachedProperty(typeof(bool))]
        public static readonly DotvvmProperty ValueProperty 
            = DelegateActionProperty<object>.Register<Fast>("Value", AddValueProperty);

        private static void AddValueProperty(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            if (control is HtmlGenericControl htmlControl && htmlControl.TagName == "fast-text-field")
            {
                var group = new KnockoutBindingGroup();
                group.Add("value", control, property);
                writer.AddKnockoutDataBind("fast-attr", group);
            }
            else
            {
                throw new DotvvmControlException(control, "$The Fast.Value property is not supported on this control.");
            }
        }
    }
}
