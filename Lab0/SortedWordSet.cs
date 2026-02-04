
namespace Lab0;

public class SortedWordSet : IWordSet
{
    private SortedSet<string> words = new();

    public int Count => words.Count;

    private const string MAX_STRING = "\uFFFF\uFFFF\uFFFF";

    public bool Add(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Add(normalizedWord);
    }

    public bool Remove(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Remove(normalizedWord);
    }

    public bool Contains(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0) return false;
        return words.Contains(normalizedWord);
    }

    public string? Next(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0) return null;

        var wordsInRange = words.GetViewBetween(normalizedWord, MAX_STRING);

        foreach (var candidate in wordsInRange)
        {
            if(candidate.CompareTo(normalizedWord) > 0)
            {
                return candidate;
            }
        }

        return null;

    }

    public string? Prev(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0) return null;

        var wordsInRange = words.GetViewBetween("", normalizedWord);

        foreach (var candidate in wordsInRange.Reverse())
        {
            // System.Console.WriteLine($"{candidate} - {normalizedWord}: {candidate.CompareTo(normalizedWord)}");
            if(candidate.CompareTo(normalizedWord) < 0)
            {
                // System.Console.WriteLine($"Returning {candidate}");
                return candidate;
            }
        }
        // System.Console.WriteLine("No previous word found!");
        return null;
    }

    public IEnumerable<string> Prefix(string prefix, int k)
    {
        if (k <= 0 || words.Count ==0) return new List<string>();
        var normalizedPrefix = Normalize(prefix);

        var results = new List<string>();
        string lo = normalizedPrefix;
        string hi = normalizedPrefix + "\uFFFF";

        foreach( var candidate in words.GetViewBetween(lo, hi))
        {
            results.Add(candidate);
            if(results.Count >= k) return results;
        }

        return results;
    }


    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        if (k <= 0 || words.Count ==0) return new List<string>();
        var normalizedLo = Normalize(lo);
        var normalizedHi = Normalize(hi);

        var results = new List<string>();

        foreach( var candidate in words.GetViewBetween(normalizedLo, normalizedHi))
        {
            results.Add(candidate);
            if(results.Count >= k) return results;
        }

        return results;
    }

    private string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return string.Empty;

        return word.Trim().ToLowerInvariant();
    }
    
}