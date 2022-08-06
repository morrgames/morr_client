using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;

public static class Compress
{
    public static byte[] CompressData(byte[] input)
    {
        Deflater compressor = new Deflater();
        compressor.SetLevel(Deflater.BEST_COMPRESSION);
        compressor.SetInput(input);
        compressor.Finish();

        byte[] buf = new byte[1024];
        using MemoryStream bos = new MemoryStream(input.Length);
        while (!compressor.IsFinished)
        {
            var len = compressor.Deflate(buf);
            bos.Write(buf, 0, len);
        }
        var res = bos.ToArray();
        return res;
    }

    public static byte[] DecompressData(byte[] input)
    {
        Inflater decompressor = new Inflater();
        decompressor.SetInput(input);

        byte[] buf = new byte[1024];
        using MemoryStream bos = new MemoryStream(input.Length);
        while (!decompressor.IsFinished)
        {
            var len = decompressor.Inflate(buf);
            bos.Write(buf, 0, len);
        }
        var res = bos.ToArray();
        return res;
    }
}