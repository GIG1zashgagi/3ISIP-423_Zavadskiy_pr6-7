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
    public class Goblin : Enemy
    {
        private double critChance = 0.2;

        public Goblin(Random random)
        {
            Name = "Гоблин";
            Type = EnemyType.Goblin;
            MaxHP = 30 + random.Next(-5, 6);
            HP = MaxHP;
            Attack = 8 + random.Next(-2, 3);
            Defense = 3 + random.Next(-1, 2);
        }

        public override bool TrySpecialAbility(Player player, Random random)
        {
            return false; 
        }

        public override int CalculateDamage(Player player, Random random)
        {
            int baseDamage = Attack;
            if (random.NextDouble() < critChance)
            {
                Console.WriteLine("Критический удар гоблина!");
                baseDamage = (int)(baseDamage * 1.5);
            }
            return Math.Max(1, baseDamage - player.GetDefense());
        }
    }
    public class Skeleton : Enemy
    {
        public Skeleton(Random random)
        {
            Name = "Скелет";
            Type = EnemyType.Skeleton;
            MaxHP = 40 + random.Next(-5, 6);
            HP = MaxHP;
            Attack = 6 + random.Next(-2, 3);
            Defense = 5 + random.Next(-1, 2);
        }

        public override bool TrySpecialAbility(Player player, Random random)
        {
            return false; 
        }

        public override int CalculateDamage(Player player, Random random)
        {
            return Attack;
        }
    }
    public class Mage : Enemy
    {
        private double freezeChance = 0.25;

        public Mage(Random random)
        {
            Name = "Маг";
            Type = EnemyType.Mage;
            MaxHP = 25 + random.Next(-5, 6);
            HP = MaxHP;
            Attack = 10 + random.Next(-2, 3);
            Defense = 2 + random.Next(-1, 2);
        }

        public override bool TrySpecialAbility(Player player, Random random)
        {
            if (random.NextDouble() < freezeChance)
            {
                Console.WriteLine("Маг замораживает вас! Вы пропустите следующий ход.");
                player.IsFrozen = true;
                return true;
            }
            return false;
        }

        public override int CalculateDamage(Player player, Random random)
        {
            return Math.Max(1, Attack - player.GetDefense());
        }
    }
    public class BossVvg : Goblin
    {
        public BossVvg(Random random) : base(random)
        {
            Name = "ВВГ (Босс Гоблин)";
            Type = EnemyType.BossVvg;
            MaxHP = (int)(MaxHP * 2.0);
            HP = MaxHP;
            Attack = (int)(Attack * 1.5);
            Defense = (int)(Defense * 1.2);
        }
    }

    public class BossKovalsky : Skeleton
    {
        public BossKovalsky(Random random) : base(random)
        {
            Name = "Ковальский (Босс Скелет)";
            Type = EnemyType.BossKovalsky;
            MaxHP = (int)(MaxHP * 2.5);
            HP = MaxHP;
            Attack = (int)(Attack * 1.3);
            Defense = (int)(Defense * 1.4);
        }
    }

    public class BossArchmage : Mage
    {
        public BossArchmage(Random random) : base(random)
        {
            Name = "Архимаг C++ (Босс Маг)";
            Type = EnemyType.BossArchmage;
            MaxHP = (int)(MaxHP * 1.8);
            HP = MaxHP;
            Attack = (int)(Attack * 1.6);
            Defense = (int)(Defense * 1.1);
        }
    }
}