using System.Diagnostics;

namespace CSharp_Lab5.Model
{
    class Module
    {
        private readonly ProcessModule _module;

        public string Name => _module.ModuleName;
        public string Path => _module.FileName;

        internal Module( ProcessModule module)
        {
            this._module = module;
        }
    }
}
