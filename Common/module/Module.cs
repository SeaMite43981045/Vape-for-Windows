namespace Vape_for_Windows.Common.Module
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

        public virtual void Draw() { }
        public virtual void Render() { }
        public virtual void Update() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
