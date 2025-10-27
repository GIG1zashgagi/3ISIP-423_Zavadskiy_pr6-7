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
    public enum EnemyType
    {
        Goblin,
        Skeleton,
        Mage,
        BossVvg,
        BossKovalsky,
        BossArchmage,
        BossPestov
    }
    public class Item
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsWeapon { get; set; }

        public Item(string name, int attack, int defense, bool isWeapon)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            IsWeapon = isWeapon;
        }

        public override string ToString()
        {
            return $"{Name} (Атака: {Attack}, Защита: {Defense})";
        }
    }
    public class Player
    {
        public int MaxHP { get; set; } = 100;
        public int HP { get; set; }
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
        public bool IsFrozen { get; set; }

        public Player()
        {
            HP = MaxHP;
            Weapon = new Item("Ржавый меч", 5, 0, true);
            Armor = new Item("Простая броня", 0, 5, false);
        }

        public int GetAttack()
        {
            return Weapon?.Attack ?? 0;
        }

        public int GetDefense()
        {
            return Armor?.Defense ?? 0;
        }

        public void Heal()
        {
            HP = MaxHP;
        }

        public override string ToString()
        {
            return $"Игрок - HP: {HP}/{MaxHP}, Атака: {GetAttack()}, Защита: {GetDefense()}";
        }
    }
    public abstract class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public EnemyType Type { get; set; }

        public abstract bool TrySpecialAbility(Player player, Random random);
        public abstract int CalculateDamage(Player player, Random random);

        public bool IsAlive => HP > 0;

        public override string ToString()
        {
            return $"{Name} - HP: {HP}/{MaxHP}, Атака: {Attack}, Защита: {Defense}";
        }
    }
}