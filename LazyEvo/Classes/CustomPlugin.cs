namespace LazyEvo.Classes
{
    using System;
    using System.Runtime.CompilerServices;

    internal class CustomPlugin
    {
        public CustomPlugin(string assemblyName, string assemblyFunctionName)
        {
            this.AssemblyName = assemblyName;
            this.AssemblyFunctionName = assemblyFunctionName;
        }

        public override string ToString() => 
            this.AssemblyFunctionName;

        public string AssemblyName { get; private set; }

        public string AssemblyFunctionName { get; private set; }
    }
}

