using UnityEngine;
using System;

namespace MuscleSystemV01
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class BttnAttribute : PropertyAttribute
    {
        public readonly string FunctionTitle;


        public BttnAttribute(string _FunctionTitle) => FunctionTitle = _FunctionTitle;
    }
}
