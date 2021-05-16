using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using DotVVM.Compiler.DTOs;
using DotVVM.Compiler.Programs;

#if NETCOREAPP2_0
using Microsoft.Extensions.DependencyModel;
using System.Runtime.Loader;
#endif

namespace DotVVM.Compiler.Resolving
{
    public class AssemblyResolver
    {

      
        internal static int isResolveRunning = 0;

        internal static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            if (Interlocked.CompareExchange(ref isResolveRunning, 1, 0) != 0) return null;

            try
            {
                Program2.WriteInfo($"Resolving assembly `{args.Name}`.");
                var r = LoadFromAlternativeFolder(args.Name);
                if (r != null) return r;
                Program2.WriteInfo($"Assembly `{args.Name}` resolve failed.");

                //We cannot do typeof(something).Assembly, because besides the compiler there are no dlls, doing that will try to load the dll from the disk
                //which fails, so this event is called again call this event...

                return null;
            }
            finally
            {
                isResolveRunning = 0;
            }
        }

        private static Assembly LoadFromAlternativeFolder(string name)
        {
            if (TryLoadAssemblyFromUserFolders(name, out var loadAssemblyFromFile)) return loadAssemblyFromFile;

            return null;
        }
        /// <summary>
        /// Tries to find and load assembly from folder specified in options and environment variable at the start of the app.
        /// </summary>
        private static bool TryLoadAssemblyFromUserFolders(string name, out Assembly loadAssemblyFromFile)
        {
            foreach (var path in Program2.assemblySearchPaths)
            {
                Program2.WriteInfo($"Searching in {path}");
                var assemblyPath = Path.Combine(path, new AssemblyName(name).Name);

                if (File.Exists(assemblyPath + ".dll"))
                {
                    {
                        loadAssemblyFromFile = LoadAssemblyFromFile(assemblyPath + ".dll");
                        Program2.WriteInfo($"Assembly found at {assemblyPath + ".dll"}");

                        return true;
                    }
                }

                if (File.Exists(assemblyPath + ".exe"))
                {
                    {
                        loadAssemblyFromFile = LoadAssemblyFromFile(assemblyPath + ".exe");
                        Program2.WriteInfo($"Assembly found at {assemblyPath + ".exe"}");
                        return true;
                    }
                }
            }

            loadAssemblyFromFile = null;
            return false;
        }

        private static Assembly LoadAssemblyFromFile(string assemblyPath)
        {
            return AssemblyLoader.LoadFile(assemblyPath);
        }

      


     
        /// <summary>
        /// Try to parse package verion (from path/PackageVersion)
        /// </summary>
        private static Version GetPackageVersion(AssemblyFileMetadata meta)
        {
            //To keep this app lightweight, do not add reference to nuget packages.

            //parse stable versions
            var pVer = meta.PackageVersion;
            if (pVer.All(s => char.IsDigit(s) || s == '.'))
            {
                return CreateNewVersion(pVer);
            }
            else
            {
                //skip the suffix
                var version = string.Concat(pVer.TakeWhile(s => char.IsDigit(s) || s == '.'));
                version = version.EndsWith(".") ? version.Substring(0, version.Length - 1) : version;
                return CreateNewVersion(version);
            }
        }

        private static Version CreateNewVersion(string version)
        {
            var level = version.Count(s => s == '.');
            if (level == 2)
            {
                version += ".0";
            }
            else if (level == 1)
            {
                version += ".0.0";
            }

            return new Version(version);
        }

     

        public static void LoadReferencedAssemblies(Assembly wsa, bool recursive = false)
        {
            foreach (var referencedAssembly in wsa.GetReferencedAssemblies())
            {
                if (AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(s => s.GetName().Name == referencedAssembly.Name) != null) continue;
                var assembly = Assembly.Load(referencedAssembly);
                if (recursive)
                    LoadReferencedAssemblies(assembly);
            }
        }
#if NETCOREAPP2_0
        public static void ResolverNetstandard(string webSiteAssemblyPath)
        {
            var dependencyContext = DependencyContext.Load(AssemblyLoader.LoadFile(webSiteAssemblyPath));
            var assemblyNames = ResolveAssemblies(dependencyContext);

            AssemblyLoadContext.Default.Resolving += (context, name) => {
                // find potential assemblies
                var assembly = assemblyNames
                    .Where(a => string.Equals(a.AssemblyFileName, name.Name, StringComparison.CurrentCultureIgnoreCase))
                    .Select(a => new { AssemblyData = a, AssemblyName = AssemblyLoadContext.GetAssemblyName(a.AssemblyFullPath) })
                    .FirstOrDefault(a => a.AssemblyName.Name == name.Name && a.AssemblyName.Version == name.Version);

                if (assembly == null)
                {
                    return null;
                }
                else
                {
                    return AssemblyLoadContext.Default.LoadFromAssemblyPath(assembly.AssemblyData.AssemblyFullPath);
                }
            };
        }
        private static ConcurrentBag<AssemblyData> ResolveAssemblies(DependencyContext dependencyContext)
        {

            return new ConcurrentBag<AssemblyData>(dependencyContext.CompileLibraries
                .SelectMany(l => {
                    try
                    {
                        var paths = l.ResolveReferencePaths();
                        return paths.Select(p => new AssemblyData {
                            Library = l,
                            AssemblyFullPath = p,
                            AssemblyFileName = Path.GetFileNameWithoutExtension(p)
                        });
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Assembly a;
                            if (TryLoadAssemblyFromUserFolders(l.Name, out a))
                            {
                                return new List<AssemblyData>(){ new AssemblyData {
                                    Library = l,
                                    AssemblyFullPath = a.Location,
                                    AssemblyFileName = Path.GetFileNameWithoutExtension(a.Location)
                                } };
                            }
                            return Enumerable.Empty<AssemblyData>();
                        }
                        catch (Exception e)
                        {
                            return Enumerable.Empty<AssemblyData>();
                        }

                    }
                })
                .ToList());
        }
    }
    internal class AssemblyData
    {
        public CompilationLibrary Library { get; set; }
        public string AssemblyFullPath { get; set; }
        public string AssemblyFileName { get; set; }

#endif
    }
}
