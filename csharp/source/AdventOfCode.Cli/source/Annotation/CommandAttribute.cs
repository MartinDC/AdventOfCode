namespace AdventOfCode.Cli.Annotation;

using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class CommandAttribute : System.Attribute {
    public string Description { get; private set; }
    public string? Name { get; private set; }

    public CommandAttribute(string description, string? name = null) {
        Description = description;
        Name = name;
    }
}