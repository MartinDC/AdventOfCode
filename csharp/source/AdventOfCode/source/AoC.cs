using AdventOfCode.Cli;

new AOCMain().BootstrapApplication();

public class AOCMain : ConsoleApp {
    public void BootstrapApplication() {
        Console.Clear();
        Run(new() {
            Commands = new() {
                [0] = AdventOfCode.Registry.Get("2022")
            }
        });
    }
}

