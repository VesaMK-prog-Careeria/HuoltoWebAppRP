using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages
{
    public class ImageService
    {
        private readonly HuoltoContext _context;

        public ImageService(HuoltoContext context)
        {
            _context = context;
        }

        public async Task SaveImagesAsync(List<IFormFile> images, string kuvaType, int entityId)
        {
            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await image.CopyToAsync(ms);
                    if (ms.Length > 0)
                    {
                        var kuva = new Kuva()
                        {
                            KuvaNimi = image.FileName,
                            KuvaData = ms.ToArray(),
                            KuvaType = kuvaType,
                            EntityId = entityId
                        };
                        _context.Kuvat.Add(kuva);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
