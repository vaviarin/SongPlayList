using System;
using System.Collections.Generic;

// Song is a record, so two songs with matching values are considered equal.
public record Song(string Title, string Artist, int DurationSeconds);

public class Playlist
{
    private readonly List<Song> _songs = new();

    public int Count => _songs.Count;

    // Calculate the total from the current songs each time this property is read.
    public int TotalDuration => _songs.Sum(song => song.DurationSeconds);

    public Song this[int index] => _songs[index];

    public void Add(Song song)
    {
        ArgumentNullException.ThrowIfNull(song);

        if (song.DurationSeconds < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(song), "Duration must be zero or greater.");
        }

        _songs.Add(song);
    }
}

public static class Program
{
    public static void Main()
    {
        var leadTrack = new Song("Northern Lights", "Mira Sol", 214);
        var matchingTrack = new Song("Northern Lights", "Mira Sol", 214);

        Console.WriteLine("VALUE EQUALITY");
        Console.WriteLine($"Matching song records compare equal: {leadTrack == matchingTrack}");

        // Create a copy and change one init-only property.
        var acousticVersion = leadTrack with { Title = "Northern Lights (Acoustic)" };
        Console.WriteLine($"Updated copy: {acousticVersion}");
        Console.WriteLine($"Copy still equals the original: {leadTrack == acousticVersion}");

        var roadTripPlaylist = new Playlist();
        roadTripPlaylist.Add(leadTrack);
        roadTripPlaylist.Add(new Song("Paper Planes", "The Daydreamers", 197));
        roadTripPlaylist.Add(new Song("Golden Hour", "June Harbor", 246));

        Console.WriteLine();
        Console.WriteLine("ROAD TRIP PLAYLIST");
        Console.WriteLine($"Track count: {roadTripPlaylist.Count}");
        Console.WriteLine($"Combined play time: {FormatDuration(roadTripPlaylist.TotalDuration)}");
        Console.WriteLine($"Opening track: {roadTripPlaylist[0].Title} by {roadTripPlaylist[0].Artist}");
    }

    private static string FormatDuration(int totalSeconds)
    {
        var minutes = totalSeconds / 60;
        var seconds = totalSeconds % 60;
        return $"{minutes}:{seconds:D2}";
    }
}