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
    public class BossPestov : Skeleton
    {
        private double freezeChance = 0.4; // 25% + 15%

        public BossPestov(Random random) : base(random)
        {
            Name = "Пестов С-- (Босс Скелет)";
            Type = EnemyType.BossPestov;
            MaxHP = (int)(MaxHP * 1.3);
            HP = MaxHP;
            Attack = (int)(Attack * 1.8);
            Defense = (int)(Defense * 0.6);
        }

        public override bool TrySpecialAbility(Player player, Random random)
        {
            if (random.NextDouble() < freezeChance)
            {
                Console.WriteLine("Пестов С-- замораживает вас! Вы пропустите следующий ход.");
                player.IsFrozen = true;
                return true;
            }
            return false;
        }
    }
    public class Game
    {
        private Player player;
        private Random random;
        private int turnCount;
        private List<Item> possibleItems;

        public Game()
        {
            player = new Player();
            random = new Random();
            turnCount = 0;
            InitializeItems();
        }

        private void InitializeItems()
        {
            possibleItems = new List<Item>
            {
                // Оружие
                new Item("Стальной меч", 10, 0, true),
                new Item("Огненный посох", 15, 0, true),
                new Item("Лук охотника", 12, 0, true),
                new Item("Секира варвара", 18, 0, true),
                
                // Доспехи
                new Item("Кожаная броня", 0, 8, false),
                new Item("Стальные доспехи", 0, 15, false),
                new Item("Магический плащ", 0, 12, false),
                new Item("Доспехи дракона", 0, 20, false)
            };
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в текстовый рогалик!");
            Console.WriteLine("Цель: выживать как можно дольше, побеждая врагов и улучшая экипировку.");
            Console.WriteLine();

            while (player.HP > 0)
            {
                turnCount++;
                Console.WriteLine($"=== Ход {turnCount} ===");
                Console.WriteLine(player);
                Console.WriteLine();

                // Каждые 10 ходов - босс
                if (turnCount % 10 == 0)
                {
                    Console.WriteLine("!!! Появляется БОСС !!!");
                    Enemy boss = GenerateBoss();
                    Combat(boss);
                }
                else
                {
                    // Обычный ход: 50% шанс сундука, 50% шанс врага
                    if (random.Next(2) == 0)
                    {
                        FindChest();
                    }
                    else
                    {
                        Enemy enemy = GenerateEnemy();
                        Combat(enemy);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Игра окончена! Вы погибли.");
            Console.WriteLine($"Вы продержались {turnCount} ходов.");
        }

        private Enemy GenerateEnemy()
        {
            int enemyType = random.Next(3);
            return enemyType switch
            {
                0 => new Goblin(random),
                1 => new Skeleton(random),
                2 => new Mage(random),
                _ => new Goblin(random)
            };
        }

        private Enemy GenerateBoss()
        {
            int bossType = random.Next(4);
            return bossType switch
            {
                0 => new BossVvg(random),
                1 => new BossKovalsky(random),
                2 => new BossArchmage(random),
                3 => new BossPestov(random),
                _ => new BossVvg(random)
            };
        }
        private void FindChest()
        {
            Console.WriteLine("Вы нашли сундук!");
            int itemType = random.Next(3);

            switch (itemType)
            {
                case 0: 
                    Console.WriteLine("В сундуке лечебное зелье! Вы полностью исцелены.");
                    player.Heal();
                    break;

                case 1:
                case 2: 
                    Item foundItem = possibleItems[random.Next(possibleItems.Count)];
                    Console.WriteLine($"В сундуке: {foundItem}");

                    Item currentItem = foundItem.IsWeapon ? player.Weapon : player.Armor;
                    Console.WriteLine($"Ваш текущий предмет: {currentItem}");

                    Console.WriteLine("Вы хотите взять новый предмет? (y/n)");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "y" || choice == "д")
                    {
                        if (foundItem.IsWeapon)
                        {
                            player.Weapon = foundItem;
                            Console.WriteLine("Вы экипировали новое оружие.");
                        }
                        else
                        {
                            player.Armor = foundItem;
                            Console.WriteLine("Вы экипировали новые доспехи.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы оставили предмет в сундуке.");
                    }
                    break;
            }
        }
        private void Combat(Enemy enemy)
        {
            Console.WriteLine($"Вы встретили: {enemy}");
            Console.WriteLine();

            bool playerDefending = false;

            while (enemy.IsAlive && player.HP > 0)
            {
                if (player.IsFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    player.IsFrozen = false;
                }
                else
                {
                    Console.WriteLine("Ваш ход:");
                    Console.WriteLine("1 - Атаковать");
                    Console.WriteLine("2 - Защищаться");
                    Console.Write("Выберите действие: ");

                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        int damage = Math.Max(1, player.GetAttack() - enemy.Defense);
                        enemy.HP -= damage;
                        Console.WriteLine($"Вы нанесли {damage} урона {enemy.Name}.");
                        playerDefending = false;
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("Вы готовитесь к защите.");
                        playerDefending = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод, вы пропускаете ход.");
                        playerDefending = false;
                    }
                }

                if (!enemy.IsAlive)
                {
                    Console.WriteLine($"Вы победили {enemy.Name}!");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine($"Ход {enemy.Name}:");

                if (playerDefending && random.NextDouble() < 0.4)
                {
                    Console.WriteLine("Вы успешно уклонились от атаки!");
                }
                else
                { 
                    int damage = enemy.CalculateDamage(player, random);

                    if (playerDefending)
                    {
                        double blockPercent = 0.7 + random.NextDouble() * 0.3; 
                        int blockedDamage = (int)(damage * (1 - blockPercent));
                        damage = Math.Max(0, damage - blockedDamage);
                        Console.WriteLine($"Вы блокируете {blockedDamage} урона.");
                    }

                    player.HP -= damage;
                    Console.WriteLine($"{enemy.Name} наносит вам {damage} урона.");

                    enemy.TrySpecialAbility(player, random);
                }

                Console.WriteLine();
                Console.WriteLine($"Ваше HP: {player.HP}/{player.MaxHP}");
                Console.WriteLine($"HP {enemy.Name}: {enemy.HP}/{enemy.MaxHP}");
                Console.WriteLine();

                if (player.HP <= 0)
                {
                    Console.WriteLine("Вы погибли в бою!");
                    break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения боя...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }
    }
}