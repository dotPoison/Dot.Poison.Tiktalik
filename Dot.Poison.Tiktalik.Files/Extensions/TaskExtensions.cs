namespace Dot.Poison.Tiktalik.Files.Extensions;

internal static class TaskExtensions
{
    public static TResult RunTaskSync<TResult>(this Task<TResult> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();
}
