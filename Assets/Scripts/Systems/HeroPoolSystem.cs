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
                World.Instance.AddEntity(entity);
            }
        }
        public void GeneratePoolFormConfig()
        {
            List<HeroesConfig> configDataList = ConfigUtil.GetConfigData<HeroesConfig>(ConstUtil.Json_File_Heroes_Config);
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
        public override void Update()
        {
            if (Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Bartender)
            {
                Debug.Log("HeroPoolSystem Update - init");
                GeneratePoolFormConfig();
                TestUtil.SetHero();
                Process.Instance.SetProcess(ConstUtil.Process_Game_Start_Heroes_Pool);
            }
            else if(Process.Instance.GetProcess() == ConstUtil.Process_Game_Start_Battle_Card)
            {
                Debug.Log("HeroPoolSystem Update - pick Hero");
                Process.Instance.SetProcess(ConstUtil.Process_Pick_Hero);
                // TODO: 弹出[选择英雄]界面,选择后设置新的状态 - Process_Prepare_Start
            }
        }
    }
}