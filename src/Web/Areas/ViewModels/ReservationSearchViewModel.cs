using Locadora.Domain;
using Locadora.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Locadora.Web.Areas.ViewModels
{
    public class ReservationSearchViewModel
    {

        public string cliente { get; set; }

        //não é apenas uma int, é uma lista
        public int? itens { get; set; }
        public DateTime? retirada { get; set; }
        public DateTime? devolucao { get; set; }
        public int? pagina { get; set; }
        public int quantidade { get; set; }

        // Por questão de padrão de empresa, construtor sempre após os campos 
        public ReservationSearchViewModel()
        {

        }

        public ReservationSearch ConvertToReservationSearch()
        {
            return new ReservationSearch()
            {
                client = this.cliente,
                page = this.pagina ?? 1,
                itens = this.itens,
                withdraw = this.retirada,
                devolution = this.devolucao,
                take = this.quantidade == 0 ? 10 : this.quantidade
            };
        }
    }
}