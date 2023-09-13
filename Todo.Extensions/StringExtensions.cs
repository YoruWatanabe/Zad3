using System;

namespace Todo.Extensions;

public static class StringExtensions
{
    public static bool IsValidDateTime(string value)
    {
        try
        {
            DateTime dateTime = DateTime.Parse(value);
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }
}
