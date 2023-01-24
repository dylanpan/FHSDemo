using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Util
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
        foreach (Entity entity in World.Instance.entityDic.Values)
        {
            if (id == entity.ID)
            {
                foreach (IComponent component in entity.components)
                {
                    copyEntity.AddComponent((IComponent)Util.Clone(component));
                }
                if (isSetStatus)
                {
                    StatusComponent statusComponent = (StatusComponent)copyEntity.GetComponent<StatusComponent>();
                    if (statusComponent != null)
                    {
                        statusComponent.status = ConstUtil.None;
                    }
                }
                break;
            }
        }
        return copyEntity;
    }
    public static Entity CopyEntity(Entity originEntity, bool isSetStatus = true)
    {
        Entity copyEntity = new Entity();
        foreach (IComponent component in originEntity.components)
        {
            copyEntity.AddComponent((IComponent)Util.Clone(component));
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
        originList.ForEach(i => copyList.Add(Util.CopyEntity(i, false)));
        return copyList;
    }

    public static Dictionary<string, string> GetCommonParamDict()
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict["name"] = "";
        paramDict["id"] = "0";
        paramDict["skin_name"] = "";
        paramDict["current_level"] = "0";
        paramDict["pieces_cost"] = "0";
        paramDict["pieces_recycle"] = "0";
        paramDict["race"] = "0";
        paramDict["atk"] = "0";
        paramDict["hp"] = "0";
        return paramDict;
    }
    #endregion

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
        bool isPiece = false;
        if (nameComponent != null 
            && skinComponent != null 
            && levelComponent != null 
            && currencyComponent != null 
            && porpertyComponent != null 
            && buffComponent != null 
            && statusComponent != null)
        {
            isPiece = true;
        }
        return isPiece;
    }
    #endregion

    #region Battle
    public static int Battle_RandomPiecesIndex(int total)
    {
        return new Random().Next(total);
    }

    public static Entity Battle_FindAtkEntity(List<Entity> list, out int findAtkIndex)
    {
        findAtkIndex = -1;
        Entity? atkEntity = null;
        for (int i = 0; i < list.Count; i++)
        {
            Entity entity = list[i];
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            if (statusComponent != null && statusComponent.status == ConstUtil.Status_PieceAtk)
            {
                atkEntity = entity;
                findAtkIndex = i;
                break;
            }
        }
        // 不更新列表长度，只更新列表状态
        if (findAtkIndex == -1)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Entity entity = list[i];
                PorpertyComponent porpertyComponent = (PorpertyComponent)entity.GetComponent<PorpertyComponent>();
                StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
                if (porpertyComponent != null && statusComponent != null)
                {
                    if (porpertyComponent.hp > 0 && statusComponent.status != ConstUtil.Status_PieceNoAtk)
                    {
                        atkEntity = entity;
                        statusComponent.status = ConstUtil.Status_PieceAtk;
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
                if (porpertyComponent.hp > 0 && statusComponent.status != ConstUtil.Status_PieceDead)
                {
                    tmpList.Add(entity);
                }
            }
        }
        if (tmpList.Count > 0)
        {
            defEntity = tmpList[Util.Battle_RandomPiecesIndex(tmpList.Count)];
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
                Util.Battle_SetEntityStatus(entity, ConstUtil.Status_PieceDead);
            }
            else if (porpertyComponent.atk > 0 && porpertyComponent.hp > 0)
            {
                Util.Battle_SetEntityStatus(entity, isFinish ? ConstUtil.Status_PieceIdle : ConstUtil.None);
            }
            else if (porpertyComponent.atk <= 0)
            {
                Util.Battle_SetEntityStatus(entity, ConstUtil.Status_PieceNoAtk);
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
        int status = -1;
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
            Console.WriteLine("Empty list");
        }
    }
    #endregion
}