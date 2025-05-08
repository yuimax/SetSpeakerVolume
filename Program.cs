using System;
using NAudio.CoreAudioApi;

class SetVol
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
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
            return;
        }

        if (args.Length != 1 || !int.TryParse(args[0], out int volumeLevel) || volumeLevel < 0 || volumeLevel > 100)
        {
            Console.WriteLine("Usage: setvol { -h | 0...100 }");
            Console.WriteLine(" -h      : show this Usage");
            Console.WriteLine(" 0...100 : set volume");
            Console.WriteLine(" no args : show current volume");
            return;
        }

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
}