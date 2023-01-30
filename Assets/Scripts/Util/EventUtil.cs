using System;
using System.Collections.Generic;
using UnityEngine;
// EventUtil.Instance.SendEvent(ConstUtil.EventType_TEST_UPDATE);
// EventUtil.Instance.Register(ConstUtil.EventType_TEST_UPDATE, UpdateHandler);
// EventUtil.Instance.Unregister(ConstUtil.EventType_TEST_UPDATE, UpdateHandler);
namespace Chess.Util
{
    public class EventUtil
    {
        public delegate void EventNoneHandler();
        public delegate void EventIntHandler(int param);
        public delegate void EventFloatHandler(float param);
        public delegate void EventObjectHandler(object param);

        private static EventUtil instance;
        
        public static EventUtil Instance
        {
            get { return instance ?? (instance = new EventUtil()); }
        }

        private enum ParamType
        {
            NONE = 1,
            INT = 2,
            FLOAT = 4,
            OBJECT = 8,
        }

        public class EventCallback
        {
            public EventNoneHandler handlerNone;
            public EventIntHandler handlerInt;
            public EventFloatHandler handlerFloat;
            public EventObjectHandler handlerObject;

            public void Clean()
            {
                handlerNone = null;
                handlerInt = null;
                handlerFloat = null;
                handlerObject = null;
            }
        }

        private readonly Dictionary<int, EventCallback> allEventHandler = new Dictionary<int, EventCallback>();

        private EventUtil()
        {
        }

        public static void Destroy()
        {
            instance = null;
        }        
        public void SendEvent(int eventType)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                if (callback.handlerNone != null) callback.handlerNone();
            }
        }
        public void SendEvent(int eventType, int param)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                if (callback.handlerInt != null) callback.handlerInt(param);
                if (callback.handlerFloat != null) callback.handlerFloat(param);
            }
        }
        public void SendEvent(int eventType, float param)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                if (callback.handlerFloat != null) callback.handlerFloat(param);
                if (callback.handlerInt != null) callback.handlerInt((int)param);
            }
        }
        public void SendEvent(int eventType, object param)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                if (callback.handlerObject != null) callback.handlerObject(param);
            }
        }

        public void Register(int eventType, EventNoneHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback == null)
            {
                callback = new EventCallback();
                allEventHandler.Add(eventType, callback);
            }
            if (callback.handlerNone != null)
            {
                callback.handlerNone += handler;
            }
            else
                callback.handlerNone = handler;
        }

        public void Register(int eventType, EventIntHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback == null)
            {
                callback = new EventCallback();
                allEventHandler.Add(eventType, callback);
            }
            if (callback.handlerInt != null)
            {
                callback.handlerInt += handler;
            }
            else
                callback.handlerInt = handler;
        }

        public void Register(int eventType, EventFloatHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback == null)
            {
                callback = new EventCallback();
                allEventHandler.Add(eventType, callback);
            }
            if (callback.handlerFloat != null)
            {
                callback.handlerFloat += handler;
            }
            else
                callback.handlerFloat = handler;
        }
        
        public void Register(int eventType, EventObjectHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback == null)
            {
                callback = new EventCallback();
                allEventHandler.Add(eventType, callback);
            }
            if (callback.handlerObject != null)
            {
                callback.handlerObject += handler;
            }
            else
                callback.handlerObject = handler;
        }

        public void Unregister(int eventType, EventNoneHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                callback.handlerNone -= handler;
                if (callback.handlerNone == null)
                {
                    callback.Clean();
                    allEventHandler.Remove(eventType);
                }
            }
        }
        public void Unregister(int eventType, EventIntHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                callback.handlerInt -= handler;
                if (callback.handlerInt == null && callback.handlerFloat == null)
                {
                    callback.Clean();
                    allEventHandler.Remove(eventType);
                }
            }
        }
        public void Unregister(int eventType, EventFloatHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                callback.handlerFloat -= handler;
                if (callback.handlerFloat == null && callback.handlerInt == null)
                {
                    callback.Clean();
                    allEventHandler.Remove(eventType);
                }
            }
        }
        public void Unregister(int eventType, EventObjectHandler handler)
        {
            EventCallback callback;
            allEventHandler.TryGetValue(eventType, out callback);
            if (callback != null)
            {
                callback.handlerObject -= handler;
                if (callback.handlerObject == null)
                {
                    callback.Clean();
                    allEventHandler.Remove(eventType);
                }
            }
        }
    }
}
