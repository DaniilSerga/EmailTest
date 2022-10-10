using System.Net;
using System.Net.Mail;
using System.Net.Mime;

string name = "Иванов Иван Иванович";
string address = "г. Солигорск, ул. Заморская, д.165";
string email = "ivenov@mail.ru";
string appealContaining = "Лайк за продукцию.";
string reciever = "Генеральному директору БрестМясоМолПрома";

await Task.Run(() => SendEmail(name, email, address, appealContaining, reciever));

// TODO Implement Additional files sending
static async Task SendEmail(string userName, string email, string address, 
    string appealContaining, string reciever)
{
    MailAddress from = new("bmmp-appeals@rambler.ru", "Обращение БрестМясоМолПром");
    MailAddress to = new("daniil.serga@gmail.com");

    MailMessage msg = new(from, to)
    {
        Subject = $"Новое обращение",
        Body = $"Отправитель: {userName}\n" +
        $"Адрес: {address}\n" +
        $"Email: {email}\n\n" +
        $"Получатель: {reciever}\n\n" +
        $"Текст обращения:\n{appealContaining}"
    };

    #region Attachment
    string file = "D:\\ВУЗик\\Daniil Serga71.pdf";
    //MemoryStream attachment = new(File.ReadAllBytes(file));
    // Create  the file attachment for this email message.
    Attachment data = new(file);

    // Add time stamp information for the file.
    ContentDisposition disposition = data.ContentDisposition!;
    disposition.CreationDate = File.GetCreationTime(file);
    disposition.ModificationDate = File.GetLastWriteTime(file);
    disposition.ReadDate = File.GetLastAccessTime(file);
    // Add the file attachment to this email message.
    msg.Attachments.Add(data);
    #endregion

    SmtpClient smtp = new()
    {
        Host = "smtp.rambler.ru",
        Port = 587,
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(from.Address, "z2x-JwT-Q2K-WaL")
    };

    await smtp.SendMailAsync(msg);
}
