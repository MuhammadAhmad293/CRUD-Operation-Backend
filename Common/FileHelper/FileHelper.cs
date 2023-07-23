namespace Common.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public string SaveFile(string base64String, string fileName,string path)
        {
            string[] splittedCoverPhoto = base64String.Split(',', StringSplitOptions.RemoveEmptyEntries);
            string cleanBase64String = splittedCoverPhoto[1];
            string extension = splittedCoverPhoto[0].Split(new char[] { ';', '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
            byte[] byteArray = Convert.FromBase64String(cleanBase64String);
            fileName += Guid.NewGuid().ToString() + "." + extension;
            string filePath = Path.Combine(path, fileName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            File.WriteAllBytes(filePath, byteArray);
            return fileName;
        }
    }
}
