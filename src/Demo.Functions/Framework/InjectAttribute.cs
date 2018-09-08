using System;
using Microsoft.Azure.WebJobs.Description;

namespace Demo.Functions.Framework
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InjectAttribute : Attribute { }
}