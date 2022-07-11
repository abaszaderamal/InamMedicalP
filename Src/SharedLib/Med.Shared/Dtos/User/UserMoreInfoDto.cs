namespace Med.Shared.Dtos.User;

public class UserMoreInfoDto
{
    public UserInfoDto UserInfo { get; set; }
    public int GroupMngrAvg { get; set; }
    public int ProjectMngrAvg { get; set; }
    public  object  Medicines { get; set; }

}