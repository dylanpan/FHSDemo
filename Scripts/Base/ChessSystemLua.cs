using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using MHU3D;

public class ChessSystemLua : LuaManager
{

    protected override void Awake()
    {
        LuaManager.instance = this;

        luaState = new LuaState();

        this.OpenLibs();
        luaState.LuaSetTop(0);
        
        LuaBinder.Bind(luaState);
        DelegateFactory.Init();
        LuaCoroutine.Register(luaState, this);
    }

    protected override void StartMain()
    {
        luaState.DoFile("Assets/Chess/Scripts/Lua/chessLogic/ChessSystemMain.lua");
    }

    protected override void OpenLibs()
    {
        luaState.OpenLibs(LuaDLL.luaopen_lpeg);
        luaState.OpenLibs(LuaDLL.luaopen_bit);

        luaState.OpenLibs(LuaDLL.luaopen_wqrandom);
        this.OpenCJson();
        LuaConst.openLuaSocket = false;
        LuaConst.openLuaDebugger = true;
    }
}
