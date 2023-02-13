using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoryHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTagData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
