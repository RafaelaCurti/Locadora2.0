using Locadora.Domain;

namespace Locadora.Web.Areas.ViewModels
{
    public class MovieSearchViewModel
    {

        public string filme { get; set; }
        public int? categoria { get; set; }
        public int? pagina { get; set; }
        public int quantidade { get; set; }

        public MovieSearchViewModel()
        {

        }

        public MovieSearch ConvertToMovieSearch()
        {
            return new MovieSearch()
            {
                name = this.filme,
                page = this.pagina ?? 1,
                category = this.categoria,
                take = this.quantidade == 0 ? 10 : this.quantidade
            };
        }
    }
}