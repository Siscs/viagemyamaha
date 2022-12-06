using System.Collections.Generic;
using System.IO;

namespace ViagemYamaha.Core.Test.Build
{
    public class FileBuild
    {
        public void CreateCsvFile(string filePath)
        {
            List<string> rotas = new List<string>
            {
                "GUA,BRC,10",
                "GUA,VCF,DFE,4",
                "GUA,ZXA,ZAZ,DFE,4",
                "GUA,ZXA,ZAZ,DFE,FDS,FGG,4",
                "GUA,MAR,TST,CDG,65",
                "BRC,SCL,5",
                "GUA,CDG,75",
                "GUA,SCL,20",
                "GUA,ORL,56",
                "AAA",
                "ORL,CDG,5",
                "SCL,ORL,20",
            };

            File.WriteAllLines(filePath, rotas);
        }

    }
}
