using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Class)]
public class PurlPropertyValidation : ValidationAttribute {
    public override bool IsValid(object value) {
        var typeInfo = value.GetType();
        var propertyInfo = typeInfo.GetProperties();
        foreach (var property in propertyInfo) {
            if (null != property.GetValue(value, null)) {
                return true;
            }
        }
        return false;
    }
}