using CommerceBuilder.Common;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
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
            var modelMapper = new ModelMapper();
            modelMapper.AddMappings(new[] { typeof(FAQMap) });
            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(hbmMapping);
        }
    }
}