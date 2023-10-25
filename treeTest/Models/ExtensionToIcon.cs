using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treeTest.Models
{
    public static class ExtensionToIcon
    {
        // Dictionary of extensions and paths to icons.
        public static Dictionary<string, Uri> Icons = new Dictionary<string, Uri>()
        {
            {".dock", new Uri("avares://treeTest/icons/dock.png")},
            {".txt", new Uri("avares://treeTest/icons/txt.png")},
        };

        // Getting an icon by path.
        public static Uri GetIcon(string extension)
        {
            if (Icons.TryGetValue(extension, out var icon))
                return icon;
            else
                return new Uri("avares://treeTest/icons/unknown-file.png");
        }
    }
}
