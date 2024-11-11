using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Evently.Modules.Users.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddRBACModel : Migration
{
    private static readonly string[] columns = ["permission_code", "role_name"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "permissions",
            schema: "users",
            columns: table => new
            {
                code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_permissions", x => x.code);
            });

        migrationBuilder.CreateTable(
            name: "roles",
            schema: "users",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "role_permissions",
            schema: "users",
            columns: table => new
            {
                permission_code = table.Column<string>(type: "character varying(100)", nullable: false),
                role_name = table.Column<string>(type: "character varying(50)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_permissions", x => new { x.permission_code, x.role_name });
                table.ForeignKey(
                    name: "fk_role_permissions_permissions_permission_code",
                    column: x => x.permission_code,
                    principalSchema: "users",
                    principalTable: "permissions",
                    principalColumn: "code",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_permissions_roles_role_name",
                    column: x => x.role_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "user_roles",
            schema: "users",
            columns: table => new
            {
                role_name = table.Column<string>(type: "character varying(50)", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_roles", x => new { x.role_name, x.user_id });
                table.ForeignKey(
                    name: "fk_user_roles_roles_roles_name",
                    column: x => x.role_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_user_roles_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "permissions",
            column: "code",
            values:
            [
                "cart:read",
                "cart:update",
                "category:read",
                "category:write",
                "event-statistics:read",
                "event:read",
                "event:write",
                "order:create",
                "order:read",
                "ticket-type:read",
                "ticket-type:write",
                "ticket:check-in",
                "ticket:read",
                "user:read",
                "user:write"
            ]);

        migrationBuilder.InsertData(
            schema: "users",
            table: "roles",
            column: "name",
            values:
            [
                "Administrator",
                "Member"
            ]);

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: columns,
            values: new object[,]
            {
                { "cart:read", "Administrator" },
                { "cart:read", "Member" },
                { "cart:update", "Administrator" },
                { "cart:update", "Member" },
                { "category:read", "Administrator" },
                { "category:write", "Administrator" },
                { "event-statistics:read", "Administrator" },
                { "event:read", "Administrator" },
                { "event:read", "Member" },
                { "event:write", "Administrator" },
                { "order:create", "Administrator" },
                { "order:create", "Member" },
                { "order:read", "Administrator" },
                { "order:read", "Member" },
                { "ticket-type:read", "Administrator" },
                { "ticket-type:read", "Member" },
                { "ticket-type:write", "Administrator" },
                { "ticket:check-in", "Administrator" },
                { "ticket:check-in", "Member" },
                { "ticket:read", "Administrator" },
                { "ticket:read", "Member" },
                { "user:read", "Administrator" },
                { "user:read", "Member" },
                { "user:write", "Administrator" },
                { "user:write", "Member" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_role_permissions_role_name",
            schema: "users",
            table: "role_permissions",
            column: "role_name");

        migrationBuilder.CreateIndex(
            name: "ix_user_roles_user_id",
            schema: "users",
            table: "user_roles",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "role_permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "user_roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "roles",
            schema: "users");
    }
}
