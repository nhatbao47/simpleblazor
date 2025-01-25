using System.ComponentModel.DataAnnotations;

public class TaskModel
{
    public int Id { get; set;}

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200)]
    public string Title { get; set;}

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000)]
    public string Description { get; set;}

    [Required(ErrorMessage = "Status is required")]
    public TaskState? State { get; set;}
}

public enum TaskState
{
    
    New = 1,
    InProgress,
    Done
}