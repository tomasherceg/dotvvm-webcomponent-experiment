using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DotvvmWebComponents.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        public string Test { get; set; } = "aaa";

        public bool IsChecked1 { get; set; } = true;

        public bool IsChecked2 { get; set; }

        public string RadioSelection { get; set; } = "maverick";

        public string SliderValue { get; set; } = "10";

        public string IsDialogVisible { get; set; } = "hidden";
    }
}
