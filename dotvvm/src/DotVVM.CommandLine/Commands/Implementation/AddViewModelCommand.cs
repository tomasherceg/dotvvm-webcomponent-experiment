﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.CommandLine.Commands.Templates;
using DotVVM.CommandLine.Metadata;

namespace DotVVM.CommandLine.Commands.Implementation
{
    public class AddViewModelCommand : CommandBase
    {
        public override string Name => "Add ViewModel";

        public override string Usage => "dotvvm add viewmodel <NAME>\ndotvvm avm <NAME>";

        public override bool TryConsumeArgs(Arguments args, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            if (string.Equals(args[0], "add", StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(args[1], "viewmodel", StringComparison.CurrentCultureIgnoreCase))
            {
                args.Consume(2);
                return true;
            }

            if (string.Equals(args[0], "avm", StringComparison.CurrentCultureIgnoreCase))
            {
                args.Consume(1);
                return true;
            }

            return false;
        }

        public override void Handle(Arguments args, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            var name = args[0];
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidCommandUsageException("You have to specify the NAME.");
            }
            name = PathHelpers.EnsureFileExtension(name, "cs");

            if (PathHelpers.IsCurrentDirectory(dotvvmProjectMetadata.ProjectDirectory) && !name.Contains("/") && !name.Contains("\\"))
            {
                name = "ViewModels/" + name;
            }

            CreateViewModel(name, dotvvmProjectMetadata);
        }

        private void CreateViewModel(string viewModelPath, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            var viewModelName = NamingHelpers.GetClassNameFromPath(viewModelPath);
            var viewModelNamespace = NamingHelpers.GetNamespaceFromPath(viewModelPath, dotvvmProjectMetadata.ProjectDirectory, dotvvmProjectMetadata.RootNamespace);
            
            // create viewmodel
            var viewModelTemplate = new ViewModelTemplate()
            {
                ViewModelName = viewModelName,
                ViewModelNamespace = viewModelNamespace
                // TODO: BaseViewModel
            };
            FileSystemHelpers.WriteFile(viewModelPath, viewModelTemplate.TransformText());
        }
    }
}
