using System.Data;

namespace textRPG
{
    internal class Program
    {
        private static Character player;
        private static List<Equipment> equipments = new List<Equipment>();
        private static ShopItem[] shopItems = new ShopItem[8];

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
            equipments.Add(new Equipment("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", Equipment.ItemType.Armor, 9));
            equipments.Add(new Equipment("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", Equipment.ItemType.Weapon, 2));
            equipments.Add(new Equipment("거북이 등껍질", "방어력 +10", "무천도사가 늘 등에 짊어지고 있는 등껍질입니다.", Equipment.ItemType.Armor, 10));
            equipments.Add(new Equipment("여의봉", "공격력 +10", "길이는 조절할 수 있지만 굵기는 일정한 봉입니다.", Equipment.ItemType.Weapon, 10));

            //상점 아이템 정보 세팅
            shopItems[0] = new ShopItem("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", Equipment.ItemType.Armor, 5, 1000);
            shopItems[1] = new ShopItem("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", Equipment.ItemType.Armor, 9, 2118);
            shopItems[2] = new ShopItem("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Equipment.ItemType.Armor, 15, 3500);
            shopItems[3] = new ShopItem("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", Equipment.ItemType.Weapon, 2, 600);
            shopItems[4] = new ShopItem("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", Equipment.ItemType.Weapon, 5, 1500);
            shopItems[5] = new ShopItem("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", Equipment.ItemType.Weapon, 7, 3500);
            shopItems[6] = new ShopItem("거북이 등껍질", "방어력 +10", "무천도사가 늘 등에 짊어지고 있는 등껍질입니다.", Equipment.ItemType.Armor, 10, 2000);
            shopItems[7] = new ShopItem("여의봉", "공격력 +10", "길이는 조절할 수 있지만 굵기는 일정한 봉입니다.", Equipment.ItemType.Weapon, 10, 2000);

            shopItems[1].Price = 0;
            shopItems[3].Price = 0;
            shopItems[6].Price = 0;
            shopItems[7].Price = 0;
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
            Console.WriteLine("4. 던전입장");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
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
                case 4:
                    DisplayEnterDungeon();
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
                equipment.PrintFormatted(true, index);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, equipments.Count);
            if (input == 0)
            {
                DisplayInventory();
            }
            else
            {
                player.EquipSlot(equipments[input - 1]);
                EquipmentManaged();
            }
        }
        static void DisplayShop()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점");
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
                shopitem.PrintFormatted();
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DisplayShopBuy();
                    break;
                case 2:
                    DisplayShopSell();
                    break;
            }
        }
        static void DisplayShopBuy()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점 - 아이템 구매");
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
            int input = CheckValidInput(0, shopItems.Length);
            if (input == 0)
            {
                DisplayShop();
            }
            else
            {
                BuyItem(shopItems[input - 1]);
            }
        }
        static void BuyItem(ShopItem item)
        {
            if (item.Price == 0)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= item.Price)
            {
                Equipment newEquipment = new Equipment(item.eName, item.Stat, item.Data, item.Type, item.StatValue);
                player.Gold -= item.Price;
                item.Price = 0; // 구매 완료 표시
                equipments.Add(newEquipment);
                Console.WriteLine($"{item.eName}을(를) 구매했습니다. 남은 골드: {player.Gold} G");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }

            Console.WriteLine();
            DisplayShopBuy();
        }
        static void DisplayShopSell()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine($"[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            int index = 0;
            List<ShopItem> matchingShopItems = new List<ShopItem>();

            foreach (Equipment equipment in equipments)
            {
                ShopItem matchingShopItem = shopItems.FirstOrDefault(shopItem => equipment.eName == shopItem.eName);
                if (matchingShopItem != null)
                {
                    ++index;
                    matchingShopItems.Add(matchingShopItem);
                    matchingShopItems[index - 1].Sell();
                    matchingShopItems[index - 1].PrintFormatted(true, index);
                    matchingShopItems[index - 1].Price = 0;

                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, matchingShopItems.Count);
            if (input == 0)
            {
                DisplayShop();
            }
            else
            {
                equipments[input - 1].SellingItem(player, matchingShopItems[input - 1]);
                equipments.Remove(equipments[input - 1]);
                matchingShopItems[input - 1].Price = matchingShopItems[input - 1].basePrice;
                DisplayShopSell();
            }
        }
        
        static void DisplayEnterDungeon()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전 입장");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 3);
           
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    Dungeon easyDungeon = new Dungeon(5, Dungeon.Difficulty.Easy);
                    ClearProbability(easyDungeon);
                    break;
                case 2:
                    Dungeon normalDungeon = new Dungeon(11, Dungeon.Difficulty.Normal);
                    ClearProbability(normalDungeon);
                    break;
                case 3:
                    Dungeon hardDungeon = new Dungeon(17, Dungeon.Difficulty.Hard);
                    ClearProbability(hardDungeon);
                    break;

            }
        }
        static void DisplayDungeonClear(Dungeon dungeon)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전 클리어!");
            Console.ResetColor();
            Console.WriteLine($"축하합니다!!\r\n{dungeon.DifficultyKorean} 던전을 클리어 하였습니다.");
            Console.WriteLine();
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {dungeon.BaseHP} -> {player.Hp}");
            Console.WriteLine($"Gold {dungeon.BaseGold} -> {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayEnterDungeon();
                    break;
            }
        }
        static void DisplayDungeonFail()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전 실패!");
            Console.ResetColor();
            Console.WriteLine("체력이 절반으로 깎였습니다.");
            player.Hp /= 2;
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayEnterDungeon();
                    break;
            }
        }
        static void ClearProbability(Dungeon dungeon)
        {
            if (player.Def < dungeon.RequiredDef)
            {
                int rand = new Random().Next(1, 101);// 1 에서 100 사이의 랜덤 값
                if (rand <= 40)
                {
                    DisplayDungeonFail();
                }
                else
                {
                    dungeon.Enter(player);
                    DisplayDungeonClear(dungeon);
                }
            }
            else
            {
                dungeon.Enter(player);
                DisplayDungeonClear(dungeon);
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
        public int Hp { get; set; }
        public int Gold { get; set; }

        private Equipment equippedArmor;
        private Equipment equippedWeapon;

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

        public void EquipSlot(Equipment item)
        {
            if (item.Type == Equipment.ItemType.Armor)
            {
                if (equippedArmor == item) UnequipArmor();
                else
                {
                    UnequipArmor();
                    equippedArmor = item;
                    equippedArmor.IsMount(true);
                    Def += equippedArmor.StatValue;
                }
            }
            else if (item.Type == Equipment.ItemType.Weapon)
            {
                if (equippedArmor == item) UnequipArmor();
                else
                {
                    UnequipWeapon();
                    equippedWeapon = item;
                    equippedWeapon.IsMount(true);
                    Atk += equippedWeapon.StatValue;
                }
            }
        }
        public void UnequipArmor()
        {
            if (equippedArmor != null)
            {
                equippedArmor.IsMount(false);
                Def -= equippedArmor.StatValue;
                equippedArmor = null;
            }
        }

        public void UnequipWeapon()
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.IsMount(false);
                Atk -= equippedWeapon.StatValue;
                equippedWeapon = null;
            }
        }

    }

    public class Equipment
    {
        public string eName { get; private set; }
        public string Stat { get; private set; }
        public string Data { get; private set; }
        public ItemType Type { get; }
        public int StatValue { get; }
        string Mount = "";
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
            string formattedString = $"- {indexString}{Mount}{eName,-10} |{Stat,-10} |{Data,-30}";
            Console.WriteLine(formattedString);
        }
        public void IsMount(bool useMount)
        {
            Mount = useMount ? "[E]" : "";
        }

        public void SellingItem(Character player, ShopItem item)
        {
            if (Mount == "[E]")
            {
                if (Type == ItemType.Armor) player.UnequipArmor();
                else if (Type == ItemType.Weapon) player.UnequipWeapon();
            }
            player.Gold += (int)(item.basePrice * 0.85);
        }
        public enum ItemType
        {
            Armor,
            Weapon
        }
    }

    public class ShopItem : Equipment
    {
        public int Price { get; set; }
        public int basePrice;
        public ShopItem(string name, string stat, string description, ItemType type, int statValue, int price)
         : base(name, stat, description, type, statValue)
        {
            Price = price;
            basePrice = Price;
        }
        public void Sell()
        {
            Price += (int)(basePrice * 0.85);
        }

        public override void PrintFormatted(bool useIndex = false, int index = -1)
        {
            string indexString = useIndex ? $"{index} " : "";
            string formattedString;
            string priceInfo = Price == 0 ? "구매완료" : $"{Price} G";
            formattedString = $"- {indexString}{eName,-10} |{Stat,-10} |{Data,-30}|{priceInfo,-10}";
            Console.WriteLine(formattedString);
        }
    }
    public class Dungeon
    {
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        public int RequiredDef { get; private set; }
        public int HpLoss { get; private set; }
        public Difficulty DungeonDifficulty { get; private set; }

        public string DifficultyKorean { get; private set; }
        public int BaseHP { get; private set; }
        public int BaseGold { get; private set; }
        public Dungeon(int requireDef, Difficulty difficulty)
        {
            RequiredDef = requireDef;
            DungeonDifficulty = difficulty;
        }

        public void Enter(Character player)
        {
            BaseHP = player.Hp;
            BaseGold = player.Gold;

            HpLoss = new Random().Next(20, 36) + Math.Max(player.Def - RequiredDef, 0); // 20 ~ 35 랜덤 값 + (내 방어력 - 권장 방어력)
            int baseReward = GetBaseReward();
            double bonusReward = new Random().Next(player.Atk, player.Atk * 2) / 100.0;
            double totalReward = baseReward *(1+ bonusReward);

            player.Hp -= HpLoss;
            player.Gold += (int)totalReward;

        }


        private int GetBaseReward()
        {
            switch (DungeonDifficulty)
            {
                case Difficulty.Easy:
                    DifficultyKorean = "쉬운";
                    return 1000;
                case Difficulty.Normal:
                    DifficultyKorean = "일반";
                    return 1700;
                case Difficulty.Hard:
                    DifficultyKorean = "어려운";
                    return 2500;
                default:
                    return 0;
            }
        }
    }
}

