using System.Reflection;
using System.Resources;

namespace MultilingualMaui
{
    [ContentProperty(nameof(Text))]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "MultiLingualMaui.Resources.AppResources";
        static readonly Lazy<ResourceManager> resmgr = new(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(Text == null)
            {
                return "";
            }

            var ci = Thread.CurrentThread.CurrentUICulture;
            var translation = resmgr.Value.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $"Key '{Text}' was not found in resources '{ResourceId}' for culture '{ci.Name}'.", nameof(Text));
#else
                translation = Text; // Fallback to the key itself if translation not found
#endif
            }

            return translation;
        }
    }
}
