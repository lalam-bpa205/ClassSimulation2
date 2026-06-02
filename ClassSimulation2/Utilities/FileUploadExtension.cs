namespace ClassSimulation2.Utilities
{
    public  static class FileUploadExtension
    {
        public static string SaveImage(this IFormFile imageFile,IWebHostEnvironment env,string folder)
        {
            string path = Path.Combine(env.WebRootPath, folder);
            string fileName=Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string fullPath=Path.Combine(path, fileName);

            if(Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using(FileStream stream=new FileStream(fileName, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return fileName;
        }
    }
}
