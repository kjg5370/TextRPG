using System.Data;
using System.Text;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace textRPG
{
    internal class Program
    {
        private static Character player;
        private static Equipment[] equipments = new Equipment[4];

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            equipments[0] = new Equipment("무쇠갑옷", "방어력 +5", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, 5);
            equipments[1] = new Equipment("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, 2);
            equipments[2] = new Equipment("거북이 등껍질", "방어력 +10", "무천도사가 늘 등에 짊어지고 있는 등껍질입니다.", ItemType.Armor, 10);
            equipments[3] = new Equipment("여의봉", "공격력 +10", "길이는 조절할 수 있지만 굵기는 일정한 봉입니다.", ItemType.Weapon, 10);



        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
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
            PrintFormattedItems(equipments,false);
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
            PrintFormattedItems(equipments,true);
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

        static void PrintFormattedItems(Equipment[] equipments, bool useIndex = false)
        {
            int i = 1; // 시작 인덱스
            foreach (Equipment equipment in equipments)
            {
                string indexString = useIndex ? $"{i} " : ""; // 인덱스 출력 여부에 따라 결정
                string formattedString = $"- {indexString}{equipment.eName,-10} |{equipment.Stat,-10} |{equipment.Data,-30}";
                Console.WriteLine(formattedString);

                if (useIndex)
                    i++;
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
}
