using Microsoft.Extensions.Localization;
using System.Reflection;

namespace API3.Models
{
    public class LanguagService
    {
        private readonly IStringLocalizer _localizer;

        public LanguagService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            return _localizer[key];
        }
    }
}
