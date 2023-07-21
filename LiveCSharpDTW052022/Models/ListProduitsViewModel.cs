using Mercadona.Repository.Produits;
using System;
using System.Collections.Generic;

namespace Mercadona.Models
{
    public class ListProduitsViewModel
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
        public List<ProduitModel> LstProduits { get; set; }

        //public string Recherche { get; set; }


    }
}
