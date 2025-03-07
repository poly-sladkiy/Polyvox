using NAudio.Wave;

using(var audioFile = new AudioFileReader(@"C:\Users\k.ignakov\Downloads\Music\06. Первобытный страх.mp3"))
using (var outputDevice = new WaveOutEvent())
{
    outputDevice.Init(audioFile);
    outputDevice.Play();
    while (outputDevice.PlaybackState == PlaybackState.Playing)
    {
        Thread.Sleep(1000);
    }
}

Console.WriteLine("Hello, World!");