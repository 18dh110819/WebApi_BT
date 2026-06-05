public class UserUpdateDto : UserInsertDto
{
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
}