using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;
using UnityEngine;

namespace Chess.Systems
{
    public class PiecesPoolSystem : ISystem
    {
        public void GeneratePieceEntity(PiecesConfig piecesConfig)
        {
            if (piecesConfig != null)
            {
                Entity entity = new Entity();
                entity.AddComponent(new NameComponent(){name = piecesConfig.name, id = piecesConfig.id});
                entity.AddComponent(new SkinComponent(){skin_name = piecesConfig.skin_name});
                entity.AddComponent(new LevelComponent(){current_level = piecesConfig.current_level});
                entity.AddComponent(new CurrencyComponent(){piece_cost = piecesConfig.piece_cost, piece_recycle = piecesConfig.piece_recycle});
                entity.AddComponent(new PorpertyComponent(){atk = piecesConfig.atk, hp = piecesConfig.hp, race = piecesConfig.race});
                entity.AddComponent(new BuffComponent());
                entity.AddComponent(new StatusComponent());
                World.Instance.AddEntity(entity);
            }
        }

        public void GeneratePoolFormConfig()
        {
            List<PiecesConfig> configDataList = ConfigUtil.GetConfigData<PiecesConfig>(ConstUtil.Json_File_Pieces_Config);
            if (configDataList.Count > 0)
            {
                for (int i = 0; i < configDataList.Count; i++)
                {
                    PiecesConfig piecesConfig = configDataList[i];
                    GeneratePieceEntity(piecesConfig);
                }
            }
            else
            {
                Debug.Log("PiecesPoolSystem get empty config");
            }
        }

        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Heroes_Pool)
            {
                Debug.Log("PiecesPoolSystem Update - init");
                GeneratePoolFormConfig();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Pieces_Pool);
            }
            else if (Process.Instance.GetProcess() == ConstUtil.Process_Prepare_Start)
            {
                Debug.Log("PiecesPoolSystem Update - battle prepare");
            }
        }
    }
}