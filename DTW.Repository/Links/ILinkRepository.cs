﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mercadona.Repository.Links
{
    public interface ILinkRepository
    {
        public List<LinkModel> GetAllLinks();
        public LinkModel GetLink(int id);
        public bool EditLink(LinkModel link);
        public bool DeleteLink(int id);
    }
}
