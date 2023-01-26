namespace Chess.Util
{
    public class ConstUtil
    {
        public const int None = -1;
        public const int Zero = 0;
        public const string Empty = "";

        public const int Status_Piece_Freeze = 0;
        public const int Status_Piece_Idle = 1;
        public const int Status_Piece_Atk = 2;
        public const int Status_Piece_Dead = 3;
        public const int Status_Piece_No_Atk = 4;
        public const int Status_Piece_Pick = 5;

        public const int Status_Hero_Dead = 6;
        public const int Status_Hero_Pick = 7;

        public const int Status_Bartender_Pick = 8;

        public const int Team_A = 0;
        public const int Team_B = 1;

        public const int Result_Status_Unknown = 0;
        public const int Result_Status_Draw = 1;
        public const int Result_Status_Win_A = 2;
        public const int Result_Status_Win_B = 3;

        public const int Max_Num_Battle_Card = 7;
        public const int Max_Num_Hand_Card = 10;
        public const int Max_Num_Player = 2;

        public const int Init_Level = 1;
        public const int Init_Currency = 3;

        public const string Json_File_Pieces_Config = "Json/PiecesConfig.json";
        public const string Json_File_Heroes_Config = "Json/HeroesConfig.json";
        public const string Json_File_Bartender_Config = "Json/BartenderConfig.json";

        // 游戏开始: 创建玩家,创建池子,创建酒馆
        public const int Process_Game_Start = 1;
        // 选择英雄
        public const int Process_Pick_Hero = 2;
        // 战斗准备开始
        public const int Process_Prepare_Start = 3;
        // 战斗准备中: 刷新酒馆
        public const int Process_Prepare_Ing = 4;
        // 战斗准备结束
        public const int Process_Prepare_End = 5;
        // 战斗开始
        public const int Process_Battle_Start = 6;
        // 战斗中: 执行战斗部分得出结果
        public const int Process_Battle_Ing = 7;
        // 战斗结束
        public const int Process_Battle_End = 8;
        // 游戏结束
        public const int Process_Game_End = 9;
    }
}