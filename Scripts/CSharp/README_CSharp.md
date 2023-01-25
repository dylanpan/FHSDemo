# FHSDemo

目前本地调试流程:
1. 安装 .Net7.0
2. 备份当前 Program.cs 内容(或者通过 Git 还原)
3. 终端运行命令进行环境创建:
    - dotnet new console --framework net7.0 --force
    - dotnet add package Newtonsoft.Json --version 13.0.2
4. 终端运行命令进行调试:
    - dotnet run