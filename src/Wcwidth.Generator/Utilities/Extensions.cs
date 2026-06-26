namespace Wcwidth.Generator;

public static class Extensions
{
    public static string? GetGroupValue(this Match match, string group, string? defaultValue = null)
    {
        if (match is null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        return match.Groups[group].Success
            ? match.Groups[group].Value
            : defaultValue;
    }

    private static IEnumerable<(int Index, T Item)> Enumerate<T>(this IEnumerator<T> source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var last = !source.MoveNext();

        for (var index = 0; !last; index++)
        {
            var current = source.Current;
            last = !source.MoveNext();
            yield return (index, current);
        }
    }

    public static IEnumerable<(int Index, T Item)> Enumerate<T>(this IEnumerable<T> source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return Enumerate(source.GetEnumerator());
    }

    public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            source.Add(item);
        }
    }

    public static (string Before, string After) Partition(this string text, string separator)
    {
        var index = text.IndexOf(separator, StringComparison.Ordinal);
        if (index == -1)
        {
            return (text, string.Empty);
        }

        return (
            text[..index],
            text.Substring(index, text.Length - index));
    }
}