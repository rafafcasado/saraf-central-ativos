using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CentralAtivos.Helpers
{
    public class Email
    {
        public Email(string destinatario, string titulo, string corpo, bool isHtml, string remetenteApelido, string destinatarioApelido)
        {
            Remetente = System.Configuration.ConfigurationManager.AppSettings["email"];
            Porta = int.Parse(System.Configuration.ConfigurationManager.AppSettings["portaEmail"]);
            Senha = System.Configuration.ConfigurationManager.AppSettings["senhaEmail"];
            Smtp = System.Configuration.ConfigurationManager.AppSettings["smtpEmail"];

            Destinatario = destinatario;
            Titulo = titulo;
            Corpo = corpo;
            IsHml = isHtml;

            RemetenteApelido = remetenteApelido;
            DestinatarioApelido = destinatarioApelido;
        }

        protected string Smtp { get; set; }
        protected string Remetente { get; set; }
        protected string RemetenteApelido { get; set; }
        protected int Porta { get; set; }
        protected string Senha { get; set; }
        protected string Destinatario { get; set; }
        protected string DestinatarioApelido { get; set; }
        protected string Titulo { get; set; }
        protected string Corpo { get; set; }
        protected bool IsHml { get; set; }

        public bool Enviar()
        {
            try
            {
                string[] emails = Destinatario.Split(';');

                foreach (string emailDestino in emails)
                {
                    MailMessage mail = new MailMessage(new MailAddress(Remetente, RemetenteApelido), new MailAddress(emailDestino, DestinatarioApelido));

                    SmtpClient client = new SmtpClient();
                    client.Host = Smtp;
                    client.Port = Porta;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(Remetente, Senha);

                    mail.Subject = Titulo;
                    mail.Body = Corpo;
                    mail.IsBodyHtml = IsHml;

                    client.Send(mail);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
