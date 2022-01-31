using Imi.Project.Blazor.Core.Entities.Memory;
using Imi.Project.Blazor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imi.Project.Blazor.Core.Repositories
{
    public class MemoryCardRepository : IMemoryCardRepository
    {
        public IEnumerable<MemoryCard> GetAllCards()
        {
            return
            new List<MemoryCard>()
            {
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 1,
                    Name = "Kento Momota",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Kento_Momota_from_Japan_cropped_%281%29.jpg/1200px-Kento_Momota_from_Japan_cropped_%281%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 2,
                    Name = "Viktor Axelsen",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Viktor_Axelsen_%282010_Dutch_Open%29.jpg/1200px-Viktor_Axelsen_%282010_Dutch_Open%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 3,
                    Name = "Anders Antonsen",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Anders_Antonsen_%28cr.ploybuster%29.jpg/409px-Anders_Antonsen_%28cr.ploybuster%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 4,
                    Name = "Anthony Sinisuka Ginting",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/Anthony_Sinisuka_Ginting_-_Indonesia_Masters_2018.jpg/474px-Anthony_Sinisuka_Ginting_-_Indonesia_Masters_2018.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 5,
                    Name = "Akane Yamaguchi",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/69/Akane_Yamaguchi.jpg/360px-Akane_Yamaguchi.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 6,
                    Name = "Nozomi Okuhara",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Nozomi_Okuhara_cropped_%281%29.jpg/297px-Nozomi_Okuhara_cropped_%281%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 1,
                    Name = "Kento Momota",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Kento_Momota_from_Japan_cropped_%281%29.jpg/1200px-Kento_Momota_from_Japan_cropped_%281%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 2,
                    Name = "Viktor Axelsen",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Viktor_Axelsen_%282010_Dutch_Open%29.jpg/1200px-Viktor_Axelsen_%282010_Dutch_Open%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 3,
                    Name = "Anders Antonsen",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Anders_Antonsen_%28cr.ploybuster%29.jpg/409px-Anders_Antonsen_%28cr.ploybuster%29.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 4,
                    Name = "Anthony Sinisuka Ginting",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/Anthony_Sinisuka_Ginting_-_Indonesia_Masters_2018.jpg/474px-Anthony_Sinisuka_Ginting_-_Indonesia_Masters_2018.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 5,
                    Name = "Akane Yamaguchi",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/69/Akane_Yamaguchi.jpg/360px-Akane_Yamaguchi.jpg"
                },
                new MemoryCard
                {
                    Id = Guid.NewGuid(),
                    CardNumber = 6,
                    Name = "Nozomi Okuhara",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Nozomi_Okuhara_cropped_%281%29.jpg/297px-Nozomi_Okuhara_cropped_%281%29.jpg"
                }
            };
        }
    }
}
