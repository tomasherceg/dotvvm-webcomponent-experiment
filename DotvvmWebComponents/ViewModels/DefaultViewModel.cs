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
		public string Test { get; set; }
    }
}
