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
                Process.Instance.AddHeroToPool(entity.ID);
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
                    List<int> hero_pool = GetRamdomHeroFormPool(2);
                    Process.Instance.AddHeroPoolToDict(entity.ID, hero_pool);
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
            List<int> hero_pool = Process.Instance.GetHeroPool();
            int id = ConstUtil.None;
            if (hero_pool.Count > 0)
            {
                id = hero_pool[CommonUtil.RandomHeroesIndex(hero_pool.Count)];
                Process.Instance.RemoveHeroFormPool(id);
            }
            return id;
        }
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Bartender)
            {
                Debug.Log("HeroPoolSystem Update - init");
                GeneratePoolFormConfig();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Heroes_Pool);
            }
            else if(Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Battle_Card)
            {
                Debug.Log("HeroPoolSystem Update - pick Hero");
                GenerateHeroListFormPool();
                Process.Instance.SetProcess(ConstUtil.Process_Pick_Hero);
            }
        }
    }
}