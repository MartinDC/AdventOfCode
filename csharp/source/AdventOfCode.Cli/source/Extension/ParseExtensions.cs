namespace AdventOfCode.Cli.Extension;

public static class ParseExtensions {
    public static int AsInt(this string? input) => int.TryParse(input, out var value) ? value : throw new CommandExpectedNumberException();
    public static float AsFloat(this string? input) => float.TryParse(input, out var value) ? value : throw new CommandExpectedDecimalException();
    public static string AsText(this string? input) => (input is not null && input.Length > 0) ? input : throw new CommandExpectedTextException();
    
    public static float? AsNullableFloat(this string? input) => float.TryParse(input, out var value) ? value : null;
    public static int? AsNullableInt(this string? input) => int.TryParse(input, out var value) ? value : null;
    
    public static bool? IsNumeric(this string? input) => int.TryParse(input, out _);

    public static bool HasValue(this string? input) => !string.IsNullOrWhiteSpace(input) && !string.IsNullOrEmpty(input);
}