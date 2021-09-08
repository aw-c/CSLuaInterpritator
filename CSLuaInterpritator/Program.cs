using System;
using static System.Console;
using NLua;

namespace CSLuaInterpritator
{
    public static class Program
    {
        public static Lua Lua;
        public static void LoadScript()
        {
            try
            {
                Lua.DoFile("script.lua");
                WriteLine("\nScript was sucessfuly loaded");
            }
            catch (Exception e)
            {
                WriteLine("Script wasn't started: \n    " + e);
            }
        }
        public static void ReloadLua()
        {
            var NewLua = new Lua();
            NewLua.State.Encoding = System.Text.Encoding.UTF8;
            NewLua.RegisterFunction("loadscript", null, typeof(Program).GetMethod("LoadScript", new Type[] { }));
            NewLua.RegisterFunction("reloadlua", null, typeof(Program).GetMethod("ReloadLua", new Type[] { }));
            WriteLine("Lua was sucessfuly "+ (Lua == null ? "loaded" : "reloaded")+"\n");
            Lua = NewLua;
        }
        public static void StartInterpritator()
        {
            ReloadLua();
            LoadScript();
            while (true)
                try
                {
                    Lua.DoString(ReadLine());
                }
                catch (Exception e)
                {
                    WriteLine(e);
                }
        }
        static void Main(string[] args)
        {
            Title = "CSLuaInterpritator";
            OutputEncoding = System.Text.Encoding.UTF8;
            WriteLine("CSharp Lua Interpritator\nDeveloped by Alan Wake\nWrite any code or edit script.lua for interpritate.");
            StartInterpritator();
        }
    }
}
