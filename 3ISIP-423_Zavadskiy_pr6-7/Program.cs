using System;
using System.Collections.Generic;

namespace TextRoguelike     // создаем пространство для нашей программы
{
    class Program       // создаем класс 'Program'
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}