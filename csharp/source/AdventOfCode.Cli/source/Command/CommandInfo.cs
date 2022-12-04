namespace AdventOfCode.Cli;

public class CommandInfo {
    public string Description { get; set; }
    public string Name { get; set; }

    public CommandInfo(string name = default!, string description = default!){
        Description = description;
        Name = name;
    }
}