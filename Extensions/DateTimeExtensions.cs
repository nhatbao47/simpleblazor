namespace SimpleBlazor.Extensions;

public static class DateTimeExtensions
{
    public static string AsLocalDate(this DateTime? value)
        => value?.ToString("dd/MM/yyyy") ?? string.Empty;
    
    public static DateOnly ToDateOnly(this DateTime? dateTime)
        => DateOnly.FromDateTime(dateTime?.Date ?? DateTime.Now);

    public static DateOnly ToDateOnly(this DateTime dateTime)
        => DateOnly.FromDateTime(dateTime);

    public static TimeOnly ToTimeOnly(this DateTime dateTime)
        => TimeOnly.FromDateTime(dateTime);
}