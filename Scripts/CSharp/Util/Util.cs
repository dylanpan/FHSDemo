using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Util
{
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
                StatusComponent statusComponent = (StatusComponent)copyEntity.GetComponent<StatusComponent>();
                if (statusComponent != null && isSetStatus)
                {
                    statusComponent.status = -1;
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
        StatusComponent statusComponent = (StatusComponent)copyEntity.GetComponent<StatusComponent>();
        if (statusComponent != null && isSetStatus)
        {
            statusComponent.status = -1;
        }
        return copyEntity;
    }
    public static List<Entity> CopyList(List<Entity> originList)
    {
        List<Entity> copyList = new List<Entity>();
        originList.ForEach(i => copyList.Add(Util.CopyEntity(i, false)));
        return copyList;
    }
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
            if (statusComponent != null && statusComponent.status == 2)
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
                    if (porpertyComponent.hp > 0 && statusComponent.status != 4)
                    {
                        atkEntity = entity;
                        statusComponent.status = 2;
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
                if (porpertyComponent.hp > 0 && statusComponent.status != 3)
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
                Util.Battle_SetEntityStatus(entity, 3);
            }
            else if (porpertyComponent.atk > 0 && porpertyComponent.hp > 0)
            {
                Util.Battle_SetEntityStatus(entity, isFinish ? 1 : -1);
            }
            else if (porpertyComponent.atk <= 0)
            {
                Util.Battle_SetEntityStatus(entity, 4);
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
    public static void Battle_LoggerListPiecesEntity(List<Entity> list)
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
}