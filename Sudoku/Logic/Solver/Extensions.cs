using System.Collections.Generic;

namespace Sudoku.Logic.Solver;

internal static class Extensions
{
    public static IEnumerable<string> Cut(this string input, int length)
    {
        for (var cursor = 0; cursor < input.Length; cursor += length)
            if (cursor + length > input.Length) yield return input[cursor..];
            else yield return input.Substring(cursor, length);
    }

    public static string DelimitWith<T>(this IEnumerable<T> source, string separator)
    {
        return string.Join(separator, source);
    }
}