using MailKit;
using MimeKit;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        string name = "Иванов Иван Иванович";
        string address = "г. Солигорск, ул. Заморская, д.165";
        string email = "ivenov@mail.ru";
        string appealContaining = "Лайк за продукцию.";
        string reciever = "Генеральному директору БрестМясоМолПрома";

        MimeMessage message = new();

        // Sender
        message.From.Add(new MailboxAddress("Моя компания", ConfigurationManager.AppSettings["sender"]));

        // Reciever
        message.To.Add(new MailboxAddress(name, ConfigurationManager.AppSettings["reciever"]));

        // Appeal
        message.Body = new BodyBuilder()
        {
            TextBody = $"Отправитель: {name}\n" +
                       $"Адрес: {address}\n" +
                       $"Email: {email}\n\n" +
                       $"Получатель: {reciever}\n\n" +
                       $"Текст обращения:\n{appealContaining}"
        }.ToMessageBody();

        // MessageTheme
        message.Subject = $"Электронное обращение от {name}";

        // IDisposable
        using MailKit.Net.Smtp.SmtpClient client = new();

        client.Connect(ConfigurationManager.AppSettings["host"], 587, true); //либо использум порт 465
        client.Authenticate(ConfigurationManager.AppSettings["sender"], ConfigurationManager.AppSettings["password"]); //логин-пароль от аккаунта
        client.Send(message);

        client.Disconnect(true);
    }
}