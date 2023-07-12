using Rocket.Core.Plugins;

namespace ArchitectureInfo
{
    public class ArchitectureInfoPlugin : RocketPlugin
    {
        public static ArchitectureInfoPlugin Instance => instance;
        private static ArchitectureInfoPlugin instance;

        protected override void Load()
        {
            instance = this;
        }

        protected override void Unload()
        {

        }
    }
}
