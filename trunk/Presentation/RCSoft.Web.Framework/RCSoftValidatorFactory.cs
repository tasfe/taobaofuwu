﻿using System;
using FluentValidation.Attributes;
using FluentValidation;
using RCSoft.Core.Infrastructure;

namespace RCSoft.Web.Framework
{
    public class RCSoftValidatorFactory:AttributedValidatorFactory
    {
        public override IValidator GetValidator(Type type)
        {
            var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
            if ((attribute != null) && (attribute.ValidatorType != null))
            {
                var instance = EngineContext.Current.ContainerManager.ResolveUnregistered(attribute.ValidatorType);
                return instance as IValidator;
            }
            return null;
        }
    }
}
