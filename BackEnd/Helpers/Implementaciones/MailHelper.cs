using BackEnd.Helpers.Interfaces;
using System.Net;
using System.Net.Mail;

namespace BackEnd.Helpers.Implementaciones
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _emailFrom;
        private readonly string _emailPassword;
        private readonly bool _enableSsl;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
            _emailFrom = _configuration["EmailSettings:EmailFrom"] ?? "";
            _emailPassword = _configuration["EmailSettings:EmailPassword"] ?? "";
            _enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"] ?? "true");
        }

        public async Task<bool> SendVerificationCodeAsync(string email, string nombre, int codigoVerificacion)
        {
            try
            {
                var subject = "Código de Verificación - Sistema de Matrícula";
                var body = $@"
                    <html>
                    <body>
                        <h2>Verificación de Cuenta</h2>
                        <p>Hola {nombre},</p>
                        <p>Tu código de verificación es: <strong style='font-size: 18px; color: #007BFF;'>{codigoVerificacion}</strong></p>
                        <p>Por favor, ingresa este código en el sistema para activar tu cuenta.</p>
                        <p>Este código expira en 24 horas.</p>
                        <br>
                        <p>Si no solicitaste esta verificación, puedes ignorar este correo.</p>
                        <p>Saludos,<br>Sistema de Matrícula</p>
                    </body>
                    </html>";

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando código de verificación: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendVerificationSuccessAsync(string email, string nombre)
        {
            try
            {
                var subject = "Cuenta Verificada Exitosamente - Sistema de Matrícula";
                var body = $@"
                    <html>
                    <body>
                        <h2>¡Cuenta Verificada!</h2>
                        <p>Hola {nombre},</p>
                        <p>Tu cuenta ha sido verificada exitosamente y ya está activa.</p>
                        <p>Ahora puedes iniciar sesión en el sistema con tus credenciales.</p>
                        <p>Fecha y hora de verificación: {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                        <br>
                        <p>¡Bienvenido al Sistema de Matrícula!</p>
                        <p>Saludos,<br>Sistema de Matrícula</p>
                    </body>
                    </html>";

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando confirmación de verificación: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendPasswordChangeNotificationAsync(string email, string nombre)
        {
            try
            {
                var subject = "Contraseña Cambiada - Sistema de Matrícula";
                var body = $@"
                    <html>
                    <body>
                        <h2>Contraseña Actualizada</h2>
                        <p>Hola {nombre},</p>
                        <p>Te confirmamos que tu contraseña ha sido cambiada exitosamente.</p>
                        <p>Fecha y hora: {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                        <br>
                        <p>Si no realizaste este cambio, ponte en contacto con el administrador inmediatamente.</p>
                        <p>Saludos,<br>Sistema de Matrícula</p>
                    </body>
                    </html>";

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando notificación de cambio de contraseña: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendPasswordResetAsync(string email, string nombre, string nuevaContrasena)
        {
            try
            {
                var subject = "Restablecimiento de Contraseña - Sistema de Matrícula";
                var body = $@"
                    <html>
                    <body>
                        <h2>Contraseña Restablecida</h2>
                        <p>Hola {nombre},</p>
                        <p>Tu contraseña ha sido restablecida exitosamente.</p>
                        <p>Tu nueva contraseña temporal es: <strong style='font-size: 16px; color: #007BFF;'>{nuevaContrasena}</strong></p>
                        <p><strong>Por seguridad, te recomendamos cambiar esta contraseña después de iniciar sesión.</strong></p>
                        <br>
                        <p>Si no solicitaste este restablecimiento, ponte en contacto con el administrador inmediatamente.</p>
                        <p>Saludos,<br>Sistema de Matrícula</p>
                    </body>
                    </html>";

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando contraseña restablecida: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                if (string.IsNullOrEmpty(_emailFrom) || string.IsNullOrEmpty(_emailPassword))
                {
                    Console.WriteLine("Configuración de correo no encontrada");
                    return false;
                }

                using var client = new SmtpClient(_smtpServer, _smtpPort);
                client.EnableSsl = _enableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailFrom, "Sistema de Matrícula"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                return false;
            }
        }
    }
}