namespace Evently.Modules.Users.Infrastructure.Users;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);
        
        builder.Property(p => p.Code).HasMaxLength(MaxLength.Medium);

        builder.HasData(
            Permission.ReadUser,
            Permission.ReadEvent,
            Permission.ReadTicketType,
            Permission.ReadCategory,
            Permission.ReadCart,
            Permission.ReadOrder,
            Permission.ReadTicket,
            Permission.ReadEventStatistics,
            Permission.WriteUser,
            Permission.WriteEvent,
            Permission.WriteTicketType,
            Permission.WriteCategory,
            Permission.UpdateCart,
            Permission.CreateOrder,
            Permission.CheckInTicket);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");

                joinBuilder.HasData(
                    // Member permissions
                    CreateRolePermission(Role.Member, Permission.ReadUser),
                    CreateRolePermission(Role.Member, Permission.ReadEvent),
                    CreateRolePermission(Role.Member, Permission.ReadTicketType),
                    CreateRolePermission(Role.Member, Permission.ReadCart),
                    CreateRolePermission(Role.Member, Permission.ReadOrder),
                    CreateRolePermission(Role.Member, Permission.ReadTicket),
                    CreateRolePermission(Role.Member, Permission.WriteUser),
                    CreateRolePermission(Role.Member, Permission.UpdateCart),
                    CreateRolePermission(Role.Member, Permission.CreateOrder),
                    CreateRolePermission(Role.Member, Permission.CheckInTicket),
                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.ReadUser),
                    CreateRolePermission(Role.Administrator, Permission.ReadEvent),
                    CreateRolePermission(Role.Administrator, Permission.ReadTicketType),
                    CreateRolePermission(Role.Administrator, Permission.ReadCategory),
                    CreateRolePermission(Role.Administrator, Permission.ReadCart),
                    CreateRolePermission(Role.Administrator, Permission.ReadOrder),
                    CreateRolePermission(Role.Administrator, Permission.ReadTicket),
                    CreateRolePermission(Role.Administrator, Permission.ReadEventStatistics),
                    CreateRolePermission(Role.Administrator, Permission.WriteUser),
                    CreateRolePermission(Role.Administrator, Permission.WriteEvent),
                    CreateRolePermission(Role.Administrator, Permission.WriteTicketType),
                    CreateRolePermission(Role.Administrator, Permission.WriteCategory),
                    CreateRolePermission(Role.Administrator, Permission.UpdateCart),
                    CreateRolePermission(Role.Administrator, Permission.CreateOrder),
                    CreateRolePermission(Role.Administrator, Permission.CheckInTicket));
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
