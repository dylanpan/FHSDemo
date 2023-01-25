using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using Chess.Util;

namespace Chess.System
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
            List<HeroesConfig> configDataList = ConfigUtil.GetConfigData<HeroesConfig>(ConstUtil.JsonFile_HeroesConfig);
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
                Console.WriteLine("HeroPoolSystem get empty config");
            }
        }
        public override void Update()
        {
            Console.WriteLine("HeroPoolSystem Update");
            GeneratePoolFormConfig();
            TestUtil.SetHero();
        }
    }
}