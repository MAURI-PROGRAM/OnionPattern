﻿using OnionPattern.DataAccess.EF;
using OnionPattern.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace OnionPattern.DependencyInjection.Data
{
    public static class DependencyDataInjector
    {
        public static async void InjectAsync(VideoGameContext context)
        {
            await InitializePlatforms(context);
            await InitalizeGames(context);
            await InitailizeGamePlatforms(context);

            await context.SaveChangesAsync();
        }

        public static void Inject(VideoGameContext context)
        {
            Task.WaitAll(InitializePlatforms(context), InitalizeGames(context), InitailizeGamePlatforms(context));
            context.SaveChanges();
        }

        private static async Task InitializePlatforms(IVideoGameContext context)
        {
            var platforms = new[]
            {
                new Platform { Name = "Nintendo", ReleaseDate = new DateTime(1983, 7, 15) },
                new Platform { Name = "Super Nintendo", ReleaseDate = new DateTime(1990, 11, 21) },
                new Platform { Name = "Nintendo 64", ReleaseDate = new DateTime(1996, 6, 23) },
                new Platform { Name = "Nintendo Game Cube", ReleaseDate = new DateTime(2001, 11, 18) },
                new Platform { Name = "Nintendo Wii", ReleaseDate = new DateTime(2006, 11, 19) },
                new Platform { Name = "Nintendo Wii U", ReleaseDate = new DateTime(2012, 11, 18) },
                new Platform { Name = "Nintendo Switch", ReleaseDate = new DateTime(2017, 3, 3) }
            };
            await context.Platforms.AddRangeAsync(platforms);
        }

        private static async Task InitalizeGames(IVideoGameContext context)
        {
            var games = new[]
            {
                new Game { Name = "The Legend of Zelda", Genre = @"Adventure\RPG", Price = 24.99, ReleaseDate = new DateTime(1987, 8, 22) },
                new Game { Name = "Zelda II: The Adventure of Link", Genre = @"Adventure\RPG", Price = 30.16, ReleaseDate = new DateTime(1988, 9, 26) },
                new Game { Name = "The Legend of Zelda: A Link To The Past", Genre = @"Adventure\RPG", Price = 40.13, ReleaseDate = new DateTime(1991, 11, 21) },
                new Game { Name = "The Legend of Zelda: Ocarina of Time", Genre = @"Adventure\RPG", Price = 69.69, ReleaseDate = new DateTime(1998, 11, 21) },
                new Game { Name = "The Legend of Zelda: Majora's Mask", Genre = @"Adventure\RPG", Price = 120.56, ReleaseDate = new DateTime(2000, 4, 27) },
                new Game { Name = "The Legend of Zelda: The Wind Waker", Genre = @"Adventure\RPG", Price = 13.13, ReleaseDate = new DateTime(2002, 12, 13) },
                new Game { Name = "The Legend of Zelda: Twilight Princess", Genre = @"Adventure\RPG", Price = 10000.12, ReleaseDate = new DateTime(2006, 12, 02) },
                new Game { Name = "The Legend of Zelda: Skyward Sword", Genre = @"Adventure\RPG", Price = 465.56, ReleaseDate = new DateTime(2011, 11, 18) },
                new Game { Name = "The Legend of Zelda: Breath of the Wild", Genre = @"Adventure\RPG", Price = 69.99, ReleaseDate = new DateTime(2017, 3, 3) }
            };
            await context.Games.AddRangeAsync(games);
        }

        private static async Task InitailizeGamePlatforms(IVideoGameContext context)
        {
            var gamePlatforms = new[]
            {
                new GamePlatform { GameId = 1, PlatformId = 1 },
                new GamePlatform { GameId = 2, PlatformId = 1 },
                new GamePlatform { GameId = 3, PlatformId = 2 },
                new GamePlatform { GameId = 4, PlatformId = 3 },
                new GamePlatform { GameId = 5, PlatformId = 3 },
                new GamePlatform { GameId = 6, PlatformId = 4 },
                new GamePlatform { GameId = 7, PlatformId = 4 },
                new GamePlatform { GameId = 7, PlatformId = 5 },
                new GamePlatform { GameId = 8, PlatformId = 5 },
                new GamePlatform { GameId = 9, PlatformId = 6 },
                new GamePlatform { GameId = 9, PlatformId = 7 },
            };
            await context.GamePlatforms.AddRangeAsync(gamePlatforms);
        }
    }
}
