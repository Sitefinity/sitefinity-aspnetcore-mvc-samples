using System.IO;

internal class TestFileHelper
{
    internal const string ImageFile1 = "migrate_data_to_cms.Samples.Media.Data.image.jpg";

    public static Stream GetEmbeddedFile(string path)
    {
        return typeof(migrate_data_to_cms.Program).Assembly.GetManifestResourceStream(path);
    }
}