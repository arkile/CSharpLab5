using System.Diagnostics;

namespace CSharp_Lab5.Model
{
    class Module
    {
        private readonly ProcessModule _module;

        public string Name => _module?.ModuleName??"System process";
        public string Path => _module?.FileName??"Permission denied";

        internal Module( ProcessModule module)
        {
            this._module = module;
        }

        internal Module()
        {
            _module = null;
        }
    }
}
