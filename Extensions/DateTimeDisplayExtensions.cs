namespace SimpleBlazor.Extensions;

public static class DateTimeDisplayExtensions
{
    public static string AsLocalDate(this DateTime? value)
        => value?.ToString("dd/MM/yyyy") ?? string.Empty;
}