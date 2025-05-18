namespace BackEnd.Helpers.Interfaces
{
    public interface IMailHelper
    {
        Task<bool> SendVerificationCodeAsync(string email, string nombre, int codigoVerificacion);
        Task<bool> SendVerificationSuccessAsync(string email, string nombre);
        Task<bool> SendPasswordChangeNotificationAsync(string email, string nombre);
        Task<bool> SendPasswordResetAsync(string email, string nombre, string nuevaContrasena);
    }
}