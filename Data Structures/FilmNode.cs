using Film_Sitesi_MVC.Models;

namespace Film_Sitesi_MVC.Data_Structures
{
    public class FilmNode
    {
        public Film Data {get;set;}
        public FilmNode sağ { get; set; }
        public FilmNode sol { get; set; }

        public FilmNode ( Film film)
        {
            this.Data = film;
            this.sağ = null;
            this.sol = null;
        }
    }
}
