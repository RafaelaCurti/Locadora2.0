using Locadora.Domain;

namespace Locadora.Web.Areas.ViewModels
{
    public class ClientSearchViewModel
    {

        public string nome { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public int? preferencia { get; set; }
        public int? id_usuario { get; set; } //UTILIZADO EXCLUSIVAMENTE NA INTEGRAÇÃO COM FOCCO
        public int? pagina { get; set; }
        public int quantidade { get; set; }

        public ClientSearchViewModel()
        {

        }

        public ClientSearch ConvertToClientSearch()
        {
            return new ClientSearch()
            {
                name = this.nome,
                page = this.pagina ?? 1,
                username = this.login,
                preference = this.preferencia,
                email = this.email,
                phone = this.telefone,
                take = this.quantidade == 0 ? 10 : this.quantidade
            };
        }
    }
}