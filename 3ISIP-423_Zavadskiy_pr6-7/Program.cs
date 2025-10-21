using System;
using System.Collections.Generic;

namespace RogueLikeGame
{
    public abstract class Item      // абстрактый класс без аналогов
    {
        public string Name { get; protected set; }      // 'get' - можно свободно прочитать; 'protected set' - установка значения доступна только внутри класса и производных классов
        public int Attack {  get; protected set; }
        public int Defence { get; protected set; }

        protected Item(string name, int attack = 0, int defence = 0)
        {
            Name = name;
            Attack = attack;
            Defence = defence;
        }
    }

    public class Weapon : Item      // создаем класс 'Weapon' который связан с абстрактным классом 'Item'
    {
        public Weapon (string name, int attack) : base (name, attack, 0) { }
    }

    public class Armor : Item     // создаемм класс 'Armor' который связан с абстрактным классом 'Item'
    {
        public Armor (string name, int defence) : base (name, defence, 0) { }
    }


}