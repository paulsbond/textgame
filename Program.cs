using System;
using System.Threading;

namespace textgame
{
  class Program
  {
    static int Rows = 5;
    static int Cols = 10;
    static int Left = 1;
    static int Top = 1;

    static void PrintMap()
    {
      string Repeat(int x, char c) =>  "".PadLeft(x, c);
      Console.WriteLine($"+{Repeat(Cols, '-')}+");
      for (int i = 0; i < Rows; i++) {
        Console.WriteLine($"|{Repeat(Cols, ' ')}|");
      }
      Console.WriteLine($"+{Repeat(Cols, '-')}+");
    }

    static void SetPlayer(int left, int top)
    {
      if (left < 1 || top < 1) return;
      if (left > Cols || top > Rows) return;
      Console.SetCursorPosition(Left, Top);
      Console.Write(" ");
      Console.ForegroundColor = ConsoleColor.Red;
      Console.SetCursorPosition(left, top);
      Console.Write("O");
      Console.ResetColor();
      Left = left;
      Top = top;
      Console.SetCursorPosition(0, Rows + 2);
    }

    static void SetTarget()
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.SetCursorPosition(Cols + 1, 3);
      Console.Write("X");
      Console.ResetColor();
      Console.SetCursorPosition(0, Rows + 2);
    }

    static bool Shoot()
    {
      if (Left < Cols)
      {
        Console.SetCursorPosition(Left + 1, Top);
        Console.Write("-");
        Console.SetCursorPosition(0, Rows + 2);
        Thread.Sleep(100);
        for (int i = Left + 1; i < Cols; i++)
        {
          Console.SetCursorPosition(i, Top);
          Console.Write(" -");
          Console.SetCursorPosition(0, Rows + 2);
          Thread.Sleep(100);
        }
        Console.SetCursorPosition(Cols, Top);
        Console.Write(" ");
        Console.SetCursorPosition(0, Rows + 2);
      }
      return Top == 3;
    }

    static void Main(string[] args)
    {
      Console.Clear();
      PrintMap();
      SetPlayer(1, 1);
      SetTarget();
      while (true)
      {
        var key = Console.ReadKey(true);
        if (key.KeyChar == 'q') break;
        if (key.KeyChar == 'w') SetPlayer(Left, Top - 1);
        if (key.KeyChar == 'a') SetPlayer(Left - 1, Top);
        if (key.KeyChar == 's') SetPlayer(Left, Top + 1);
        if (key.KeyChar == 'd') SetPlayer(Left + 1, Top);
        if (key.KeyChar == ' ')
        {
          var hit = Shoot();
          if (hit)
          {
            Console.Clear();
            Console.WriteLine(@"
######  ####### ####### #     # 
#     # #     # #     # ##   ## 
#     # #     # #     # # # # # 
######  #     # #     # #  #  # 
#     # #     # #     # #     # 
#     # #     # #     # #     # 
######  ####### ####### #     #");
            break;
          }
        }
      }
    }
  }
}
