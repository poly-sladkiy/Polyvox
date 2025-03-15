namespace AudioFileService.Exceptions;

public class NotAllowedSongFileExtension()
    : Exception("Only FLAC, WAV, ALAC, and MP3 files are allowed.");