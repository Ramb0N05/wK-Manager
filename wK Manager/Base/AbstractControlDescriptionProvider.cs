using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    public class AbstractControlDescriptionProvider<TAbsctract, TBase> : TypeDescriptionProvider
    {
        public AbstractControlDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(TAbsctract)))
        { }

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicFields)]
        public override Type GetReflectionType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicFields)] Type objectType, object? instance)
        {
            return objectType == typeof(TAbsctract)
                ? typeof(TBase)
                : base.GetReflectionType(objectType, instance);
        }

        public override object? CreateInstance(IServiceProvider? provider, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type objectType, Type[]? argTypes, object[]? args)
        {
            if (objectType == typeof(TAbsctract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}
