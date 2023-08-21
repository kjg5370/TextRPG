﻿using System.Data;

namespace textRPG
{
    internal class Program
    {
        private static Character player;
        private static Equipment[] equipments = new Equipment[4];
        private static ShopItem[] shopItems = new ShopItem[6];

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            //인벤토리 아이템 정보 세팅
            equipments[0] = new Equipment("무쇠갑옷", "방어력 +5", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, 5);
            equipments[1] = new Equipment("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, 2);
            equipments[2] = new Equipment("거북이 등껍질", "방어력 +10", "무천도사가 늘 등에 짊어지고 있는 등껍질입니다.", ItemType.Armor, 10);
            equipments[3] = new Equipment("여의봉", "공격력 +10", "길이는 조절할 수 있지만 굵기는 일정한 봉입니다.", ItemType.Weapon, 10);

            //상점 아이템 정보 세팅
            shopItems[0] = new ShopItem("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor, 5, 1000);
            shopItems[1] = new ShopItem("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, 9, 0);
            shopItems[2] = new ShopItem("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor,15, 3500);
            shopItems[3] = new ShopItem("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, 2, 0);
            shopItems[4] = new ShopItem("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon, 5, 1500);
            shopItems[5] = new ShopItem("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon, 7, 3500);

        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            player.PrintMount();
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            foreach (Equipment equip in equipments)
            {
                equip.PrintFormatted();
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    EquipmentManaged();
                    break;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void EquipmentManaged()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            int index = 0;
            foreach (Equipment equipment in equipments)
            {
                index++;
                equipment.PrintFormatted(true,index);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, equipments.Length);
            if (input == 0)
            {
                DisplayInventory();
            }
            else
            {
                equipments[input - 1].MountEquipment(player);
                EquipmentManaged();
            }
        }
        static void DisplayShop()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점- 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine($"[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            int index = 0;
            foreach (ShopItem shopitem in shopItems)
            {
                index++;
                shopitem.PrintFormatted(true, index);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, equipments.Length);
            if (input == 0)
            {
                DisplayGameIntro();
            }
        }
    }
    public enum ItemType
    {
        Armor,
        Weapon
    }

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
        public void PrintMount()
        {
            string atkDescription = Atk == 10 ? $"공격력 : {Atk}" : $"공격력 : {Atk} (+{Atk - 10})";
            string defDescription = Def == 5 ? $"방어력 : {Def}" : $"방어력 : {Def} (+{Def - 5})";

            Console.WriteLine(atkDescription);
            Console.WriteLine(defDescription);

        }
    }

    public class Equipment
    {
        public string eName { get; private set; }
        public string Stat { get; private set; }
        public string Data { get; private set; }
        public ItemType Type { get; }
        public int StatValue { get; }
        string Mount = "[E]";
        public Equipment(string name, string stat, string data, ItemType type, int statValue)
        {
            eName = name;
            Stat = stat;
            Data = data;
            Type = type;
            StatValue = statValue;
        }
        public virtual void PrintFormatted(bool useIndex = false, int index = -1)
        {
            string indexString = useIndex ? $"{index} " : "";
            string formattedString = $"- {indexString}{eName,-10} |{Stat,-10} |{Data,-30}";
            Console.WriteLine(formattedString);
        }
        public void MountEquipment(Character player)
        {
            if (eName.IndexOf(Mount) == -1)
            {
                eName = Mount + eName;
                if (Type == ItemType.Weapon)
                {
                    player.Atk += StatValue;
                }
                else if (Type == ItemType.Armor)
                {
                    player.Def += StatValue;
                }
            }
            else
            {
                eName = eName.Replace(Mount, "");
                if (Type == ItemType.Weapon)
                {
                    player.Atk -= StatValue;
                }
                else if (Type == ItemType.Armor)
                {
                    player.Def -= StatValue;
                }
            }
        }
    }
    public class ShopItem : Equipment
    {
        public int Price { get; }

        public ShopItem(string name, string stat, string description, ItemType type, int statValue, int price)
         : base(name, stat, description, type, statValue)
        {
            Price = price;
        }
        public override void  PrintFormatted(bool useIndex = false, int index = -1)
        {
            string indexString = useIndex ? $"{index} " : "";
            string formattedString;
            if (Price != 0)formattedString = $"- {indexString}{eName,-10} |{Stat,-10} |{Data,-30}|{Price}G";
            else formattedString = $"- {indexString}{eName,-10} |{Stat,-10} |{Data,-30}|구매완료";
            Console.WriteLine(formattedString);
        }
    }
}
