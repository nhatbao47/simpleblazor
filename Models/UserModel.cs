using System.ComponentModel.DataAnnotations;

public class UserModel : ICloneable
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Fullname is required")]
    [MaxLength(200)]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100)]
    public string Title { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}