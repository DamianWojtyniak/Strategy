using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace Strategy
{
    public class TGame
    {
        public Bitmap Map;

        public TGame([CallerFilePath] string filePath = null)
        {
            var path = Path.GetDirectoryName(filePath) + "/bin/debug";
            Map = new Bitmap(path + "/map.bmp"); 
        }
    }
}
