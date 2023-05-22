using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace GestionTienda.Services
{
    public class EmailService
    {
        private readonly SmtpClient Cliente;
        public EmailService(SmtpClient smtpClient)
        {
            
            Cliente = smtpClient;
        }

        public void SendOrderStatusNotification(string emailAddress, string orderNumber, string newStatus)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587); // Reemplaza con la información de tu servidor SMTP
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("monterrey-luis@hotmail.com", "rayado");


            MailMessage message = new MailMessage();
            message.From = new MailAddress("monterrey-luis@hotmail.com");
            message.To.Add(new MailAddress("ricardo.rocham@uanl.edu.mx"));
            message.Subject = "Actualización de estado del pedido";
            message.Body = $"El estado de su pedido {orderNumber} ha cambiado a {newStatus}.";

            client.Send(message);
        }

    }
    public class OrderController : ApiController
    {
        private readonly EmailService _emailService;

        public OrderController(EmailService emailService)
        {
            _emailService = emailService;
        }

        // Método para cambiar el estado del pedido
        public IHttpActionResult UpdateOrderStatus(int orderId, string newStatus)
        {
            // Lógica para actualizar el estado del pedido en la base de datos

            // Obtener la dirección de correo electrónico del usuario asociado al pedido
            string emailAddress = GetCustomerEmailAddress(orderId);

            // Enviar notificación por correo electrónico al usuario
            _emailService.SendOrderStatusNotification(emailAddress, orderId.ToString(), newStatus);

            return Ok();
        }
        public string GetCustomerEmailAddress(int orderId)
        {
            // Lógica para obtener la dirección de correo electrónico del usuario asociado al pedido
            // Aquí puedes consultar la base de datos o cualquier otro medio para obtener la dirección de correo electrónico

            return "ricardo.rocham@uanl.edu.mx";
        }

    }
}
