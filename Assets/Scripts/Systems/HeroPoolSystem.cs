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
    public class HeroPoolSystem : ISystem
    {
        public void GenerateHeroEntity(HeroesConfig heroesConfig)
        {
            if (heroesConfig != null)
            {
                Entity entity = new Entity();
                entity.AddComponent(new NameComponent(){name = heroesConfig.name, id = heroesConfig.id});
                entity.AddComponent(new SkinComponent(){skin_name = heroesConfig.skin_name});
                entity.AddComponent(new SkillComponent(){skill_id = heroesConfig.skill_id});
                entity.AddComponent(new PorpertyComponent(){atk = heroesConfig.atk, hp = heroesConfig.hp});
                entity.AddComponent(new StatusComponent());
                entity.AddComponent(new ConfigComponent<HeroesConfig>(){config = heroesConfig});
                World.Instance.AddEntity(entity);
                Process.GetInstance().AddHeroToPool(entity.ID);
            }
        }
        public void GeneratePoolFormConfig()
        {
            List<HeroesConfig> configDataList = ConfigUtil.GetConfigDataList<HeroesConfig>(ConstUtil.Json_File_Heroes_Config);
            if (configDataList.Count > 0)
            {
                for (int i = 0; i < configDataList.Count; i++)
                {
                    HeroesConfig heroesConfig = configDataList[i];
                    GenerateHeroEntity(heroesConfig);
                }
            }
            else
            {
                Debug.Log("HeroPoolSystem get empty config");
            }
        }
        public void GenerateHeroListFormPool()
        {
            foreach (Entity entity in World.Instance.entityDic.Values)
            {
                if (CommonUtil.CheckIsPlayer(entity))
                {
                    // TODO: VIP 扩展 4 个
                    // TODO: 切换不同 player 视角(编写 AI 执行脚本可观测操作流程)
                    List<int> hero_list = GetRamdomHeroFormPool(2);
                    Process.GetInstance().AddHeroListToDict(entity.ID, hero_list);
                }
            }
        }
        public List<int> GetRamdomHeroFormPool(int max)
        {
            List<int> hero_pool = new List<int>();
            for (int i = 0; i < max; i++)
            {
                hero_pool.Add(RandomHero());
            }
            return hero_pool;
        }
        private int RandomHero()
        {
            List<int> hero_pool = Process.GetInstance().GetHeroPool();
            int id = ConstUtil.None;
            if (hero_pool.Count > 0)
            {
                id = hero_pool[CommonUtil.RandomHeroesIndex(hero_pool.Count)];
                Process.GetInstance().RemoveHeroFormPool(id);
            }
            return id;
        }
        public override void Update()
        {
            List<int> player_list = Process.GetInstance().GetPlayerIdList();
            for (int i = 0; i < player_list.Count; i++)
            {
                int player_id = player_list[i];
                if (Process.GetInstance().CheckProcessIsEqual(Process.GetInstance().GetShowPlayerId(), ConstUtil.Process_Game_Start_Bartender_Pool))
                {
                    Debug.Log("HeroPoolSystem Update - init");
                    GeneratePoolFormConfig();
                    Process.GetInstance().SetProcess(ConstUtil.Process_Game_Start_Heroes_Pool, Process.GetInstance().GetShowPlayerId());
                }
                else if(Process.GetInstance().CheckProcessIsEqual(player_id, ConstUtil.Process_Game_Start_Player))
                {
                    Debug.Log("HeroPoolSystem Update - pick Hero");
                    GenerateHeroListFormPool();
                    Process.GetInstance().SetProcess(ConstUtil.Process_Pick_Hero, player_id);
                }
            }
        }
    }
}