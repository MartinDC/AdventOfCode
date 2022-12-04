namespace AdventOfCode.Common;

public static class UtilFuncs {
    public static int FindHighestNumber(this List<int> item) {
        int? age = 0;
        return item.LinearSearchNumberWithPred(x => {
            if (x > age) { age = x; }
            return age <= x;
        });
    }

    public static int LinearSearchNumberWithPred(this List<int> item, Func<int, bool> pred) {
        int found = int.MinValue;
        for (var i = 0; i < item.Count; i++) {
            if (item[i] != int.MinValue && pred(item[i])) { 
                found = item[i];
            }
        }
        return found;
    }
}