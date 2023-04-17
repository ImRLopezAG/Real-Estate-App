using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class ImageService : GenericService<ImageVm, SaveImageVm, Image>, IImageService {
  private readonly IImageRepository _imageRepository;
  private readonly IMapper _mapper;
  public ImageService(IImageRepository repository, IMapper mapper) : base(repository, mapper) {
    _imageRepository = repository;
    _mapper = mapper;
  }
}
