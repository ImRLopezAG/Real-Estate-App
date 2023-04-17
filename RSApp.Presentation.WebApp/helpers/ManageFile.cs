
namespace RSApp.Presentation.WebApp.helpers;

public static class ManageFile {
  public static string Upload(IFormFile file, string id, bool isEditMode = false, string imagePath = "") {
    if (isEditMode) {
      if (file == null)
        return imagePath;
    }

    string basePath = $"/Images/{id}/Profile";
    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);

    Guid guid = Guid.NewGuid();
    FileInfo fileInfo = new(file.FileName);
    string fileName = guid + fileInfo.Extension;

    string fileNameWithPath = Path.Combine(path, fileName);

    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
      file.CopyTo(stream);

    if (isEditMode) {
      string[] oldImagePart = imagePath.Split("/");
      string oldImagePath = oldImagePart[^1];
      string completeImageOldPath = Path.Combine(path, oldImagePath);

      if (File.Exists(completeImageOldPath))
        File.Delete(completeImageOldPath);
    }
    return $"{basePath}/{fileName}";
  }


  public static string UploadProperty(IFormFile file, string id,int pId, bool isEditMode = false, string imagePath = "") {
    if (isEditMode) {
      if (file == null)
        return imagePath;
    }

    string basePath = $"/Images/{id}/Properties/{pId}";
    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);

    Guid guid = Guid.NewGuid();
    FileInfo fileInfo = new(file.FileName);
    string fileName = guid + fileInfo.Extension;

    string fileNameWithPath = Path.Combine(path, fileName);

    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
      file.CopyTo(stream);

    if (isEditMode && imagePath != null) {

      string[] oldImagePart = imagePath.Split("/");
      string oldImagePath = oldImagePart[^1];
      string completeImageOldPath = Path.Combine(path, oldImagePath);

      if (File.Exists(completeImageOldPath))
        File.Delete(completeImageOldPath);

    }
    return $"{basePath}/{fileName}";
  }

  public static void DeleteProperty(string id, string imagePath) {
    string basePath = $"/Images/{id}/Properties";
    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);

    string[] oldImagePart = imagePath.Split("/");
    string oldImagePath = oldImagePart[^1];
    string completeImageOldPath = Path.Combine(path, oldImagePath);

    if (File.Exists(completeImageOldPath))
      File.Delete(completeImageOldPath);
  }

  public static string UploadPropertyImages(IFormFile file, string id, int pId, bool isEditMode = false, string imagePath = "") {
    if (isEditMode) {
      if (file == null)
        return imagePath;
    }

    string basePath = $"/Images/{id}/Properties/{pId}/Images";
    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);

    Guid guid = Guid.NewGuid();
    FileInfo fileInfo = new(file.FileName);
    string fileName = guid + fileInfo.Extension;

    string fileNameWithPath = Path.Combine(path, fileName);

    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
      file.CopyTo(stream);

    if (isEditMode && imagePath != null) {

      string[] oldImagePart = imagePath.Split("/");
      string oldImagePath = oldImagePart[^1];
      string completeImageOldPath = Path.Combine(path, oldImagePath);

      if (File.Exists(completeImageOldPath))
        File.Delete(completeImageOldPath);

    }
    return $"{basePath}/{fileName}";
  }
}
