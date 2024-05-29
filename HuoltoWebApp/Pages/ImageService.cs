using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages
{
    public class ImageService //VK
    {
        private readonly HuoltoContext _context;

        public ImageService(HuoltoContext context)
        {
            _context = context;
        }

        // Metodi hakee kuvan kuvaid:n perusteella
        public async Task SaveImagesAsync(List<IFormFile> images, string kuvaType, int entityId) //Tämä metodi tallentaa kuvat tietokantaan
        {
            foreach (var image in images)                                   // Käydään läpi kaikki kuvat
            {
                if (image.Length > 0)                                       
                {
                    using var ms = new MemoryStream();                      // Luodaan MemoryStream-olio
                    await image.CopyToAsync(ms);                            // Kopioidaan kuvadata MemoryStream-olioon
                    if (ms.Length > 0)
                    {
                        // Luodaan Kuva-olio ja asetetaan sille tiedot
                        var kuva = new Kuva()
                        {
                            KuvaNimi = image.FileName,
                            KuvaData = ms.ToArray(),
                            KuvaType = kuvaType,
                            EntityId = entityId
                        };
                        // Aseta oikea viittaus kuvatyyppiin perustuen. Tallenetaa kuvat tietokantaan viitteillä
                        if (kuvaType == "AutoInfo")
                        {
                            kuva.AutoInfoId = entityId;
                        }
                        else if (kuvaType == "SäiliöInfo")
                        {
                            kuva.SäiliöInfoId = entityId;
                        }
                        else if (kuvaType == "PvInfo")
                        {
                            kuva.PvInfoId = entityId;
                        }

                        _context.Kuvat.Add(kuva);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
