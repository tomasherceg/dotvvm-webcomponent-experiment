using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmWebComponents.ViewModels.FluentUI
{
    public class BasicInputsViewModel : DotvvmViewModelBase
    {

        public int Count { get; set; }


        public bool IsChecked1 { get; set; }

        public bool IsChecked2 { get; set; } = true;


        public string RadioSelection { get; set; } = "maverick";


        public string Text { get; set; } = "hello web components";


        public int SliderValue { get; set; } = 10;


        public List<string> ComboBoxOptions { get; set; } = new List<string>()
        {
            "Please Please Me",
            "With The Beatles",
            "A Hard Day's Night",
            "Beatles for Sale",
            "Help!",
            "Rubber Soul",
            "Revolver",
            "Sgt. Pepper's Lonely Hearts Club Band",
            "Magical Mystery Tour",
            "The Beatles",
            "Yellow Submarine",
            "Abbey Road",
            "Let It Be"
        };

        public string ComboBoxSelection { get; set; } = "";

    }


}

