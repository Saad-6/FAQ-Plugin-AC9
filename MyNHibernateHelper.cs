using CommerceBuilder.Common;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System;
using System.Reflection;

namespace FAQPlugin
{
    public class MyNHibernateHelper
    {
        private static bool _mappingsAdded = false;

        public ISession GetSession()
        {
            if (!_mappingsAdded)
            {
                AddMappings();
                _mappingsAdded = true;
            }
            return AbleContext.Current.Database.GetSession();
        }

        private void AddMappings()
        {
            var configuration = AbleContext.Current.DatabaseFactory.Configuration;

            // Add the FAQPlugin assembly
            var faqPluginAssembly = Assembly.GetExecutingAssembly();
            try
            {

            configuration.AddAssembly(faqPluginAssembly);
            }
            catch(Exception ex)
            {

            }

            // You may need to specify the exact resource name if the above doesn't work
            var resourceName = "FAQPlugin.FAQ.hbm.xml";
            try
            {
                configuration.AddResource(resourceName, faqPluginAssembly);

            }
            catch (Exception ex) {
            
            }
            faqPluginAssembly = Assembly.GetExecutingAssembly();
            var resourceNames = faqPluginAssembly.GetManifestResourceNames();
        }
    }
}
