namespace Imi.Project.Api.Core.Entities
{
    public static class Constants
    {
        public const int PageSize = 10;
        public const string AdminRoleName = "admin";
        public const string UserRoleName = "user";
        public const string AdminPolicyName = "BeAdmin";
        public const string UserPolicyName = "BeUser";
        public const string WrongRacketTypeGivenErrorMessage = "Given RacketType was invalid. Choose between: Attacking, Defending or AllRounder";
        public const string WrongShuttleTypeGivenErrorMessage = "Given ShuttleCockType was invalid. Choose between: Feather or Plastic";
        public const string MustBeImageErrorMessage = "Given file must be of type: Image";
    }
}
