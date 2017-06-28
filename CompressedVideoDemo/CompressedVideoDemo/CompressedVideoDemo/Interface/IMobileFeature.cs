using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompressedVideoDemo.Interface
{
    public interface IMobileFeature
    {
        Task<string> RecordVideo();
        Task<string> SelectVideo();
        Task<bool> PlayVideo(string path);
       Task<bool> CompressVideo(string inputPath,string outputPath);
    }
}
