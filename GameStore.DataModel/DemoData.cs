using System.Collections.Generic;

namespace GameStore.DataModel
{
    public static class DemoData
    {
        public static IEnumerable<ProductEntity> Products =>
            new[]
            {
                new ProductEntity
                {
                    Name = "Grand Theft Auto V - PlayStation 3",
                    Description = "Comes in original case with manual. Game is in excellent condition",
                    Published = true,
                    Price = 19.9M
                },
                new ProductEntity
                {
                    Name = "Assassin's Creed Valhalla - PlayStation 4",
                    Description = "Become a legendary Viking warrior raised on tales of battle and glory. " +
                                  "Raid your enemies, grow your settlement, and build your political power in the quest " +
                                  "to earn a place among the gods in Valhalla.",
                    Published = false,
                    Price = 59.99M
                },
                new ProductEntity
                {
                    Name = "The Witcher 3: Wild Hunt – Complete Edition",
                    Description = "The most awarded game of 2015!\n" +
                                  "Become a monster slayer for hire and embark on an epic journey to\n" +
                                  "track down the child of prophecy, a living weapon capable of untold destruction.\n" +
                                  "The Complete Edition includes The Witcher 3: Wild Hunt, all 16 DLCs, and 2 Expansion Packs: " +
                                  "Hearts of Stone & Blood and Wine.",
                    Published = true,
                    Price = 9.99M
                },
                new ProductEntity
                {
                    Name = "Cyberpunk 2077 - PlayStation 5",
                    Description =
                        "Cyberpunk 2077 is an open-world action-adventure from the creators of The Witcher 3: " +
                        "Wild Hunt, CD Projekt Red.",
                    Published = false,
                    Price = 59.99M
                }
            };
    }
}