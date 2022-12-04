namespace AdventOfCode.Cli;

/**
    A Facade for Console operations - Simplify rendering to the console.
*/

using System;
using AdventOfCode.Cli.Extension;

public static class ConsolePrompt {
    public static class ConsoleMetrics {
        public static int PositionLeft() => Console.GetCursorPosition().Left;
        public static int PositionTop() => Console.GetCursorPosition().Top;
        public static int Height() => Console.BufferHeight;
        public static int Width() => Console.BufferWidth;
        public static int Left() => Console.CursorLeft;
        public static int Top() => Console.CursorTop;
    }

    private static readonly IRenderingFacade Renderer = new RenderingFacadeSpectre();
    private static readonly IPromptRenderingFacade PromptRenderer = new PromptRenderingFacadeSpectre();
    public static IPromptRenderingFacade GetPrompt() => PromptRenderer;
    public static IRenderingFacade GetRenderer() => Renderer;

    public static int? PromptForInputNumberDefault() => PromptForInputDefault().AsInt();
    public static string? PromptForInputTextDefault() => PromptForInputDefault().AsText();
    public static string? PromptForInputDefault() => PromptForInput("[dim blue]-->[/]");
    
    public static int? PromptForInputNumber(string message) => PromptForInput(message).AsInt();
    public static string? PromptForInputText(string message) => PromptForInput(message).AsText();
    public static string? PromptForInput(string message) => GetPrompt().DrawPrompt(message);
    public static void Clear() => Console.Clear();
}