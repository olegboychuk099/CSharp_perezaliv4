using System;
using System.IO;
using Csharp_laba4.Model;

namespace Csharp_laba4.Tools
{
    internal static class PersonControler
    {
        internal static readonly string WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4");
        internal static Person GetPerson { get; set; }
    }
}
