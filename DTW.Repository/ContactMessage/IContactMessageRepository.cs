using Mercadona.Repository.ContactMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.ContactMessage
{
    public interface IContactMessageRepository
    {
        public bool SaveContactMessage(ContactMessageModel ContactMessage);
    }
}

