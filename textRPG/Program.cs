using System.Data;

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
            equipments[0] = new Equipment("무쇠갑옷", "방어력 +5", "무쇠로 만들어져 튼튼한 갑옷입니다.",0,5);
            equipments[1] = new Equipment("낡은 검 ", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.",2,0);
            equipments[2] = new Equipment("거북이 등껍질 ", "방어력 +10", "무천도사가 늘 등에 짊어지고 있는 등껍질입니다.", 0, 10);
            equipments[3] = new Equipment("여의봉 ", "공격력 +10", "길이는 분명히 조절할 수 있지만 굵기는 일정한 봉입니다.", 10, 0);
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
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("인벤토리");
            Console.ResetColor ();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine($"[아이템 목록]");
            Console.WriteLine("- " + equipments[0].PrintEquipment());
            Console.WriteLine("- " + equipments[1].PrintEquipment());
            Console.WriteLine("- " + equipments[2].PrintEquipment());
            Console.WriteLine("- " + equipments[3].PrintEquipment());
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
            Console.WriteLine("- 1 " + equipments[0].PrintEquipment());
            Console.WriteLine("- 2 " + equipments[1].PrintEquipment());
            Console.WriteLine("- 3 " + equipments[2].PrintEquipment());
            Console.WriteLine("- 4 " + equipments[3].PrintEquipment());
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;

                case 1:
                    equipments[0].MountEquipment(player);
                    EquipmentManaged();
                    break;
                case 2:
                    equipments[1].MountEquipment(player);
                    EquipmentManaged();
                    break;
                case 3:
                    equipments[2].MountEquipment(player);
                    EquipmentManaged();
                    break;
                case 4:
                    equipments[3].MountEquipment(player);
                    EquipmentManaged();
                    break;
            }
        }

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
            if (Atk == 10 && Def == 5)
            {
                Console.WriteLine($"공격력 : {Atk}");
                Console.WriteLine($"방어력 : {Def}");
            }
            else if (Atk != 10 && Def == 5)
            {
                Console.WriteLine("공격력 : {0} (+{1})", Atk, Atk - 10);
                Console.WriteLine($"방어력 : {Def}");
            }
            else if (Atk == 10 && Def != 5)
            {
                Console.WriteLine($"공격력 : {Atk}");
                Console.WriteLine("방어력 : {0} (+{1})", Def, Def - 5);
            }
            else
            {
                Console.WriteLine("공격력 : {0} (+{1})", Atk, Atk - 10);
                Console.WriteLine("방어력 : {0} (+{1})", Def, Def - 5);
            }

        }
    }

    public class Equipment
    {
        public string eName { get; private set; }
        public string Stat { get; }
        public string Data { get; }
        public int eAtk { get; }
        public int eDef { get; }
        string Mount = "[E]";
        public Equipment(string name, string stat, string data, int atk, int def)
        {
            eName = name;
            Stat = stat;
            Data = data;
            eAtk = atk;
            eDef = def;
        }
        public string PrintEquipment()
        {
           return eName + "|" + Stat +"|"+ Data;
        }
        public void MountEquipment(Character player)
        {
            if (eName.IndexOf(Mount) == -1)
            {
                eName = Mount + eName;
                if(eAtk != 0)
                {
                    player.Atk += eAtk;
                }
                else if (eDef != 0)
                {
                    player.Def += eDef;
                }
            }
            else
            {
               eName = eName.Replace(Mount, "");
               player.Atk -= eAtk;
               player.Def -= eDef;
            }
        }
    }
}