using System;

namespace BubblePopper.DesktopGL
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BubblePopperGame())
                game.Run();
        }
    }
}
