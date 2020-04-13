namespace AbpMicroRabbit.Banking.Application.Contracts.Permissions
{
    public class AccountPermissions
    {
        public const string GroupName = "Accounts";

        public static class Accounts
        {
            public const string Default = GroupName + ".Account";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Transfer = Default + ".Transfer";
        }
    }
}
