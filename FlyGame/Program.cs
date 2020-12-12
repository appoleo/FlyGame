using System;

namespace FlyGame
{
    class Program
    {
        static int[] maps = new int[100];

        static int[] playersPos = new int[2];

        static string[] playersName = new string[2];

        static bool[] flags = new bool[2];

        static void Main(string[] args)
        {
            // 游戏头
            DrawHeader();
            // 玩家姓名
            #region
            Console.WriteLine("请输入玩家A的姓名");
            playersName[0] = Console.ReadLine();
            while (playersName[0] == "")
            {
                Console.WriteLine("玩家姓名不能为空，请重新输入");
                playersName[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名");
            playersName[1] = Console.ReadLine();
            while (playersName[1] == "" || playersName[1] == playersName[0])
            {
                if (playersName[1] == "")
                {
                    Console.WriteLine("玩家姓名不能为空，请重新输入");
                }
                else
                {
                    Console.WriteLine("玩家姓名不能重复，请重新输入");
                }
                playersName[1] = Console.ReadLine();
            }
            #endregion
            // 清屏
            Console.Clear();
            DrawHeader();
            Console.WriteLine("{0}的士兵用A表示", playersName[0]);
            Console.WriteLine("{0}的士兵用B表示", playersName[1]);
            // 初始化地图
            InitMaps();
            // 绘制地图
            DrawMaps();

            
            while (playersPos[0] < 99 && playersPos[1] < 99)
            {
                if (!flags[0])
                {
                    playGame(0);
                    if (playersPos[0] >= 99)
                    {
                        Console.WriteLine("玩家{0}获胜", playersName[0]);
                        break;
                    }
                }
                else
                {
                    flags[0] = false;
                }
                if (!flags[1])
                {
                    playGame(1);
                    if (playersPos[1] >= 99)
                    {
                        Console.WriteLine("玩家{0}获胜", playersName[1]);
                        break;
                    }
                    else
                    {
                        flags[1] = false;
                    }
                }
            }
            Console.ReadKey();
        }

        public static void playGame(int playerIndex)
        {
            Random random = new Random();
            Console.WriteLine("{0}按任意键开始掷骰子", playersName[playerIndex]);
            Console.ReadKey(true);
            int step = random.Next(6) + 1;
            playersPos[playerIndex] += step;
            Console.WriteLine("{0}掷出了{1}", playersName[playerIndex], step);
            Console.ReadKey(true);
            Console.WriteLine("{0}按任意键开始行动", playersName[playerIndex]);
            Console.ReadKey(true);
            //Console.WriteLine("{0}行动完了", playersName[playerIndex]);
            //Console.ReadKey(true);
            // 踩到玩家后，被踩玩家后退6步
            if (playersPos[playerIndex] == playersPos[1 - playerIndex])
            {
                playersPos[1 - playerIndex] -= 6;
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}后退6步", playersName[playerIndex], playersName[1 - playerIndex], playersName[1 - playerIndex]);
                Console.ReadKey();
            }
            else
            {
                switch (maps[playersPos[playerIndex]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全", playersName[playerIndex]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择：1-交换位置  2-轰炸对方（后退6步）", playersName[playerIndex]);
                        string choose = Console.ReadLine();
                        while (true)
                        {
                            if (choose == "1")
                            {
                                Console.WriteLine("玩家{0}选择跟玩家{1}交换位置", playersName[playerIndex], playersName[1 - playerIndex]);
                                Console.ReadKey();
                                playersPos[playerIndex] = playersPos[playerIndex] ^ playersPos[1 - playerIndex];
                                playersPos[1 - playerIndex] = playersPos[playerIndex] ^ playersPos[1 - playerIndex];
                                playersPos[playerIndex] = playersPos[playerIndex] ^ playersPos[1 - playerIndex];
                                Console.WriteLine("交换完成，按任意键继续游戏");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (choose == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{2}后退6步", playersName[playerIndex], playersName[1 - playerIndex], playersName[1 - playerIndex]);
                                Console.ReadKey();
                                playersPos[1 - playerIndex] -= 6;
                                Console.WriteLine("玩家{0}后退了6步", playersName[1 - playerIndex]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2，1-交换位置，2-轰炸对方");
                                choose = Console.ReadLine();
                            }
                            Console.ReadKey(true);
                        }

                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，后退6步", playersName[playerIndex]);
                        Console.ReadKey(true);
                        playersPos[playerIndex] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停1回合", playersName[playerIndex]);
                        flags[playerIndex] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10步", playersName[playerIndex]);
                        Console.ReadKey(true);
                        playersPos[playerIndex] += 10;
                        break;
                    default:
                        break;
                }
            }
            ChangePos();
            Console.Clear();
            DrawMaps();
        }

        /// <summary>
        /// 绘制游戏头
        /// </summary>
        public static void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********* Fly Game **********");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitMaps()
        {
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 }; // 1
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 }; // 2
            int[] pause = { 9, 27, 60, 93 }; // 3
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 }; // 4
            for (int i = 0; i < luckyTurn.Length; i++)
            {
                maps[luckyTurn[i]] = 1;
            }
            for (int i = 0; i < landMine.Length; i++)
            {
                maps[landMine[i]] = 2;
            }
            for (int i = 0; i < pause.Length; i++)
            {
                maps[pause[i]] = 3;
            }
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                maps[timeTunnel[i]] = 4;
            }
        }

        public static void DrawMaps()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("幸运轮盘 ◎   地雷 ☆   暂停 ▲   时空隧道 卍");
            // 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawCurrentPos(i));
            }
            Console.WriteLine();
            // 第一竖行
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DrawCurrentPos(i));
                Console.WriteLine();
            }
            // 第二横行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawCurrentPos(i));
            }
            Console.WriteLine();
            // 第二竖行
            for (int i = 65; i < 70; i++)
            {
                Console.Write(DrawCurrentPos(i));
                Console.WriteLine();
            }
            // 第三横行
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DrawCurrentPos(i));
            }
            Console.WriteLine();
        }

        public static void ChangePos()
        {
            playersPos[0] = playersPos[0] < 0 ? 0 : playersPos[0] > 99 ? 99 : playersPos[0];
            playersPos[1] = playersPos[1] < 0 ? 0 : playersPos[1] > 99 ? 99 : playersPos[1];
        }

        /// <summary>
        /// 绘制当前maps索引位置的元素
        /// </summary>
        /// <param name="i">索引</param>
        public static String DrawCurrentPos(int i)
        {
            if (playersPos[0] == playersPos[1] && playersPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "<>";
            }
            else if (playersPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return "Ａ";
            }
            else if (playersPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return "Ｂ";
            }
            else
            {
                return DrawBaseMaps(maps[i]);
            }
        }

        public static String DrawBaseMaps(int i)
        {
            switch (i)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return "□";
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    return "◎";
                case 2:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    return "☆";
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    return "▲";
                case 4:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return "卍";
                default:
                    return "";
            }
        }
    }
}
