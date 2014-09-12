﻿using System.CodeDom;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace Cvent.SchemaToPoco.Json.Class.Property.Types
{
    /// <summary>
    /// Represents the Boolean schema property type.
    /// </summary>
    public class BooleanPropertyType : JsonSchemaPropertyType
    {
        /// <see cref="JsonSchemaPropertyType"/>
        public override CodeTypeReference GetType(JsonPropertyInfo info)
        {
            var required = info.Definition.Required.HasValue && info.Definition.Required.Value;
            var defaultPresent = GetDefaultAssignment(info) != null;
            return required || defaultPresent 
                ? new CodeTypeReference(typeof (bool))
                : new CodeTypeReference(typeof (bool?));
        }

        /// <see cref="JsonSchemaPropertyType"/>
        public override CodeAssignStatement GetDefaultAssignment(JsonPropertyInfo info)
        {
            if (info.Definition.Default == null || info.Definition.Default.Type != JTokenType.Boolean)
                return null;

            return CreatePrimitiveDefaultAssignment(info.FieldName, info.Definition.Default.Value<bool>());
        }
    }
}
