using System;
using NAudio.CoreAudioApi;

class SetVol
{
    static void Main(string[] args)
    {
        if (args.Length != 1 || !int.TryParse(args[0], out int volumeLevel) || volumeLevel < 0 || volumeLevel > 100)
        {
            Console.WriteLine("Usage: setvol <0-100>");
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