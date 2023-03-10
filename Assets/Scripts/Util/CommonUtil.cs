using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chess.Base;
using Chess.Config;
using Chess.Component;
using UnityEngine;

namespace Chess.Util
{
    public class CommonUtil
    {
        #region Common
        public static object Clone(object obj)
        {
            Type T = obj.GetType();
            object O = Activator.CreateInstance(T);
            PropertyInfo[] PI = T.GetProperties();
            for (int i = 0; i < PI.Length; i++)
            {
                PropertyInfo P = PI[i];
                P.SetValue(O, P.GetValue(obj));
            }
            return O;
        }

        public static Entity CopyEntity(int id, bool isSetStatus = true)
        {
            Entity copyEntity = new Entity();
            if (World.Instance.entityDic.ContainsKey(id))
            {
                Entity entity = World.Instance.entityDic[id];
                foreach (IComponent component in entity.components)
                {
                    copyEntity.AddComponent((IComponent)CommonUtil.Clone(component));
                }
                if (isSetStatus)
                {
                    StatusComponent statusComponent = (StatusComponent)copyEntity.GetComponent<StatusComponent>();
                    if (statusComponent != null)
                    {
                        statusComponent.status = ConstUtil.None;
                    }
                }
            }
            return copyEntity;
        }
        public static Entity CopyEntity(Entity originEntity, bool isSetStatus = true)
        {
            Entity copyEntity = new Entity();
            foreach (IComponent component in originEntity.components)
            {
                copyEntity.AddComponent((IComponent)CommonUtil.Clone(component));
            }
            if (isSetStatus)
            {
                StatusComponent statusComponent = (StatusComponent)copyEntity.GetComponent<StatusComponent>();
                if (statusComponent != null)
                {
                    statusComponent.status = ConstUtil.None;
                }
            }
            return copyEntity;
        }
        public static List<Entity> CopyList(List<Entity> originList)
        {
            List<Entity> copyList = new List<Entity>();
            originList.ForEach(i => copyList.Add(CommonUtil.CopyEntity(i, false)));
            return copyList;
        }
        #endregion

        public static void ResetPiecesStatus(List<int> piecesIdList)
        {
            foreach (int pieceId in piecesIdList)
            {
                Entity piece = World.Instance.entityDic[pieceId];
                CommonUtil.Battle_SetEntityStatus(piece, ConstUtil.None);
            }
        }

        #region Check
        public static bool CheckIsPiece(Entity entity)
        {
            NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
            SkinComponent skinComponent = (SkinComponent)entity.GetComponent<SkinComponent>();
            LevelComponent levelComponent = (LevelComponent)entity.GetComponent<LevelComponent>();
            CurrencyComponent currencyComponent = (CurrencyComponent)entity.GetComponent<CurrencyComponent>();
            PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
            BuffComponent buffComponent = (BuffComponent)entity.GetComponent<BuffComponent>();
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            ConfigComponent<PiecesConfig> configComponent = (ConfigComponent<PiecesConfig>)entity.GetComponent<ConfigComponent<PiecesConfig>>();
            bool isPiece = false;
            if (nameComponent != null 
                && skinComponent != null 
                && levelComponent != null 
                && currencyComponent != null 
                && porpertyComponent != null 
                && buffComponent != null 
                && statusComponent != null
                && configComponent != null)
            {
                isPiece = true;
            }
            return isPiece;
        }
        public static bool CheckIsHero(Entity entity)
        {
            NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
            SkinComponent skinComponent = (SkinComponent)entity.GetComponent<SkinComponent>();
            SkillComponent skillComponent = (SkillComponent)entity.GetComponent<SkillComponent>();
            PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            ConfigComponent<HeroesConfig> configComponent = (ConfigComponent<HeroesConfig>)entity.GetComponent<ConfigComponent<HeroesConfig>>();
            bool isHero = false;
            if (nameComponent != null 
                && skinComponent != null 
                && skillComponent != null 
                && porpertyComponent != null
                && statusComponent != null
                && configComponent != null)
            {
                isHero = true;
            }
            return isHero;
        }
        public static bool CheckIsPlayer(Entity entity)
        {
            NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
            PlayerComponent playerComponent = (PlayerComponent)entity.GetComponent<PlayerComponent>();
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            ConfigComponent<AIConfig> configComponent = (ConfigComponent<AIConfig>)entity.GetComponent<ConfigComponent<AIConfig>>();
            bool isPlayer = false;
            if (nameComponent != null 
                && playerComponent != null
                && statusComponent != null
                && configComponent != null)
            {
                isPlayer = true;
            }
            return isPlayer;
        }
        public static bool CheckIsBartender(Entity entity)
        {
            NameComponent nameComponent = (NameComponent)entity.GetComponent<NameComponent>();
            SkinComponent skinComponent = (SkinComponent)entity.GetComponent<SkinComponent>();
            LevelComponent levelComponent = (LevelComponent)entity.GetComponent<LevelComponent>();
            CurrencyComponent currencyComponent = (CurrencyComponent)entity.GetComponent<CurrencyComponent>();
            PiecesListComponent piecesListComponent = (PiecesListComponent)entity.GetComponent<PiecesListComponent>();
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            ConfigComponent<BartenderConfig> configComponent = (ConfigComponent<BartenderConfig>)entity.GetComponent<ConfigComponent<BartenderConfig>>();
            bool isBartender = false;
            if (nameComponent != null 
                && skinComponent != null 
                && levelComponent != null 
                && currencyComponent != null 
                && piecesListComponent != null
                && statusComponent != null
                && configComponent != null)
            {
                isBartender = true;
            }
            return isBartender;
        }
        #endregion

        #region Prepare
        public static int RandomHeroesIndex(int total)
        {
            return new System.Random().Next(total);
        }
        public static int RandomPiecesIndex(int total)
        {
            return new System.Random().Next(total);
        }
        public static int RandomPlayerIndex(int total)
        {
            return new System.Random().Next(total);
        }
        public static int RandomFirstIndex(int total)
        {
            return new System.Random().Next(total);
        }
        public static void SetPieceBelong(int id, int belong = ConstUtil.None)
        {
            Entity piece = World.Instance.entityDic[id];
            if (piece != null)
            {
                NameComponent nameComponent = (NameComponent)piece.GetComponent<NameComponent>();
                if (nameComponent != null)
                {
                    nameComponent.belong = belong;
                }
            }
        }
        public static void GetPieceBelong(int id)
        {
            int belong = ConstUtil.None;
            Entity piece = World.Instance.entityDic[id];
            if (piece != null)
            {
                NameComponent nameComponent = (NameComponent)piece.GetComponent<NameComponent>();
                if (nameComponent != null)
                {
                    belong = nameComponent.belong;
                }
            }
            return belong;
        }
        #endregion

        #region Battle
        public static int Battle_RandomPiecesIndex(int total)
        {
            return new System.Random().Next(total);
        }

        public static Entity Battle_FindAtkEntity(List<Entity> list, out int findAtkIndex)
        {
            findAtkIndex = -1;
            Entity? atkEntity = null;
            for (int i = 0; i < list.Count; i++)
            {
                Entity entity = list[i];
                StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                if (statusComponent != null && statusComponent.status == ConstUtil.Status_Piece_Atk)
                {
                    atkEntity = entity;
                    findAtkIndex = i;
                    break;
                }
            }
            // ?????????????????????????????????????????????
            if (findAtkIndex == -1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Entity entity = list[i];
                    PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
                    StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                    if (porpertyComponent != null && statusComponent != null)
                    {
                        if (porpertyComponent.hp > 0 && statusComponent.status != ConstUtil.Status_Piece_No_Atk)
                        {
                            atkEntity = entity;
                            statusComponent.status = ConstUtil.Status_Piece_Atk;
                            findAtkIndex = i;
                            break;
                        }
                    }
                }
            }
            return atkEntity;
        }
        public static Entity Battle_FindDefEntity(List<Entity> list)
        {
            Entity? defEntity = null;
            List<Entity> tmpList = new List<Entity>();
            for (int i = 0; i < list.Count; i++)
            {
                Entity entity = list[i];
                PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
                StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                if (porpertyComponent != null && statusComponent != null)
                {
                    if (porpertyComponent.hp > 0 && statusComponent.status != ConstUtil.Status_Piece_Dead)
                    {
                        tmpList.Add(entity);
                    }
                }
            }
            if (tmpList.Count > 0)
            {
                defEntity = tmpList[CommonUtil.Battle_RandomPiecesIndex(tmpList.Count)];
            }
            return defEntity;
        }
        public static Entity Battle_GetEmptyEntity()
        {
            Entity entity = new Entity();
            entity.AddComponent(new NameComponent(){name = "BattleEmpty_" + entity.ID});
            entity.AddComponent(new PiecesListComponent());
            return entity;
        }
        public static bool Battle_CheckListHaveStatus(int checkStatus, List<Entity> checkList)
        {
            bool isHave = false;
            for (int i = 0; i < checkList.Count; i++)
            {
                Entity entity = checkList[i];
                StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                if (statusComponent != null && statusComponent.status == checkStatus)
                {
                    isHave = true;
                    break;
                }
            }
            return isHave;
        }
        public static bool Battle_CheckListAllSpecificStatus(int[] checkStatusList, List<Entity> checkList)
        {
            bool isAll = true;
            int count = 0;
            for (int i = 0; i < checkList.Count; i++)
            {
                Entity entity = checkList[i];
                StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                if (statusComponent != null)
                {
                    for (int j = 0; j < checkStatusList.Length; j++)
                    {
                        if (statusComponent.status == checkStatusList[j])
                        {
                            count = count + 1;
                            break;
                        }
                    }
                }
            }
            if (count != checkList.Count)
            {
                isAll = false;
            }
            return isAll;
        }

        public static void Battle_UpdateEntityStatus(Entity entity, bool isFinish = false)
        {
            PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
            if (porpertyComponent != null)
            {
                if (porpertyComponent.hp <= 0)
                {
                    CommonUtil.Battle_SetEntityStatus(entity, ConstUtil.Status_Piece_Dead);
                }
                else if (porpertyComponent.atk > 0 && porpertyComponent.hp > 0)
                {
                    CommonUtil.Battle_SetEntityStatus(entity, isFinish ? ConstUtil.Status_Piece_Idle : ConstUtil.None);
                }
                else if (porpertyComponent.atk <= 0)
                {
                    CommonUtil.Battle_SetEntityStatus(entity, ConstUtil.Status_Piece_No_Atk);
                }
            }
        }

        public static void Battle_SetEntityStatus(Entity entity, int status)
        {
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            if (statusComponent != null)
            {
                statusComponent.status = status;
            }
        }

        public static int Battle_GetEntityStatus(Entity entity)
        {
            int status = ConstUtil.None;
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            if (statusComponent != null)
            {
                status = statusComponent.status;
            }
            return status;
        }

        public static void Battle_LoggerListPiecesEntity(List<Entity> list)
        {
            if (list.Count > 0)
            {
                foreach (Entity _entity in list)
                {
                    NameComponent nameComponent = (NameComponent)_entity.GetComponent<NameComponent>();
                    PorpertyComponent porpertyComponent = (PorpertyComponent)_entity.GetComponent<PorpertyComponent>();
                    StatusComponent statusComponent = (StatusComponent)_entity.GetComponent<StatusComponent>();
                    if (porpertyComponent != null && nameComponent != null && statusComponent != null)
                    {
                        nameComponent.LoggerString();
                        porpertyComponent.LoggerString();
                        statusComponent.LoggerString();
                    }
                }
            }
            else
            {
                Debug.Log("Empty list");
            }
        }
        #endregion
    }
}