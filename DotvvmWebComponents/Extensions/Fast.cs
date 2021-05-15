using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmWebComponents.Extensions
{
    [ContainsDotvvmProperties]
    public class Fast
    {

        [AttachedProperty(typeof(object))]
        [PropertyGroup("Bind-")]
        public static DotvvmPropertyGroup BindGroupDescriptor =
            DelegateActionPropertyGroup<object>.Register<Fast>("Bind-", "Bind", AddBindProperty);

        private static void AddBindProperty(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmPropertyGroup group, DotvvmControl control, IEnumerable<DotvvmProperty> properties)
        {
            var bindingGroup = new KnockoutBindingGroup();
            foreach (var prop in properties)
            {
                bindingGroup.Add(prop.Name.Split(':')[1], control, prop);
            }
            writer.AddKnockoutDataBind("fast-attr", bindingGroup);
        }

        [AttachedProperty(typeof(IEnumerable))]
        public static DotvvmProperty OptionsProperty =
            DelegateActionProperty<IEnumerable>.Register<Fast>("Options", AddOptionsProperty);

        private static void AddOptionsProperty(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            writer.AddKnockoutDataBind("fast-options", control, property);
        }


        [AttachedProperty(typeof(object))]
        public static DotvvmProperty RowsDataProperty =
            DelegateActionProperty<object>.Register<Fast>("RowsData", AddRowsDataProperty);

        private static void AddRowsDataProperty(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            writer.AddKnockoutDataBind("fast-rowsData", control, property);
        }

    }
}
