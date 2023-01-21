public class Util
{
    public static Entity CopyEntity(int id, bool isSetStatus = true)
    {
        Entity copyEntity = new Entity();
        foreach (Entity entity in World.Instance.entityDic.Values)
        {
            if (id == entity.ID)
            {
                foreach (IComponent component in entity.components)
                {
                    copyEntity.AddComponent(component);
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
            copyEntity.AddComponent(component);
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
                    if (porpertyComponent.hp > 0)
                    {
                        atkEntity = entity;
                        statusComponent.status = 2;
                        findAtkIndex = i;
                        break;
                    }
                    else
                    {
                        statusComponent.status = 3;
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
    public static bool Battle_CheckListAllStatus(int checkStatus, List<Entity> checkList)
    {
        bool isAll = true;
        for (int i = 0; i < checkList.Count; i++)
        {
            Entity entity = checkList[i];
            StatusComponent statusComponent = (StatusComponent)entity.GetComponent<StatusComponent>();
            if (statusComponent != null && statusComponent.status != checkStatus)
            {
                isAll = false;
                break;
            }
        }
        return isAll;
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
            PorpertyComponent porpertyComponent = (PorpertyComponent)_entity.GetComponent<PorpertyComponent>();
            if (porpertyComponent != null)
            {
                porpertyComponent.tostring();
            }
        }
    }
}