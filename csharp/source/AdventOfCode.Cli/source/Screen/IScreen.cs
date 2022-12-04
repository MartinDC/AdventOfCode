namespace AdventOfCode.Cli.Screen;

/***
    A screen is something that does not itself reacts to input - It only displays some data. 
    If you need to react to user input or data you should use a command instead.
    
    This is the screen that renders the main menu.
*/

public interface IScreen {
    void Draw();
}