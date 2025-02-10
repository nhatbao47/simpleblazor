using System.ComponentModel.DataAnnotations;

public class ScheduleModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Creator is required")]
    public int? UserId { get; set; }

    public string Creator { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required(ErrorMessage = "Location is required")]
    [MaxLength(100)]
    public string Location { get; set; }

    public DateTime? Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public DateOnly DatePicker { get; set; }
    public TimeOnly StartTimePicker { get; set; }
    public TimeOnly EndTimePicker { get; set; }
}