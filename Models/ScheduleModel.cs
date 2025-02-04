public class ScheduleModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public string Creator { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime? Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}