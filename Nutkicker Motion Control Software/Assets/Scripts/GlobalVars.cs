using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GlobalVars
{
    public static string RawDataString = string.Empty;

    public static NumberFormatInfo myNumberFormat()
    {
        var format = new NumberFormatInfo();
        format.NegativeSign = "-";
        format.NumberDecimalSeparator = ".";
        format.NumberDecimalDigits = 4;

        return format;
    }
}
