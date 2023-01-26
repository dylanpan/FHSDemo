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
    public class HandCardSystem : ISystem
    {
        public Entity CreateHandCardEntity()
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "HandCard"});
            entity.AddComponent(new PiecesListComponent(){max_num = ConstUtil.Max_Num_Hand_Card, hand_card_id = entity.ID});
            return entity;
        }
        public void CheckAddHandCardEntity()
        {
            int[] keyList = World.Instance.entityDic.Keys.ToArray();
            for (int i = 0; i < keyList.Length; i++)
            {
                Entity entity = World.Instance.entityDic[keyList[i]];
                PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    if (playerComponent.hand_card_id == ConstUtil.None)
                    {
                        Entity handCard = CreateHandCardEntity();
                        playerComponent.hand_card_id = handCard.ID;
                        World.Instance.AddEntity(handCard);
                    }
                }
            }
        }
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Pieces_Pool)
            {
                Debug.Log("HandCardSystem Update - init");
                CheckAddHandCardEntity();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Hand_Card);
            }
        }
    }
}