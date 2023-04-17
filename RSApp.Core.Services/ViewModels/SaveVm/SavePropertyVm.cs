using Microsoft.AspNetCore.Http;
using RSApp.Core.Services.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RSApp.Core.Services.ViewModels.SaveVm;

public class SavePropertyVm : BaseVm {
  public string? Code { get; set; } = null!;
  [Required(ErrorMessage = "The Price is required")]
  public double Price { get; set; }
  [Required(ErrorMessage = "The Area is required")]
  public double Area { get; set; }
  [Required(ErrorMessage = "The Rooms is required")]
  public int Rooms { get; set; }
  [Required(ErrorMessage = "The Bathrooms is required")]
  public int Bathrooms { get; set; }
  [Required(ErrorMessage = "The Description is required")]
  public string Description { get; set; } = null!;
  public string? Agent { get; set; } = null!;
  [Required(ErrorMessage = "The Type is required")]
  public int TypeId { get; set; }
  [Required(ErrorMessage = "The Sale is required")]
  public int SaleId { get; set; }
  [DataType(DataType.Upload)]
  public ICollection<IFormFile>? ImageFiles { get; set; } = null!;
  [DataType(DataType.Upload)]
  public IFormFile? ImageFile { get; set; } = null!;
  public string? Portrait { get; set; } = null!;
  public IEnumerable<PropTypeVm>? Types { get; set; } = null!;
  public IEnumerable<SaleVm>? Sales { get; set; } = null!;
}
