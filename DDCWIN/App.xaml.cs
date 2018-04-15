using DDCCI;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace DDCWIN
{
    public partial class App : Application
    {
        private static readonly DisplayService displayService = new DisplayService();

        public static void InitializeComposition(object instance)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));

            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue<IDisplayService>(displayService);
            container.ComposeParts(instance);
        }
    }
}
