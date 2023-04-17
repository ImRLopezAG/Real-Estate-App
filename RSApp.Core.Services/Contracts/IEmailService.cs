using RSApp.Core.Services.Dtos;

namespace RSApp.Core.Services.Contracts;

public interface IEmailService {
  Task SendEmail(EmailRequest request);
}
