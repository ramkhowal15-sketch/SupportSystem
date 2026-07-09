using AutoMapper;
using System.Reflection;

namespace Application.Commons.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        ApplyMappingsFromAssembly(GetType().Assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var profileType = typeof(Profile);
        var mapFromType = typeof(IMapFrom<>);
        var createMapFromType = typeof(ICreateMapFrom<>);

        bool HasInterface(Type t, Type interfaceType) =>
            t.GetInterfaces()
             .Any(i => i.IsGenericType &&
                       i.GetGenericTypeDefinition() == interfaceType);

        var types = assembly.GetExportedTypes()
            .Where(t => !t.IsAbstract &&
                        !t.IsInterface &&
                        (HasInterface(t, mapFromType) || HasInterface(t, createMapFromType)))
            .ToList();

        foreach (var type in types)
        {
            
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
                continue;

            var instance = Activator.CreateInstance(type);

            var interfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .ToList();

            foreach (var @interface in interfaces)
            {
                var genericType = @interface.GetGenericTypeDefinition();

                if (genericType == mapFromType || genericType == createMapFromType)
                {
                    var methodName = genericType == mapFromType
                        ? nameof(IMapFrom<object>.CreateMapping)
                        : nameof(ICreateMapFrom<object>.CreateMapping);

                    var mappingMethod = @interface.GetMethod(methodName);

                    mappingMethod?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}