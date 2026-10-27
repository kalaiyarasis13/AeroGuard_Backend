namespace AeroGuard.Services.Implementation
{
    public class ImageStorageService
    {
        private readonly string _uploadPath;

        public ImageStorageService(IWebHostEnvironment env)
        {
            _uploadPath = Path.GetFullPath(
                Path.Combine(
                    env.ContentRootPath,
                    "..", "..", "storage", "uploads"));

            Directory.CreateDirectory(_uploadPath);
        }

        public async Task<(string Path, long Size)> SaveAsync(
            IFormFile file)
        {
            if (file.Length == 0 ||
                file.Length > 10 * 1024 * 1024)
            {
                throw new ArgumentException(
                    "Image must be between 1 byte and 10 MB.");
            }

            string extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png")
            {
                throw new ArgumentException(
                    "Only JPG and PNG images are allowed.");
            }

            // Basic file signature validation for the local MVP.
            await using var input = file.OpenReadStream();

            byte[] header = new byte[8];

            int read = await input.ReadAsync(
                header.AsMemory(0, header.Length));

            bool isJpeg = read >= 3 &&
                header[0] == 0xFF &&
                header[1] == 0xD8 &&
                header[2] == 0xFF;

            bool isPng = read >= 8 &&
                header.AsSpan(0, 8).SequenceEqual(
                    new byte[] {
                    137, 80, 78, 71, 13, 10, 26, 10
                    });

            if ((extension == ".png" && !isPng) ||
                (extension != ".png" && !isJpeg))
            {
                throw new ArgumentException(
                    "File content does not match the image type.");
            }

            string uniqueName =
                $"{Guid.NewGuid():N}{extension}";

            string fullPath =
                Path.Combine(_uploadPath, uniqueName);

            input.Position = 0;

            await using var output = new FileStream(
                fullPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);

            await input.CopyToAsync(output);

            return (fullPath, file.Length);
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
