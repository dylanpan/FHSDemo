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
        
        public const string Json_File_Pieces_Config = "Assets/Scripts/Json/PiecesConfig.json";
        public const string Json_File_Heroes_Config = "Assets/Scripts/Json/HeroesConfig.json";
        public const string Json_File_Bartender_Config = "Assets/Scripts/Json/BartenderConfig.json";

        // 游戏开始: 创建玩家,创建池子,创建酒馆
        public const int Process_Game_Start = 100;
        public const int Process_Game_Start_Main_View = 101;
        public const int Process_Game_Start_Player = 102;
        public const int Process_Game_Start_Bartender = 103;
        public const int Process_Game_Start_Heroes_Pool = 104;
        public const int Process_Game_Start_Pieces_Pool = 105;
        public const int Process_Game_Start_Hand_Card = 106;
        public const int Process_Game_Start_Battle_Card = 107;
        // 选择英雄
        public const int Process_Pick_Hero = 200;
        public const int Process_Pick_Hero_Ing = 201;
        // 战斗准备开始
        public const int Process_Prepare_Start = 300;
        // 战斗准备中: 刷新酒馆
        public const int Process_Prepare_Ing = 400;
        // 战斗准备结束
        public const int Process_Prepare_End = 500;
        // 战斗开始
        public const int Process_Battle_Start = 600;
        // 战斗中: 执行战斗部分得出结果
        public const int Process_Battle_Ing = 700;
        // 战斗结束
        public const int Process_Battle_End = 800;
        // 游戏结束
        public const int Process_Game_End = 900;

        // 通知
        public const int Event_Type_close_hero_pick_view = 10000;
        public const int Event_Type_update_bartender_pieces_view = 10001;
    }
}