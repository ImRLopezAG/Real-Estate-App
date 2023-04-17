using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSApp.Core.Domain.Settings;
using RSApp.Core.Services.Contracts;
using RSApp.Infrastructure.Shared.Services;

namespace RSApp.Infrastructure.Shared;
public static class ServiceRegistration {
  public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration) {
    services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
    services.AddTransient<IEmailService, EmailService>();
  }
}