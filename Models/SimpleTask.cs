public class SimpleTask
{
    public int Id { get; set;}
    public string Title { get; set;}
    public string Description { get; set;}
    public TaskState State { get; set;}
}

public enum TaskState
{
    New = 1,
    Inprogress,
    Done
}