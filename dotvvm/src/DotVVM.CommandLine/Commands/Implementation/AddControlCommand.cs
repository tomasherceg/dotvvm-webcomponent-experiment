﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.CommandLine.Commands.Templates;
using DotVVM.CommandLine.Metadata;

namespace DotVVM.CommandLine.Commands.Implementation
{
    public class AddControlCommand : CommandBase
    {
        public override string Name => "Add Control";

        public override string Usage => "dotvvm add control <NAME> [-c|--code|--codebehind]\ndotvvm ac <NAME> [-c|--code|--codebehind]";

        public override bool TryConsumeArgs(Arguments args, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            if (string.Equals(args[0], "add", StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(args[1], "control", StringComparison.CurrentCultureIgnoreCase))
            {
                args.Consume(2);
                return true;
            }

            if (string.Equals(args[0], "ac", StringComparison.CurrentCultureIgnoreCase))
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
            name = PathHelpers.EnsureFileExtension(name, "dotcontrol");

            var codeBehind = args.HasOption("-c", "--code", "--codebehind");

            CreateControl(name, codeBehind, dotvvmProjectMetadata);
        }

        private void CreateControl(string viewPath, bool createCodeBehind, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            var codeBehindPath = PathHelpers.ChangeExtension(viewPath, "cs");
            var codeBehindClassName = NamingHelpers.GetClassNameFromPath(viewPath);
            var codeBehindClassNamespace = NamingHelpers.GetNamespaceFromPath(viewPath, dotvvmProjectMetadata.ProjectDirectory, dotvvmProjectMetadata.RootNamespace);

            // create control
            var controlTemplate = new ControlTemplate()
            {
                CreateCodeBehind = createCodeBehind
            };
            if (createCodeBehind)
            {
                controlTemplate.CodeBehindClassName = codeBehindClassName;
                controlTemplate.CodeBehindClassNamespace = codeBehindClassNamespace;
                controlTemplate.CodeBehindClassRootNamespace = dotvvmProjectMetadata.RootNamespace;
            }
            FileSystemHelpers.WriteFile(viewPath, controlTemplate.TransformText());

            // create code behind
            if (createCodeBehind)
            {
                var codeBehindTemplate = new ControlCodeBehindTemplate()
                {
                    CodeBehindClassNamespace = codeBehindClassNamespace,
                    CodeBehindClassName = codeBehindClassName
                };
                FileSystemHelpers.WriteFile(codeBehindPath, codeBehindTemplate.TransformText());
            }
        }
    }
}
