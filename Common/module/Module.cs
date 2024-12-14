using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vape_for_Windows.Common.module;

namespace VapeForWindows.Common
{
    internal abstract class Module
    {
        private readonly string name;
        private readonly Category category;
        private bool isEnabled = false;
        private int key;

        public Module(string name, Category category) {
            this.name = name;
            this.category = category;
        }

        public string GetName() { return name; }

        public Category GetModuleType() { return category; }

        public bool GetEnabled() { return isEnabled; }

        public void SetEnabled(bool isEnabled)
        {
            this.isEnabled = isEnabled;
        }

        public int GetKey() { return key; }

        public void SetKey(int key)
        {
            this.key = key;
        }

        public abstract void Draw();
        public abstract void Render();
        public abstract void Update();
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}
