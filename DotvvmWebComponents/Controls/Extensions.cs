using System;
using System.Collections;
using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmWebComponents.Controls
{
    public static class Extensions
    {

        public static T WithAttribute<T>(this T control, string name, object value) where T : HtmlGenericControl
        {
            // TODO: is this better than passing ValueOrBinding into the Attributes collection?
            if (value is ValueOrBinding valueOrBinding)
            {
                value = valueOrBinding.BindingOrDefault ?? valueOrBinding.BoxedValue;
            }

            control.Attributes[name] = value;
            return control;
        }

        public static T WithProperty<T>(this T control, DotvvmProperty property, IBinding binding) where T : DotvvmControl
        {
            control.SetBinding(property, binding);
            return control;
        }

        public static T WithProperty<T>(this T control, DotvvmProperty property, object value) where T : DotvvmControl
        {
            control.SetValue(property, value);
            return control;
        }

        public static T WithContent<T>(this T control, Action<IDotvvmRequestContext, DotvvmControl> buildContent) where T : DotvvmControl
        {
            control.Children.Add(new LateControlBuilder()
            {
                Builder = buildContent
            });
            return control;
        }

        public static T WithContent<T>(this T control, ITemplate template) where T : DotvvmControl
        {
            control.Children.Add(new LateControlBuilder()
            {
                Builder = template.BuildContent
            });
            return control;
        }

        public static T WithInnerTextOrContent<T>(this T control, ValueOrBinding<string> text, ITemplate template) where T : HtmlGenericControl
        {
            var hasText = text.ValueOrDefault != null || text.BindingOrDefault != null;
            var hasTemplate = template != null;
            if (hasText && hasTemplate)
            {
                throw new DotvvmControlException(control, "The Text and ContentTemplate properties cannot be set at the same time!");
            }

            if (hasText)
            {
                return control.WithProperty(HtmlGenericControl.InnerTextProperty, text);
            }
            else if (hasTemplate)
            {
                return control.WithContent(template.BuildContent);
            }
            else
            {
                return control;
            }
        }

        public static T WithChildren<T>(this T control, IEnumerable<DotvvmControl> children) where T : DotvvmControl
        {
            foreach (var child in children)
            {
                control.Children.Add(child);
            }
            return control;
        }
    }

    class LateControlBuilder : DotvvmControl
    {

        public Action<IDotvvmRequestContext, DotvvmControl> Builder { get; set; }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            Builder?.Invoke(context, this);
            base.OnInit(context);
        }
    }
}