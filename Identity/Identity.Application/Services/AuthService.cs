using Identity.Application.Contracts;
using Identity.Application.Contracts.Repositories;
using Identity.Application.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private IBrockerMessage _brockerMessage;
        public AuthService(IUserRepository userRepository, IBrockerMessage brockerMessage)
        {
            _userRepository = userRepository;
            _brockerMessage = brockerMessage;
        }

        public async Task<bool> CreateUser(UsuarioPersonalizado usuario)
        {
            var result = await _userRepository.AddUser(usuario);
            if (result)
            {
                int codeForSend = await _userRepository.AddVerificationCode(usuario);
                if (codeForSend != -1)
                {
                    EmailToSend emailToSend = new EmailToSend();
                    emailToSend.From = "";
                    emailToSend.To = usuario.Email;
                    emailToSend.Subject = "TE REGISTRASTE A UPDS";
                    emailToSend.Body = $"Bienvenido {usuario.Nombre} a nuestra sitio UPDS, este es tu codigo {codeForSend} para validar tu cuenta ";
                    await _brockerMessage.Produce(emailToSend);
                }
                return true;
            }
            else { return false; }
        }
    }
}
