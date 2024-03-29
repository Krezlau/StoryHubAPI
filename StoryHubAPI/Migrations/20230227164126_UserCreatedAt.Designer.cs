﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoryHubAPI.Data;

#nullable disable

namespace StoryHubAPI.Migrations
{
    [DbContext(typeof(StoryHubDbContext))]
    [Migration("20230227164126_UserCreatedAt")]
    partial class UserCreatedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("StoryHubAPI.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("StoryHubAPI.Models.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("StoryHubAPI.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("StoryHubAPI.Models.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("StoryHubAPI.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5d229ba2-f6d5-4b07-b8b6-cb855b9ddbc2"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(1990),
                            Name = "erotic"
                        },
                        new
                        {
                            Id = new Guid("62d9798d-341a-4154-9f7b-f4f6f49a04e7"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2039),
                            Name = "ghosts"
                        },
                        new
                        {
                            Id = new Guid("9e40af23-2e94-48c7-b70d-20d2d74efcb8"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2042),
                            Name = "horror"
                        },
                        new
                        {
                            Id = new Guid("4347863d-c4b7-4534-9e2e-992e91c2bf6b"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2045),
                            Name = "zombies"
                        },
                        new
                        {
                            Id = new Guid("208c0e15-ab87-49e9-85cd-8ce1f8749b0e"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2048),
                            Name = "monsters"
                        },
                        new
                        {
                            Id = new Guid("fde251d8-f78d-481b-b1b6-48c5e1f2a949"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2052),
                            Name = "aliens"
                        },
                        new
                        {
                            Id = new Guid("349c0c4c-3f29-4c44-977b-49b4b073b298"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2055),
                            Name = "apocalypse"
                        },
                        new
                        {
                            Id = new Guid("e2679ce6-2395-42cd-a42c-12b5ecfc14af"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2058),
                            Name = "dystopia"
                        },
                        new
                        {
                            Id = new Guid("06ad0935-ae5a-4c3b-b44e-234dbb5f7f5e"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2063),
                            Name = "utopia"
                        },
                        new
                        {
                            Id = new Guid("0c779e8d-5d5e-4f78-aa68-ecdffd38d882"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2067),
                            Name = "worldbuilding"
                        },
                        new
                        {
                            Id = new Guid("6b3279d8-c99c-4825-abaf-0965c9b7896c"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2069),
                            Name = "spiritual"
                        },
                        new
                        {
                            Id = new Guid("ec6d0289-3fe9-4e34-a942-a28f4c40518d"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2072),
                            Name = "science fiction"
                        },
                        new
                        {
                            Id = new Guid("b13f8a35-c19f-44d6-87e9-3bf6100ba27d"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2075),
                            Name = "fantasy"
                        },
                        new
                        {
                            Id = new Guid("3c1ac7ec-d810-448b-9b3e-d74f870d3d9f"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2077),
                            Name = "fairy tale"
                        },
                        new
                        {
                            Id = new Guid("e9a56330-d015-4250-b46c-efa76b38cd92"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2080),
                            Name = "mythology"
                        },
                        new
                        {
                            Id = new Guid("4bed1426-5d4e-4c0a-b5d8-35c95cb5e0ac"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2082),
                            Name = "historical fiction"
                        },
                        new
                        {
                            Id = new Guid("f07c0fe8-5ab9-4b19-b85a-562242ac5fb9"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2087),
                            Name = "historical"
                        },
                        new
                        {
                            Id = new Guid("9061a704-1125-42e7-a871-af22baa4c02f"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2091),
                            Name = "time travel"
                        },
                        new
                        {
                            Id = new Guid("ba1b3560-cc04-4643-a3b1-19c2c85e7b70"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2093),
                            Name = "action"
                        },
                        new
                        {
                            Id = new Guid("56aaed01-0bbe-4077-8428-ccf41b5216f2"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2096),
                            Name = "superhero"
                        },
                        new
                        {
                            Id = new Guid("2190ea02-f4bd-4bdd-ae0d-b7ea4b41941e"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2099),
                            Name = "murder"
                        },
                        new
                        {
                            Id = new Guid("3597f3fd-cf32-448a-b096-056f16aff677"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2101),
                            Name = "thriller"
                        },
                        new
                        {
                            Id = new Guid("d84a8bfb-36c3-4a21-be0d-e25c7be41f46"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2105),
                            Name = "humor"
                        },
                        new
                        {
                            Id = new Guid("786570d1-414b-4ada-ba10-fde81e2ef42f"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2107),
                            Name = "legend"
                        },
                        new
                        {
                            Id = new Guid("14825d41-49d1-4cc1-b704-d4af7106d54a"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2112),
                            Name = "young adult"
                        },
                        new
                        {
                            Id = new Guid("45c17aef-b2c0-4e96-b43b-e6f4a7674628"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2115),
                            Name = "futuristic"
                        },
                        new
                        {
                            Id = new Guid("32e99654-f528-4f5f-89b7-855260d4c787"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2117),
                            Name = "fiction"
                        },
                        new
                        {
                            Id = new Guid("a3976a81-bef7-465a-a4e6-05f87242e66b"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2120),
                            Name = "cyberpunk"
                        },
                        new
                        {
                            Id = new Guid("165d273c-11b2-4701-95d9-f5576d5b736f"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2123),
                            Name = "dark fantasy"
                        },
                        new
                        {
                            Id = new Guid("087370d1-7479-4d4f-96a8-b13eb03d0d5d"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2125),
                            Name = "comedy"
                        },
                        new
                        {
                            Id = new Guid("6f5e965d-d3fd-40a1-8bb5-efbef37bdae1"),
                            CreatedAt = new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2128),
                            Name = "romance"
                        });
                });

            modelBuilder.Entity("StoryHubAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("StoryTag", b =>
                {
                    b.Property<Guid>("StoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StoriesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("StoryTag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StoryHubAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StoryHubAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryHubAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StoryHubAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoryHubAPI.Models.Comment", b =>
                {
                    b.HasOne("StoryHubAPI.Models.Story", "Story")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryHubAPI.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoryHubAPI.Models.Like", b =>
                {
                    b.HasOne("StoryHubAPI.Models.Story", "Story")
                        .WithMany("Likes")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryHubAPI.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoryHubAPI.Models.RefreshToken", b =>
                {
                    b.HasOne("StoryHubAPI.Models.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("StoryHubAPI.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoryHubAPI.Models.Story", b =>
                {
                    b.HasOne("StoryHubAPI.Models.User", "Author")
                        .WithMany("Stories")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("StoryTag", b =>
                {
                    b.HasOne("StoryHubAPI.Models.Story", null)
                        .WithMany()
                        .HasForeignKey("StoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryHubAPI.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoryHubAPI.Models.Story", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("StoryHubAPI.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("RefreshToken")
                        .IsRequired();

                    b.Navigation("Stories");
                });
#pragma warning restore 612, 618
        }
    }
}
