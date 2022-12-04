namespace AdventOfCode.Cli;

using System;

public class CommandException : Exception {
    public CommandException(string message) : base($"{message}") { }
}

public class CommandParseException : CommandException {
    public CommandParseException(string message) : base($"{message}") { }
}

public class CommandExpectedTextException : CommandException {
    public CommandExpectedTextException() : base($"You must write at least one value") { }
}

public class CommandExpectedNumberException : CommandException {
    public CommandExpectedNumberException() : base($"You did not input a correct number") { }
}

public class CommandExpectedDecimalException : CommandException {
    public CommandExpectedDecimalException() : base($"You did not input a decimal value") { }
}

public class CommandExpectedConfirmationException : CommandException {
    public CommandExpectedConfirmationException() : base($"You must confirmation (y/n)") { }
}