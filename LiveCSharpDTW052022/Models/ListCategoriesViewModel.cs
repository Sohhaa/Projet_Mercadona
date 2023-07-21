using Mercadona.Repository.Categorie;

using System;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class ListCategoriesViewModel
    {
        //public int PerPage { get; set; }
        //public int NbPage { get; set; }
        //public int NbLinksTotalBdd { get; set; }
       // public int NbPageTotal
       // {
         //   get
        //    {
         //       return Convert.ToInt16(Math.Ceiling(((decimal)NbLinksTotalBdd / PerPage)));
        //    }
      //  }
        public List<CategorieModel> LstCategories { get; set; }

 
    }
}
