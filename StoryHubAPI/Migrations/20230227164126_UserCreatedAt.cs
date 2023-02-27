using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoryHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("03e508ae-3e1d-48b6-baf7-e9cfef5f5807"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("092daa83-651f-4ca5-bd1b-96f97f8f4488"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0af004f0-819e-4cb9-adf3-32142862fc84"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0c7ad4fd-3f12-43e0-8932-d55869ad963d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("103b7295-d24b-4a06-a02d-137841541aa5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1fce7c71-e049-4175-806a-09ddf332b67b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2bec8192-f8e6-4997-ae3f-9fff4e4b8fce"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("351f363a-36e3-40e9-b8b2-1e9ccef6e74b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3b6ad72a-3548-4608-ab0a-679084261523"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3d5fbaf4-5757-4e69-bcb8-284b357bbbd1"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("44181a8f-3a7c-4432-8f11-8306fa1c2009"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5d663aae-ee29-4687-8b35-6f785356f72b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6c97deb9-5936-498d-8687-e69070e578ba"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("71e94a09-7fad-4ffc-8153-0994036317b5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7819b913-9752-4794-998a-1ef8488a806f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("84e08a62-19ac-42cf-b95e-183f69495d68"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("88e6decb-2a3f-4bbd-9253-f80d8662286a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("96dd63f3-609c-49a2-8f1a-5ab1ee38c175"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("afd49b1f-1685-4fcb-a0f1-7e7d7fb71f78"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b0ff1ad9-c583-47b9-aa36-5cf03be9cd21"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b12d3e89-5982-44de-994f-bafe7c97b652"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b148083f-5c7d-42c0-9d11-7f84c209e78c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b4d92b4d-ad8e-4d89-b6b2-845b0460171c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b6ec44fd-429c-4376-8dab-3aeacf053450"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b9e27dd2-16b6-4d6e-9a47-aeb0b0592829"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c302fb2a-5190-42f9-a747-40c425058186"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c55711f5-627c-45be-84a8-83ac5c748adb"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cde300c2-bfea-402f-801a-c64edd013934"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d1162a53-c27c-4999-b4f5-e686a21f9f7b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d84338ba-50dd-4a4b-acd0-2133053954bf"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("dc04759e-bc2b-4075-b6ca-8771de7b0144"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "LastModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("06ad0935-ae5a-4c3b-b44e-234dbb5f7f5e"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2063), null, "utopia" },
                    { new Guid("087370d1-7479-4d4f-96a8-b13eb03d0d5d"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2125), null, "comedy" },
                    { new Guid("0c779e8d-5d5e-4f78-aa68-ecdffd38d882"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2067), null, "worldbuilding" },
                    { new Guid("14825d41-49d1-4cc1-b704-d4af7106d54a"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2112), null, "young adult" },
                    { new Guid("165d273c-11b2-4701-95d9-f5576d5b736f"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2123), null, "dark fantasy" },
                    { new Guid("208c0e15-ab87-49e9-85cd-8ce1f8749b0e"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2048), null, "monsters" },
                    { new Guid("2190ea02-f4bd-4bdd-ae0d-b7ea4b41941e"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2099), null, "murder" },
                    { new Guid("32e99654-f528-4f5f-89b7-855260d4c787"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2117), null, "fiction" },
                    { new Guid("349c0c4c-3f29-4c44-977b-49b4b073b298"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2055), null, "apocalypse" },
                    { new Guid("3597f3fd-cf32-448a-b096-056f16aff677"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2101), null, "thriller" },
                    { new Guid("3c1ac7ec-d810-448b-9b3e-d74f870d3d9f"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2077), null, "fairy tale" },
                    { new Guid("4347863d-c4b7-4534-9e2e-992e91c2bf6b"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2045), null, "zombies" },
                    { new Guid("45c17aef-b2c0-4e96-b43b-e6f4a7674628"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2115), null, "futuristic" },
                    { new Guid("4bed1426-5d4e-4c0a-b5d8-35c95cb5e0ac"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2082), null, "historical fiction" },
                    { new Guid("56aaed01-0bbe-4077-8428-ccf41b5216f2"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2096), null, "superhero" },
                    { new Guid("5d229ba2-f6d5-4b07-b8b6-cb855b9ddbc2"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(1990), null, "erotic" },
                    { new Guid("62d9798d-341a-4154-9f7b-f4f6f49a04e7"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2039), null, "ghosts" },
                    { new Guid("6b3279d8-c99c-4825-abaf-0965c9b7896c"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2069), null, "spiritual" },
                    { new Guid("6f5e965d-d3fd-40a1-8bb5-efbef37bdae1"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2128), null, "romance" },
                    { new Guid("786570d1-414b-4ada-ba10-fde81e2ef42f"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2107), null, "legend" },
                    { new Guid("9061a704-1125-42e7-a871-af22baa4c02f"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2091), null, "time travel" },
                    { new Guid("9e40af23-2e94-48c7-b70d-20d2d74efcb8"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2042), null, "horror" },
                    { new Guid("a3976a81-bef7-465a-a4e6-05f87242e66b"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2120), null, "cyberpunk" },
                    { new Guid("b13f8a35-c19f-44d6-87e9-3bf6100ba27d"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2075), null, "fantasy" },
                    { new Guid("ba1b3560-cc04-4643-a3b1-19c2c85e7b70"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2093), null, "action" },
                    { new Guid("d84a8bfb-36c3-4a21-be0d-e25c7be41f46"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2105), null, "humor" },
                    { new Guid("e2679ce6-2395-42cd-a42c-12b5ecfc14af"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2058), null, "dystopia" },
                    { new Guid("e9a56330-d015-4250-b46c-efa76b38cd92"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2080), null, "mythology" },
                    { new Guid("ec6d0289-3fe9-4e34-a942-a28f4c40518d"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2072), null, "science fiction" },
                    { new Guid("f07c0fe8-5ab9-4b19-b85a-562242ac5fb9"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2087), null, "historical" },
                    { new Guid("fde251d8-f78d-481b-b1b6-48c5e1f2a949"), new DateTime(2023, 2, 27, 17, 41, 26, 40, DateTimeKind.Local).AddTicks(2052), null, "aliens" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("06ad0935-ae5a-4c3b-b44e-234dbb5f7f5e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("087370d1-7479-4d4f-96a8-b13eb03d0d5d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0c779e8d-5d5e-4f78-aa68-ecdffd38d882"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("14825d41-49d1-4cc1-b704-d4af7106d54a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("165d273c-11b2-4701-95d9-f5576d5b736f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("208c0e15-ab87-49e9-85cd-8ce1f8749b0e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2190ea02-f4bd-4bdd-ae0d-b7ea4b41941e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("32e99654-f528-4f5f-89b7-855260d4c787"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("349c0c4c-3f29-4c44-977b-49b4b073b298"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3597f3fd-cf32-448a-b096-056f16aff677"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3c1ac7ec-d810-448b-9b3e-d74f870d3d9f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4347863d-c4b7-4534-9e2e-992e91c2bf6b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("45c17aef-b2c0-4e96-b43b-e6f4a7674628"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4bed1426-5d4e-4c0a-b5d8-35c95cb5e0ac"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("56aaed01-0bbe-4077-8428-ccf41b5216f2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5d229ba2-f6d5-4b07-b8b6-cb855b9ddbc2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("62d9798d-341a-4154-9f7b-f4f6f49a04e7"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6b3279d8-c99c-4825-abaf-0965c9b7896c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6f5e965d-d3fd-40a1-8bb5-efbef37bdae1"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("786570d1-414b-4ada-ba10-fde81e2ef42f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9061a704-1125-42e7-a871-af22baa4c02f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9e40af23-2e94-48c7-b70d-20d2d74efcb8"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a3976a81-bef7-465a-a4e6-05f87242e66b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b13f8a35-c19f-44d6-87e9-3bf6100ba27d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ba1b3560-cc04-4643-a3b1-19c2c85e7b70"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d84a8bfb-36c3-4a21-be0d-e25c7be41f46"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e2679ce6-2395-42cd-a42c-12b5ecfc14af"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e9a56330-d015-4250-b46c-efa76b38cd92"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ec6d0289-3fe9-4e34-a942-a28f4c40518d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f07c0fe8-5ab9-4b19-b85a-562242ac5fb9"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("fde251d8-f78d-481b-b1b6-48c5e1f2a949"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "LastModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("03e508ae-3e1d-48b6-baf7-e9cfef5f5807"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2146), null, "fiction" },
                    { new Guid("092daa83-651f-4ca5-bd1b-96f97f8f4488"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2096), null, "worldbuilding" },
                    { new Guid("0af004f0-819e-4cb9-adf3-32142862fc84"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2157), null, "romance" },
                    { new Guid("0c7ad4fd-3f12-43e0-8932-d55869ad963d"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2109), null, "mythology" },
                    { new Guid("103b7295-d24b-4a06-a02d-137841541aa5"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2144), null, "futuristic" },
                    { new Guid("1fce7c71-e049-4175-806a-09ddf332b67b"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2017), null, "erotic" },
                    { new Guid("2bec8192-f8e6-4997-ae3f-9fff4e4b8fce"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2116), null, "historical" },
                    { new Guid("351f363a-36e3-40e9-b8b2-1e9ccef6e74b"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2085), null, "apocalypse" },
                    { new Guid("3b6ad72a-3548-4608-ab0a-679084261523"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2134), null, "humor" },
                    { new Guid("3d5fbaf4-5757-4e69-bcb8-284b357bbbd1"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2141), null, "young adult" },
                    { new Guid("44181a8f-3a7c-4432-8f11-8306fa1c2009"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2136), null, "legend" },
                    { new Guid("5d663aae-ee29-4687-8b35-6f785356f72b"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2099), null, "spiritual" },
                    { new Guid("6c97deb9-5936-498d-8687-e69070e578ba"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2152), null, "dark fantasy" },
                    { new Guid("71e94a09-7fad-4ffc-8153-0994036317b5"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2070), null, "ghosts" },
                    { new Guid("7819b913-9752-4794-998a-1ef8488a806f"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2131), null, "thriller" },
                    { new Guid("84e08a62-19ac-42cf-b95e-183f69495d68"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2104), null, "fantasy" },
                    { new Guid("88e6decb-2a3f-4bbd-9253-f80d8662286a"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2126), null, "superhero" },
                    { new Guid("96dd63f3-609c-49a2-8f1a-5ab1ee38c175"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2083), null, "aliens" },
                    { new Guid("afd49b1f-1685-4fcb-a0f1-7e7d7fb71f78"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2129), null, "murder" },
                    { new Guid("b0ff1ad9-c583-47b9-aa36-5cf03be9cd21"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2076), null, "zombies" },
                    { new Guid("b12d3e89-5982-44de-994f-bafe7c97b652"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2093), null, "utopia" },
                    { new Guid("b148083f-5c7d-42c0-9d11-7f84c209e78c"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2079), null, "monsters" },
                    { new Guid("b4d92b4d-ad8e-4d89-b6b2-845b0460171c"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2107), null, "fairy tale" },
                    { new Guid("b6ec44fd-429c-4376-8dab-3aeacf053450"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2088), null, "dystopia" },
                    { new Guid("b9e27dd2-16b6-4d6e-9a47-aeb0b0592829"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2073), null, "horror" },
                    { new Guid("c302fb2a-5190-42f9-a747-40c425058186"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2154), null, "comedy" },
                    { new Guid("c55711f5-627c-45be-84a8-83ac5c748adb"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2120), null, "time travel" },
                    { new Guid("cde300c2-bfea-402f-801a-c64edd013934"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2101), null, "science fiction" },
                    { new Guid("d1162a53-c27c-4999-b4f5-e686a21f9f7b"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2112), null, "historical fiction" },
                    { new Guid("d84338ba-50dd-4a4b-acd0-2133053954bf"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2123), null, "action" },
                    { new Guid("dc04759e-bc2b-4075-b6ca-8771de7b0144"), new DateTime(2023, 2, 12, 17, 46, 32, 140, DateTimeKind.Local).AddTicks(2149), null, "cyberpunk" }
                });
        }
    }
}
