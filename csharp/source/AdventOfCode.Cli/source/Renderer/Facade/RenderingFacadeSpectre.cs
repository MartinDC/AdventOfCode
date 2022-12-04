namespace AdventOfCode.Cli;


/**
    A Facade for Spectre.Console - Simplify rendering to the console and hide Spectre.Console details.
*/

using Spectre.Console;

public class RenderingFacadeSpectre : IRenderingFacade {
    private IRenderingFacade DefaultImpl => this;

    public IRenderingFacade DrawText(string markup, bool newline = true) {
        return DefaultImpl.ChainingFunction(() => {
            return new Markup($"{markup}");
        }, newline);
    }

    public IRenderingFacade DrawLine(string message = "", string? color = "yellow") {
        return DefaultImpl.ChainingFunction(() => {
            return new Rule($"[{color}]{message}[/]").Alignment(Justify.Left);
        });
    }

    public IRenderingFacade DrawTable(string title, List<string> cols, List<string> rows) {
        return DrawTable(t => t.Title(title).AddColumns(cols.ToArray()).AddRow(rows.ToArray()).AddEmptyRow());
    }

    public IRenderingFacade DrawCenteredTable(string title, List<string> cols, List<string> rows) {
        return DrawTable(t => t.Title(title).AddRow(cols.ToArray()).AddEmptyRow().Centered());
    }

    public IRenderingFacade DrawTree(string root, List<ConsoleTreeNode> nodes) {
        ConsoleTreeNode RenderNodeRecursive(Tree context, TreeNode? parent, ConsoleTreeNode node) {
            var nodeparent = parent?.AddNode(node.Markup) ?? context.AddNode(node.Markup);
            nodeparent.Expanded = false;
            if(node.Nodes is null || node.Nodes.Any() == false) { 
                return node; 
            }
            node.Nodes.ForEach(childnode => RenderNodeRecursive(context, nodeparent, childnode));
            return node;
        };

        return DrawTree(root, t => {
            t.Style(Style.Parse("grey")).Guide(TreeGuide.Line);
            nodes.ForEach(node => RenderNodeRecursive(t, null, node));
        });
    }
    
    private IRenderingFacade DrawTree(string root, Action<Tree> performConfiguration) {
        return DefaultImpl.ChainingFunction(() => {
            var tree = new Tree(root);
            if (performConfiguration is not null) {
                performConfiguration(tree);
            }
            return tree;
        });
    }

    private IRenderingFacade DrawTable(Action<Table> performConfiguration) {
        return DefaultImpl.ChainingFunction(() => {
            var table = new Table();
            if (performConfiguration is not null) {
                performConfiguration(table);
            }
            return table;
        });
    }
}
