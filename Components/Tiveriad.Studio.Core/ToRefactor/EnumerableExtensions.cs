namespace Tiveriad.Studio.Core.ToRefactor;

public  static class EnumerableExtensions
{
    
    public static bool AtLeast<T>(this IEnumerable<T> source, int count) =>
        source.Take(count).Count() == count;

    public static TResult[] SelectToArray<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector) =>
        source.Select(selector).ToArray();
}