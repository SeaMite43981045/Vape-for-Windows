using Vape_for_Windows.Common.Utils;

namespace Vape_for_Windows.Common.Module
{
    internal class ModuleManager
    {
        private static ModuleManager? instance;

        private readonly List<Module> modules = new List<Module>();

        private ModuleManager() { }

        public List<Module> GetModules() { return modules; }

        public List<Module> GetEnabledModules()
        {
            return modules.Where(m => m.GetEnabled()).ToList();
        }

        public Module GetModuleByName(string name)
        {
            return modules.Where(m => m.GetName() == name).First();
        }

        public List<Module> GetModulesByType(Category type)
        {
            return modules.Where(m => m.GetModuleType() == type).ToList();
        }

        public Module GetModule(Type moduleClass)
        {
            return modules.Where(m => m.GetType() == moduleClass).First();
        }

        public void Load()
        {
            // To Load Modules

            Logger.Log(LogSource.Module, "Modules loaded.");
        }

        public static ModuleManager Get()
        {
            if(instance == null)
            {
                instance = new ModuleManager();
            }

            return instance;
        }
    }
}
