using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LiteDBViewer
{
    internal sealed class Konstants
    {
        public static string IniFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}.ini");
    }
}
