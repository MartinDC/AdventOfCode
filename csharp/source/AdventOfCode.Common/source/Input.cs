namespace AdventOfCode.Common;

public class InputDataReader {
    public static List<string> SplitStringOnSeparator(string value, string separator) {
        return value.Split(separator)?.ToList() ?? new();
    }

    public static List<(string left, string right)> SplitTupleOnSeparator(string value, string separator) {
        return SplitStringOnSeparator(value, "\n").Select(x => x.Split(separator))
            .Select(split => (split.First(), split.Last())).ToList();
    }

    public static async Task<string> ReadInputAsTextAsync(string year, string day) {
        var combinedPath = $"{AppDomain.CurrentDomain.BaseDirectory}/{year}/day{day}.dat";
        if (!File.Exists(combinedPath)) { 
            throw new FileNotFoundException(combinedPath); 
        };

        return await File.ReadAllTextAsync(combinedPath, System.Text.Encoding.UTF8);
    }
}