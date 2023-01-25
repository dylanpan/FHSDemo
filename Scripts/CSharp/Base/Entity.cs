using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Chess.Util;

namespace Chess.Base
{
    public class Entity
    {
        public int ID = ConstUtil.Zero;
        public List<IComponent> components = new List<IComponent>();

        public Entity()
        {
            ID = GenerateId();
        }
        
        private int GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int id = BitConverter.ToInt32(buffer, 1);
            return id;
        }
        
        public void AddComponent(IComponent component)
        {
            if (!components.Contains(component))
            {
                components.Add(component);
            }
        }
        public bool RemoveComponent<T>()
        {
            bool isRemove = false;
            int index = HaveComponent<T>();
            if (index >= 0)
            {
                components.RemoveAt(index);
                isRemove = true;
            }
            return isRemove;
        }
        public int HaveComponent<T>()
        {
            int isHave = -1;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                {
                    isHave = i;
                    break;
                }
            }
            return isHave;
        }
        public IComponent GetComponent<T>()
        {
            int index = HaveComponent<T>();
            if (index >= 0)
            {
                return components[index];
            }
            return null;
        }
    }
}