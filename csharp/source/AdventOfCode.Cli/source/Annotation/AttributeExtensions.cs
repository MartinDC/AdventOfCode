namespace AdventOfCode.Cli.Extension;

using AdventOfCode.Cli.Annotation;
using System;

public static class AttributeExtensions {
    public static CommandAttribute? GetConsoleCommandAttribute(this ICommand item) {
        return GetAttribute<CommandAttribute>(item.GetType());
    }

    private static T? GetAttribute<T>(Type t) where T : class {
        Attribute? attribute = Attribute.GetCustomAttribute(t, typeof (T));
        if (attribute is not null && attribute is T attr) {
            return attr;
        }
        return null;
    }
}