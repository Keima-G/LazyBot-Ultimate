using System;
using System.Reflection;

[Obfuscation(Feature="renaming", ApplyToMembers=true)]
public enum LogType
{
    Warning,
    Error,
    Normal,
    Info,
    Good
}

