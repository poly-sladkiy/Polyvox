using Minio;
using Minio.DataModel.Args;

namespace AudioFileService.Services;

public class MinioService
{
    private readonly string _bucketName;
    private readonly IMinioClient _minioClient;

    public MinioService(IMinioClient minioClient, IConfiguration configuration)
    {
        _minioClient = minioClient;
        _bucketName = configuration.GetValue<string>("Minio:BucketName")!;
    }

    public async Task<Stream> GetFileStreamAsync(string objectName)
    {
        var memoryStream = new MemoryStream();

        var args = new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream));

        await _minioClient.GetObjectAsync(args);

        memoryStream.Position = 0; // Reset stream position for reading
        return memoryStream;
    }

    public async Task UploadFileAsync(string objectName, Stream data, string contentType)
    {
        // Make a bucket on the server, if not already present.
        var beArgs = new BucketExistsArgs().WithBucket(_bucketName);
        var found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);
        if (!found)
        {
            var mbArgs = new MakeBucketArgs()
                .WithBucket(_bucketName);
            await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
        }

        var args = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
    }
}