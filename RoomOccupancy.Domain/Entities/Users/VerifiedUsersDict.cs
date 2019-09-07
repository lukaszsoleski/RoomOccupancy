using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Users
{
    /// <summary>
    /// The user can only be verified after confirming his e-mail address by clicking on the activation link in the received message.
    /// This table contains predefined users who can be verified automatically because,
    /// for example, their emails are included in the list of university students.
    /// 
    /// Użytkownik może zostać zweryfikowany dopiero po potwierdzeniu swojego adresu e-maila przez kliknięcie linku aktywacyjnego w otrzymanej wiadomości. Ta tabela zawiera predefiniowanych użytkowników, których można zweryfikować automatycznie, ponieważ na przykład między innymi ich emaile znajdują się na liście studentów uczelni.
    /// </summary>
    public class VerifiedUsersDict : IEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }
    }
}
