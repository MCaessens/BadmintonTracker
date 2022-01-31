using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Imi.Project.Api.Infrastructure.Data
{
    public class BadmintonDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Racket> Rackets { get; set; }
        public DbSet<ShuttleCock> ShuttleCocks { get; set; }

        public BadmintonDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<Location>()
                .HasMany(l => l.RelatedGames)
                .WithOne(g => g.Location)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.User)
                .WithMany(g => g.Locations)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ShuttleCock>()
                .HasMany(l => l.RelatedGames)
                .WithOne(g => g.ShuttleCock)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ShuttleCock>()
                .HasOne(l => l.User)
                .WithMany(g => g.ShuttleCocks)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Racket>()
                .HasMany(l => l.RelatedGames)
                .WithOne(r => r.Racket)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Racket>()
                .HasOne(l => l.User)
                .WithMany(r => r.Rackets)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.User)
                .WithMany(u => u.Games)
                .OnDelete(DeleteBehavior.SetNull);
            User[] users = new User[]
                    {
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            FirstName = "Marco",
                            LastName = "Caessens",
                            UserName = "WolloW",
                            NormalizedUserName = "WOLLOW",
                            Email = "caessens.marco@gmail.com",
                            NormalizedEmail = "CAESSENS.MARCO@GMAIL.COM",
                            SecurityStamp = "s2mkkh6qbU",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            FirstName = "Cedric",
                            LastName = "Theys",
                            UserName = "Eatle",
                            NormalizedUserName = "EATLE",
                            Email = "cedric.theys@gmail.com",
                            NormalizedEmail = "CEDRIC.THEYS@GMAIL.COM",
                            SecurityStamp = "n2E7T6ssNJ",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            FirstName = "Hennesley",
                            LastName = "Moerman",
                            UserName = "JustALad",
                            NormalizedUserName = "JUSTALAD",
                            Email = "hennesley.moerman@gmail.com",
                            NormalizedEmail = "HENNESLEY.MOERMAN@GMAIL.COM",
                            SecurityStamp = "G3rvJEt2r8",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            FirstName = "Felien",
                            LastName = "Braeckevelt",
                            UserName = "Noorie",
                            NormalizedUserName = "NOORIE",
                            Email = "felien.braeckevelt@gmail.com",
                            NormalizedEmail = "FELIEN.BRAECKEVELT@GMAIL.COM",
                            SecurityStamp = "XasGBe9U74",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            FirstName = "Louis",
                            LastName = "Caessens",
                            UserName = "Luigi6509",
                            NormalizedUserName = "LUIGI6509",
                            Email = "louis.caessens@gmail.com",
                            NormalizedEmail = "LOUIS.CAESSENS@GMAIL.COM",
                            SecurityStamp = "6fIqpRuE7E",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            FirstName = "Wesley",
                            LastName = "Caessens",
                            UserName = "CobbleWobbles",
                            NormalizedUserName = "COBBLEWOBBLES",
                            Email = "wesley.caessens@gmail.com",
                            NormalizedEmail = "WESLEY.CAESSENS@GMAIL.COM",
                            SecurityStamp = "L62fp75ope",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            FirstName = "Pamela",
                            LastName = "Valcke",
                            UserName = "Pamke",
                            NormalizedUserName = "PAMKE",
                            Email = "pamela.valcke@gmail.com",
                            NormalizedEmail = "PAMELA.VALCKE@GMAIL.COM",
                            SecurityStamp = "6d7QUQ6wue",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            FirstName = "Spyro",
                            LastName = "Caessens",
                            UserName = "Spyroenkie",
                            NormalizedUserName = "SPYROENKIE",
                            Email = "spyro.caessens@gmail.com",
                            NormalizedEmail = "SPYRO.CAESSENS@GMAIL.COM",
                            SecurityStamp = "YnNTPhEWik",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            FirstName = "Valerie",
                            LastName = "Seline",
                            UserName = "VaSeline",
                            NormalizedUserName = "VASELINE",
                            Email = "helena.bafort@gmail.com",
                            NormalizedEmail = "HELENA.BAFORT@GMAIL.COM",
                            SecurityStamp = "lMLdnKH1il",
                            AccountIntegrityId = Guid.NewGuid()
                        },
                        new User{
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            FirstName = "Jill",
                            LastName = "Valentijn",
                            Email = "jill.valentijn@gmail.com",
                            NormalizedEmail = "JILL.VALENTIJN@GMAIL.COM",
                            UserName = "JillVal",
                            NormalizedUserName = "JILLVAL",
                            SecurityStamp = "3xSn0ECaag",
                            AccountIntegrityId = Guid.NewGuid()
                        }
                    };
            users[0].PasswordHash = hasher.HashPassword(users[0], "TEST");
            users[1].PasswordHash = hasher.HashPassword(users[1], "TEST");
            users[2].PasswordHash = hasher.HashPassword(users[2], "TEST");
            users[3].PasswordHash = hasher.HashPassword(users[3], "TEST");
            users[4].PasswordHash = hasher.HashPassword(users[4], "TEST");
            users[5].PasswordHash = hasher.HashPassword(users[5], "TEST");
            users[6].PasswordHash = hasher.HashPassword(users[6], "TEST");
            users[7].PasswordHash = hasher.HashPassword(users[7], "TEST");
            users[8].PasswordHash = hasher.HashPassword(users[8], "TEST");
            users[9].PasswordHash = hasher.HashPassword(users[9], "TEST");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().HasData(
                new[]
                {
                    // Email Claims
                    new IdentityUserClaim<Guid>
                    {
                        Id = 1,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[0].Email,
                        UserId = users[0].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 2,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[1].Email,
                        UserId = users[1].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 3,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[2].Email,
                        UserId = users[2].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 4,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[3].Email,
                        UserId = users[3].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 5,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[4].Email,
                        UserId = users[4].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 6,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[5].Email,
                        UserId = users[5].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 7,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[6].Email,
                        UserId = users[6].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 8,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[7].Email,
                        UserId = users[7].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 9,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[8].Email,
                        UserId = users[8].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 10,
                        ClaimType = ClaimTypes.Email,
                        ClaimValue = users[9].Email,
                        UserId = users[9].Id
                    },
                    // NameIdentifier
                    new IdentityUserClaim<Guid>
                    {
                        Id = 11,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[0].Id.ToString(),
                        UserId = users[0].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 12,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[1].Id.ToString(),
                        UserId = users[1].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 13,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[2].Id.ToString(),
                        UserId = users[2].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 14,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[3].Id.ToString(),
                        UserId = users[3].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 15,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[4].Id.ToString(),
                        UserId = users[4].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 16,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[5].Id.ToString(),
                        UserId = users[5].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 17,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[6].Id.ToString(),
                        UserId = users[6].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 18,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[7].Id.ToString(),
                        UserId = users[7].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 19,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[8].Id.ToString(),
                        UserId = users[8].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 20,
                        ClaimType = CustomClaimTypes.NameIdentifier,
                        ClaimValue = users[9].Id.ToString(),
                        UserId = users[9].Id
                    },
                    // AccountIntegrityId
                    new IdentityUserClaim<Guid>
                    {
                        Id = 21,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[0].AccountIntegrityId.ToString(),
                        UserId = users[0].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 22,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[1].AccountIntegrityId.ToString(),
                        UserId = users[1].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 23,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[2].AccountIntegrityId.ToString(),
                        UserId = users[2].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 24,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[3].AccountIntegrityId.ToString(),
                        UserId = users[3].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 25,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[4].AccountIntegrityId.ToString(),
                        UserId = users[4].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 26,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[5].AccountIntegrityId.ToString(),
                        UserId = users[5].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 27,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[6].AccountIntegrityId.ToString(),
                        UserId = users[6].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 28,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[7].AccountIntegrityId.ToString(),
                        UserId = users[7].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 29,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[8].AccountIntegrityId.ToString(),
                        UserId = users[8].Id
                    },
                    new IdentityUserClaim<Guid>
                    {
                        Id = 30,
                        ClaimType = CustomClaimTypes.AccountIntegrityId,
                        ClaimValue = users[9].AccountIntegrityId.ToString(),
                        UserId = users[9].Id
                    },
                }
            );
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        Name = Constants.AdminRoleName,
                        NormalizedName = Constants.AdminRoleName.ToUpper(),
                    },
                    new IdentityRole<Guid>
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        Name = Constants.UserRoleName,
                        NormalizedName = Constants.UserRoleName.ToUpper(),
                    }
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new[]
                {
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[0].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[1].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[2].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[3].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[4].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[5].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[6].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[7].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[8].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                    new IdentityUserRole<Guid>
                    {
                        UserId = users[9].Id,
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    },
                }
            );

            // SEEDING
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Location>().HasData(
                    new[]
                    {
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            PostalCode = "9900",
                            City = "Eeklo",
                            Street = "Burgemeester Pussemierstraat 3",
                            Name = "Sportpark Eeklo",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000001.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Latitude = 51.1845092f,
                            Longitude = 3.57635f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            PostalCode = "2340",
                            City = "Beerse",
                            Street = "Rerum Novarumlaan 31",
                            Name = "Sportcentrum Beerse",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000002.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Latitude = 51.3090487f,
                            Longitude = 4.8408717f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            PostalCode = "9930",
                            City = "Lievegem",
                            Street = "Den Boer 17",
                            Name = "Zwembad Den Boer",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000003.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            Latitude = 51.1231295f,
                            Longitude = 3.5617187f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            PostalCode = "9950",
                            City = "Lievegem",
                            Street = "Kleine Weg 3",
                            Name = "Sportcentrum Waarschoot",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000004.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            Latitude = 51.1544607966285f,
                            Longitude = 3.6095564713173656f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            PostalCode = "8310",
                            City = "Brugge",
                            Street = "Nijverheidsstraat 112",
                            Name = "Sport Vlaanderen Brugge",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000005.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            Latitude = 51.206584303790734f,
                            Longitude = 3.2418122401716225f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            PostalCode = "9000",
                            City = "Gent",
                            Street = "Sint-Denijslaan 251",
                            Name = "Sporthal Gent",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000006.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            Latitude = 51.03460888942907f,
                            Longitude = 3.7039249374761756f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            PostalCode = "9052",
                            City = "Gent",
                            Street = "Ter Linden 29",
                            Name = "Sporthal Hekers",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000007.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Latitude = 51.00321447653393f,
                            Longitude = 3.710870170850151f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            PostalCode = "9880",
                            City = "Aalter",
                            Street = "Lindestraat 17",
                            Name = "Sportpark Aalter-centrum",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000008.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            Latitude = 51.08936794559371f,
                            Longitude = 3.438724672873933f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            PostalCode = "8400",
                            City = "Oostende",
                            Street = "Sportparklaan 6",
                            Name = "Mister V-arena",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000009.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            Latitude = 51.209963031141044f,
                            Longitude = 2.924667474469436f
                        },
                        new Location
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            PostalCode = "8450",
                            City = "Bredene",
                            Street = "Spuikomlaan 21",
                            Name = "Sporthal Ter Polder",
                            ImageUrl = "images/locations/00000000-0000-0000-0000-000000000010.jpg",
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            Latitude = 51.23793818789807f,
                            Longitude = 2.96593766830851f
                        }
                    }
            );
            modelBuilder.Entity<Racket>().HasData(
                    new[]
                    {
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Brand = "Yonex",
                            Model = "Voltric DG7 Lime",
                            RacketType = RacketType.Attacking,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000001.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Brand = "Yonex",
                            Model = "Atrox 2 Blue",
                            RacketType = RacketType.Defending,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000002.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            Brand = "Yonex",
                            Model = "Atrox 5 FX",
                            RacketType = RacketType.AllRounder,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000003.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            Brand = "Yonex",
                            Model = "Isometric TR-1 White",
                            RacketType = RacketType.Attacking,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000004.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            Brand = "Yonex",
                            Model = "Nanoflare Blue",
                            RacketType = RacketType.AllRounder,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000005.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            Brand = "Yonex",
                            Model = "Astrox 55 Light Silver",
                            RacketType = RacketType.AllRounder,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000006.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Brand = "Perfly",
                            Model = "BR700",
                            RacketType = RacketType.Attacking,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000007.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            Brand = "Adidas",
                            Model = "E08.2 Groen",
                            RacketType = RacketType.Defending,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000008.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            Brand = "Idema",
                            Model = "Spordas Junior",
                            RacketType = RacketType.AllRounder,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000009.jpg"
                        },
                        new Racket
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            Brand = "Victor",
                            Model = "Advanced Junior",
                            RacketType = RacketType.AllRounder,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            ImageUrl = "images/rackets/00000000-0000-0000-0000-000000000010.jpg"
                        }
                    }
            );
            modelBuilder.Entity<ShuttleCock>().HasData(
                    new[]
                    {
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Brand = "Perfly",
                            Model = "FSC 930",
                            ShuttleType = ShuttleType.Feather,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000001.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Brand = "Perfly",
                            Model = "PSC 100",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000002.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            Brand = "Perfly",
                            Model = "PSC 130",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000003.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            Brand = "Yonex",
                            Model = "League 7",
                            ShuttleType = ShuttleType.Feather,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000004.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            Brand = "Yonex",
                            Model = "Mavis 350",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000005.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            Brand = "Yonex",
                            Model = "Aerosensa 30",
                            ShuttleType = ShuttleType.Feather,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000006.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Brand = "Yonex",
                            Model = "Mavis 200",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000007.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            Brand = "Yonex",
                            Model = "Mavis 2000",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000008.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            Brand = "Yonex",
                            Model = "Mavis 600",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000009.jpg"
                        },
                        new ShuttleCock
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            Brand = "Yonex",
                            Model = "Mavis 10",
                            ShuttleType = ShuttleType.Plastic,
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            ImageUrl = "images/shuttlecocks/00000000-0000-0000-0000-000000000010.jpg"
                        }
                    }
            );
            modelBuilder.Entity<Game>().HasData(
                    new[]
                    {
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            Opponent = "Louis Caessens",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            Opponent = "Felien Braeckevelt",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            Opponent = "Filip Bruyr",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            Opponent = "Amber Lippens",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            Opponent = "Stefaan Turpyn",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            Opponent = "Wesley Caessens",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            Opponent = "Louis Caessens",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            Opponent = "Tine Franchois",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            Opponent = "Tine Franchois",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                        },
                        new Game
                        {
                            Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            LocationId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            Opponent = "John Doe",
                            RacketId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                            UserId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                        }
                    }
            );

        }
    }
}
