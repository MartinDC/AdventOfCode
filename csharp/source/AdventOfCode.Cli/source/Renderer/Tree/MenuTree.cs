namespace AdventOfCode.Cli;

using AdventOfCode.Cli.Extension;

public class MenuTree {
    public List<ConsoleTreeNode> Nodes { get; set; } = default!;

    public List<ConsoleTreeNode> BuildTreeNodesFromMenuStructure(Dictionary<int, ICommand> menuStructure) {
        return Nodes = menuStructure.Values.Select(x => x.TranslateTreeNode()).ToList();
    }
}