using System;

namespace EllinghamScript.Variables.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class VarMethodAvailableAttribute : Attribute
    {
        public bool IsAvailable { get; set; } = false;
    }
}