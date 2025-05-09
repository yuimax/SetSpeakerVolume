using System;
using NAudio.CoreAudioApi;

class SetVol
{
    static void Main(string[] args)
    {
        int volumeLevel = 0;

        if (args.Length == 0)
        {
            ShowVolume();
        }
        else if (args.Length != 1
            || !int.TryParse(args[0], out volumeLevel)
            || volumeLevel < 0
            || volumeLevel > 100)
        {
            ShowUsage();
        }
        else
        {
            SetVolume(volumeLevel);
        }
    }

    static void ShowVolume()
    {
        try
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia))
                {
                    float volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
                    Console.WriteLine($"Volume is now {Math.Round(volume * 100.0f)}%");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting volume: {ex.Message}");
        }
    }

    static void SetVolume(int volumeLevel)
    {
        try
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia))
                {
                    float newVolume = volumeLevel / 100.0f;
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume;
                    Console.WriteLine($"Volume set to {volumeLevel}%");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting volume: {ex.Message}");
        }
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage: setvol { -h | 0...100 }");
        Console.WriteLine(" -h      : show this Usage");
        Console.WriteLine(" 0...100 : set volume");
        Console.WriteLine(" no args : show current volume");
    }
}