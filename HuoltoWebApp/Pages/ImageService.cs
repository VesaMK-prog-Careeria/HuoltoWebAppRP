using HuoltoWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Services
{
    public class ImageService
    {
        private readonly HuoltoContext _context;

        public ImageService(HuoltoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tallentaa lähetetyt kuvat tietokantaan tietylle kohteelle (entity)
        /// </summary>
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

                        // Asetetaan oikea viite entiteettiin
                        if (kuvaType == "AutoInfo")
                            kuva.AutoInfoId = entityId;
                        else if (kuvaType == "SäiliöInfo")
                            kuva.SäiliöInfoId = entityId;
                        else if (kuvaType == "PvInfo")
                            kuva.PvInfoId = entityId;

                        _context.Kuvat.Add(kuva);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Hakee kuvat annetun KuvaType:n ja EntityId:n perusteella
        /// </summary>
        public async Task<List<Kuva>> GetKuvatAsync(string kuvaType, int entityId)
        {
            return await _context.Kuvat
                .Where(k => k.KuvaType == kuvaType && k.EntityId == entityId)
                .ToListAsync();
        }
    }
}
