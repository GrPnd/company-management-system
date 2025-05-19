namespace App.DAL.EF.DataSeeding;

public static class InitialData
{
    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
            ("user", null),
        ];

    public static readonly (string name, string firstName, string lastName, string password, Guid? id, string[] roles)[]
        Users =
        [
            ("admin@cc.ee", "admin", "collabcore", "Foo.Bar.1", null, ["admin"]),
        ];
    
    
    public static readonly (string teamRoleName, Guid? id)[]
        TeamRoles =
        [
            ("Worker", null),
            ("Team Leader", null),
        ];
    
    public static readonly (string statusName, Guid? id)[]
        Statuses =
        [
            ("Assigned", null),
            ("Completing", null),
            ("Done", null),
        ];
}