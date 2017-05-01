
using System.Threading.Tasks;

namespace ImageCropperDemo.Interface
{
    public interface Imedia
    {
        Task<string> TakePhotoAsync(int width, int height);
        Task<string> ChoosePicture(int width, int height);
    }
}