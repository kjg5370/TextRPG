using System.Data;
using System.Threading;

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
        }//게임 시작 전 데이터 세팅

        static void DisplayGameIntro() // 게임 인트로 화면
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();            //상태보기
                    break;
                case 2:
                    DisplayInventory();         //인벤토리보기
                    break;
                case 3:
                    DisplayShop();              //상점보기
                    break;
                case 4:
                    DisplayEnterDungeon();  //던전입장
                    break;
                case 5:
                    DisplayRest();               //휴식하기
                    break;
            }
        }

        static int CheckValidInput(int min, int max) //유효한 입력 확인
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
            player.PrintMount();  // 플레이어의 공격력 방어력 출력
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
                    DisplayGameIntro(); // 인트로 화면보기
                    break;
            }
        } // 상태보기

        static void DisplayInventory() //인벤토리보기
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            foreach (Equipment equip in equipments) //현재 인벤토리에 있는 아이템 순서대로 출력
            {
                equip.PrintFormatted();                     //장비 설명 출력
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();             //인트로 화면보기
                    break;

                case 1:
                    DisplayEquipmentManaged(); //장착 관리보기
                    break;
                case 2:
                    DisplayInventorySort(); //장착 관리보기
                    break;
            }
        }

        static void DisplayInventorySort() //인벤토리 정렬
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            int index = 0;                                             // 아이템 번호 
            foreach (Equipment equipment in equipments)  //현재 인벤토리에 있는 아이템 번호 순서대로 출력
            {
                index++;
                equipment.PrintFormatted(true, index);         //장비 설명 출력 (번호 사용 유무, 번호)
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayInventory();             //인트로 화면보기
                    break;

                case 1:
                    equipments.Sort((item1, item2) => item2.eName.Length.CompareTo(item1.eName.Length));
                    DisplayInventorySort();
                    break;
            }
        }

        static void DisplayEquipmentManaged() //장착관리
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            int index = 0;                                             // 아이템 번호 
            foreach (Equipment equipment in equipments)  //현재 인벤토리에 있는 아이템 번호 순서대로 출력
            {
                index++;
                equipment.PrintFormatted(true, index);         //장비 설명 출력 (번호 사용 유무, 번호)
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, equipments.Count);
            if (input == 0)
            {
                DisplayInventory();                             //인벤토리 보기
            }
            else
            {
                player.EquipSlot(equipments[input - 1]); //선택한 번호의 장비 장착
                DisplayEquipmentManaged();               //장착관리 보기
            }
        }

        static void DisplayShop() //상점보기
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
            foreach (ShopItem shopitem in shopItems) //현재 상점에 있는 아이템 순서대로 출력
            {
                shopitem.PrintFormatted();                  //장비 설명 출력
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
                    DisplayGameIntro(); //인트로 화면 보기
                    break;
                case 1:
                    DisplayShopBuy();   //아이템 구매 화면보기
                    break;
                case 2:
                    DisplayShopSell();   //아이템 판매 화면보기
                    break;
            }
        }

        static void DisplayShopBuy() //아이템 구매 화면보기
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
            int index = 0;                                      //아이템 번호
            foreach (ShopItem shopitem in shopItems)//현재 상점에 있는 아이템 번호 순서대로 출력
            {
                index++;
                shopitem.PrintFormatted(true, index);   //장비 설명 출력 (번호 사용 유무, 번호)
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, shopItems.Length);
            if (input == 0)
            {
                DisplayShop();                      //상점보기
            }
            else
            {
                BuyItem(shopItems[input - 1]); //선택한 번호의 아이템 구매
            }
        }

        static void BuyItem(ShopItem item) //아이템 구매
        {
            if (item.Price == 0)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= item.Price)
            {
                Equipment newEquipment = new Equipment(item.eName, item.Stat, item.Data, item.Type, item.StatValue);
                player.Gold -= item.Price;
                item.Price = 0;                          // 구매 완료 표시
                equipments.Add(newEquipment);  //인벤토리에 아이템 추가
                Console.WriteLine($"{item.eName}을(를) 구매했습니다. 남은 골드: {player.Gold} G");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Console.WriteLine();
            Thread.Sleep(1000);
            DisplayShopBuy();
        }

        static void DisplayShopSell() //아이템 판매 화면보기
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
            int index = 0;                                                                      //아이템 번호
            List<ShopItem> matchingShopItems = new List<ShopItem>();
            foreach (Equipment equipment in equipments)
            {
                ShopItem matchingShopItem = shopItems.FirstOrDefault(shopItem => equipment.eName == shopItem.eName);  
                if (matchingShopItem != null)                                            // 인벤토리의 아이템과 같은 이름의 아이템 리스트 생성
                {
                    ++index;
                    matchingShopItems.Add(matchingShopItem);
                    matchingShopItems[index - 1].SellPrice();                                //판매가격 저장
                    matchingShopItems[index - 1].PrintFormatted(true, index);   //판매가능한 아이템 번호 순서대로 출력
                    matchingShopItems[index - 1].Price = 0;                          // 구매완료 표시를 위해 0으로 초기화
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, matchingShopItems.Count);
            if (input == 0)
            {
                DisplayShop();  //상점보기
            }
            else
            {
                equipments[input - 1].SellingItem(player, matchingShopItems[input - 1]);          //선택한 번호의 아이템 판매
                equipments.Remove(equipments[input - 1]);                                               //인벤토리에서 아이템 제거
                matchingShopItems[input - 1].Price = matchingShopItems[input - 1].basePrice;  //원래 아이템의 가격으로 초기화
                DisplayShopSell(); //아이템 판매 화면보기
            }
        }
        
        static void DisplayEnterDungeon() //던전입장
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
                    DisplayGameIntro();                                                                         //인트로 화면 보기
                    break;
                case 1:
                    if (player.Hp == 1) CannotEnterDungeon();
                    Dungeon easyDungeon = new Dungeon(5, Dungeon.Difficulty.Easy);//쉬운 난이도 던전
                    ClearProbability(easyDungeon);
                    break;
                case 2:
                    if (player.Hp == 1) CannotEnterDungeon();
                    Dungeon normalDungeon = new Dungeon(11, Dungeon.Difficulty.Normal); //보통 난이도 던전
                    ClearProbability(normalDungeon);
                    break;
                case 3:
                    if (player.Hp == 1) CannotEnterDungeon();
                    Dungeon hardDungeon = new Dungeon(17, Dungeon.Difficulty.Hard);        //어려움 난이도 던전
                    ClearProbability(hardDungeon);
                    break;

            }
        }

        static void DisplayDungeonClear(Dungeon dungeon) //던전 클리어 성공 함수
        {
            player.ClearCount++; 
            player.LevelUp();                                            //플레이어 레벨업
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
                    DisplayEnterDungeon();                            //던전입장
                    break;
            }
        }

        static void DisplayDungeonFail() //던전 클리어 실패 함수
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
                    DisplayEnterDungeon(); //던전입장
                    break;
            }
        }

        static void ClearProbability(Dungeon dungeon) //던전 클리어 확률 함수
        {
            if (player.Def < dungeon.RequiredDef)
            {
                int rand = new Random().Next(1, 101);   // 1 에서 100 사이의 랜덤 값
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
        
        static void CannotEnterDungeon() //던전 입장 불가
        {
                Console.WriteLine("체력이 없어서 던전 입장이 불가능 합니다.");
                Thread.Sleep(1000);
            DisplayEnterDungeon();
        }

        static void DisplayRest()   //휴식하기
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("휴식하기");
            Console.ResetColor();
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
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
                    Rest();
                    DisplayRest();
                    break;
            }
        }

        static void Rest() //골드를 뺏고 체력을 회복하는 함수
        {
            if (player.Gold >= 500)
            {
                Console.WriteLine("휴식을 완료했습니다.");
                player.Gold -= 500;
                player.Hp = 100;
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Thread.Sleep(1000);
        }
    }

    public class Character //플레이어가 될Character 클래스
    {
        private double atk;
        private int def;
        private int hp;
        private int gold;
        private int armorStat;
        private int weaponStat;

        public string Name { get; }
        public string Job { get; }
        public int Level { get; private set; }
        public int ClearCount { get; set; }

        public double Atk
        {
            get { return atk; }
            set { atk = Math.Max(value, 0); }  // 0 이하로 떨어지지 않도록 제한
        }

        public int Def
        {
            get { return def; }
            set { def = Math.Max(value, 0); }  // 0 이하로 떨어지지 않도록 제한
        }

        public int Hp
        {
            get { return hp; }
            set { hp = Math.Max(value, 1); }  // 0 이하로 떨어지지 않도록 제한
        }

        public int Gold
        {
            get { return gold; }
            set { gold = Math.Max(value, 0); }  // 0 이하로 떨어지지 않도록 제한
        }

        private Equipment equippedArmor;
        private Equipment equippedWeapon;

        

        public Character(string name, string job, int level, double atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            ClearCount = 0;
        }

        public void PrintMount()  //플레이어의 공격력, 방어력 출력
        {
            string atkDescription = Atk == Atk + weaponStat ? $"공격력 : {Atk}" : $"공격력 : {Atk} (+{weaponStat})";
            string defDescription = Def == Def + armorStat ? $"방어력 : {Def}" : $"방어력 : {Def} (+{armorStat})";

            Console.WriteLine(atkDescription);
            Console.WriteLine(defDescription);
        }

        public void EquipSlot(Equipment item) //장비 장착 개선
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
                    armorStat = equippedArmor.StatValue;
                }
            }
            else if (item.Type == Equipment.ItemType.Weapon)
            {
                if (equippedWeapon == item) UnequipWeapon();
                else
                {
                    UnequipWeapon();
                    equippedWeapon = item;
                    equippedWeapon.IsMount(true);
                    Atk += equippedWeapon.StatValue;
                    weaponStat = equippedWeapon.StatValue;
                }
            }
            
        } 

        public void UnequipArmor() //방어구 해제
        {
            if (equippedArmor != null)
            {
                equippedArmor.IsMount(false);
                Def -= equippedArmor.StatValue;
                equippedArmor = null;
                armorStat = 0;
            }
        } 

        public void UnequipWeapon() //무기 해제
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.IsMount(false);
                Atk -= equippedWeapon.StatValue;
                equippedWeapon = null;
                weaponStat = 0;
            }
        }

        public void LevelUp() //플레이어 레벨업
        {
            if(ClearCount ==Level)
            {
                Level = ClearCount+1;
                ClearCount = 0;
                Atk += 0.5;
                Def += 1;
            }
        }
    }

    public class Equipment //장비 클래스
    {
        string Mount = "";

        public string eName { get; private set; }
        public string Stat { get; private set; }
        public string Data { get; private set; }
        public ItemType Type { get; }
        public int StatValue { get; }
        
        public Equipment(string name, string stat, string data, ItemType type, int statValue)
        {
            eName = name;
            Stat = stat;
            Data = data;
            Type = type;
            StatValue = statValue;
        }

        public virtual void PrintFormatted(bool useIndex = false, int index = -1) //장비 설명 출력
        {
            string indexString = useIndex ? $"{index} " : "";
            string formattedString = $"- {indexString}{Mount}{eName,-10} |{Stat,-10} |{Data,-30}";
            Console.WriteLine(formattedString);
        }

        public void IsMount(bool useMount) //장착 확인
        {
            Mount = useMount ? "[E]" : "";
        }

        public void SellingItem(Character player, ShopItem item) //아이템 판매
        {
            if (Mount == "[E]")
            {
                if (Type == ItemType.Armor) player.UnequipArmor();
                else if (Type == ItemType.Weapon) player.UnequipWeapon();
            }
            player.Gold += (int)(item.basePrice * 0.85);
        }
        
        public enum ItemType //아이템 타입
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
        public void SellPrice() //아이템 판매 가격 설정
        {
            Price += (int)(basePrice * 0.85);
        }

        public override void PrintFormatted(bool useIndex = false, int index = -1) //장비 설명 출력 재정의
        {
            string indexString = useIndex ? $"{index} " : "";
            string formattedString;
            string priceInfo = Price == 0 ? "구매완료" : $"{Price} G";
            formattedString = $"- {indexString}{eName,-10} |{Stat,-10} |{Data,-30}|{priceInfo,-10}";
            Console.WriteLine(formattedString);
        }
    } //상점 아이템 클래스

    public class Dungeon //던전 클래스
    {
        public enum Difficulty //난이도 설정
        {
            Easy,
            Normal,
            Hard
        }

        public int RequiredDef { get; private set; }
        public int HpLoss { get; private set; }
        public int BaseHP { get; private set; }
        public int BaseGold { get; private set; }
        public Difficulty DungeonDifficulty { get; private set; }
        public string DifficultyKorean { get; private set; }

        public Dungeon(int requireDef, Difficulty difficulty)
        {
            RequiredDef = requireDef;
            DungeonDifficulty = difficulty;
        }

        public void Enter(Character player) //입장 함수
        {
            BaseHP = player.Hp;
            BaseGold = player.Gold;

            HpLoss = new Random().Next(20, 36) - (player.Def - RequiredDef); // 20 ~ 35 랜덤 값 + (내 방어력 - 권장 방어력)
            int baseReward = GetBaseReward();
            double bonusReward = new Random().Next((int)player.Atk, (int)player.Atk * 2) / 100.0;
            double totalReward = baseReward *(1+ bonusReward);

            player.Hp -= HpLoss;
            player.Gold += (int)totalReward;

        }

        private int GetBaseReward() //보상 함수
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

