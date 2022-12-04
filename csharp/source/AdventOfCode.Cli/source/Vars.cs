namespace AdventOfCode.Cli.Vars;

public static class Vars {
    public class EmojiVars {
        public string MagnifyingGlass = Spectre.Console.Emoji.Known.MagnifyingGlassTiltedLeft;
        public string Folder = Spectre.Console.Emoji.Known.FileFolder;
    }

    public static readonly string PostitiveReply = "y";
    public static readonly string NegativeReply = "n";
    public static EmojiVars Emoji = new();
}
