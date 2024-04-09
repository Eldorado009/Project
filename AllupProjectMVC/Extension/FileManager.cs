namespace AllupProjectMVC.Extension;

public static class FileManager
{
    public static string SaveFile(this IFormFile file, string rootPath, string folderName)
    {
        string fileName = file.FileName;
        fileName = fileName.Length > 64 ? fileName.Substring(fileName.Length - 64, 64) : fileName;
        fileName = Guid.NewGuid().ToString() + fileName; // 100

        string path = Path.Combine(rootPath, folderName, fileName);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return fileName;
    }

    public static void DeleteFile(string rootPath, string folderName, string fileName)
    {
        string deletePath = Path.Combine(rootPath, folderName, fileName);

        if (System.IO.File.Exists(deletePath))
        {
            System.IO.File.Delete(deletePath);
        }
    }

    public static string GetFilePath(this IWebHostEnvironment env, string folder, string fileName)
    {
        return Path.Combine(env.WebRootPath, folder, fileName);
    }

    public static bool CheckFileType(this IFormFile file, string pattern)
    {
        return file.ContentType.Contains(pattern);
    }

    public static bool CheckFilesize(this IFormFile file, long size)
    {
        return file.Length / 1024 < size;
    }

    public async static Task SaveFileAsync(this IFormFile file, string path)
    {
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }
}
