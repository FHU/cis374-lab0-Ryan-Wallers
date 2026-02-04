using System.Runtime.InteropServices;

namespace Lab0;

// [ "ryan", "beau", "caleb", "rye", 
// "beautiful", "cale", "cephas", "rhino", "cervid", "cecily"
// "ethan" , "ethel"]

/// <summary>
/// WordSet implementation using HashSet. 
/// Exact lookups are fast, but ordered/prefix operations scan and sort.
/// </summary>
public sealed class HashWordSet : IWordSet
{
    private HashSet<string> words = new();

    public int Count => words.Count;

    public bool Add(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Add(normalizedWord);
    }

    public bool Contains(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Contains(normalizedWord);
    }

    public bool Remove(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Remove(normalizedWord);
    }

    /// TODO
    public string? Prev(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return null;
        string? best = null;

        // look for a better best
        foreach(var w in words)
        {
            // word > w && w > best
            if( normalizedWord.CompareTo(w) > 0
                && (best is null || w.CompareTo(best) > 0))
            {
                best = w;
            }
        }

        return best;
    }

    public string? Next(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return null;
        string? best = null;

        // look for a better best
        foreach(var w in words)
        {
            // word < w && w < best
            if( normalizedWord.CompareTo(w) < 0
                && (best is null || w.CompareTo(best) < 0))
            {
                best = w;
            }
        }

        return best;
    }

    public IEnumerable<string> Prefix(string prefix, int k)
    {
        var normalizedPrefix = Normalize(prefix);
        // if (normalizedPrefix.Length == 0) return null;
        var results = new List<string>();

        foreach(var word in words)
        {
            if(word.StartsWith(normalizedPrefix))
            {
                results.Add(word);
            }
        }

        results.Sort();

        return results.Slice(0, Math.Min(k, results.Count));
    }

    /// TODO
    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var normalizedLo = Normalize(lo);
        var normalizedHi = Normalize(hi);
        if (normalizedLo.Length == 0 || normalizedHi.Length == 0) return null;

        var results = new List<string>();

        foreach( var candidate in words)
        {
            // System.Console.WriteLine($"{normalizedLo} - {normalizedHi}: {candidate}");
            // System.Console.WriteLine($" - {candidate.CompareTo(normalizedLo)}, {candidate.CompareTo(normalizedHi)}");
            if ((candidate.CompareTo(normalizedLo) >= 0) && (candidate.CompareTo(normalizedHi) <= 0)) results.Add(candidate); //System.Console.WriteLine($"Added {candidate}");
        }
        // System.Console.WriteLine("Ran out of words!");
        results.Sort();
        return results.Take(k);
    }

    private string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return string.Empty;

        return word.Trim().ToLowerInvariant();
    }

}
