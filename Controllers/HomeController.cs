using Microsoft.AspNetCore.Mvc;
using Film_Sitesi_MVC.Data_Structures;
using Film_Sitesi_MVC.Models;



namespace Film_Sitesi_MVC.Controllers
{
   
    public class HomeController : Controller
    {
        private  static FilmBST Film_Agacı;
        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
            if (Film_Agacı == null)
            {
                Film_Agacı = new FilmBST();
                
                    Verileri_Yükle();
            }
           
        }
        public void Verileri_Yükle()
        {
            string dosyayolu = Path.Combine(_env.ContentRootPath, "Data", "Films.txt");
            if (System.IO.File.Exists(dosyayolu))
            {
                string[] satırlar = System.IO.File.ReadAllLines(dosyayolu);
            
            foreach(var satır in satırlar)
                {
                    if (string.IsNullOrWhiteSpace(satır))continue;

                    string[] parcalar = satır.Split('|');
                    if (parcalar.Length >= 4)
                    {
                        Film Yeni_Film = new Film
                        {
                            Ad = parcalar[0].Trim(),
                            Yil = int.Parse(parcalar[1].Trim()),
                            puan = double.Parse(parcalar[2].Trim()),
                            Tur = parcalar[3].Trim()



                        };
                        Film_Agacı.Ekle(Yeni_Film);
                        

                    }
                }
           
            
            
            }
        }
        public IActionResult Index()
        {
            var Siraliliste = Film_Agacı.SıralıListeyiGetir();

            return View(Siraliliste);
        }
        [HttpPost]
        public IActionResult Index(string aranankelime)
        {
            if (string.IsNullOrEmpty(aranankelime))
            {
                return View(Film_Agacı.SıralıListeyiGetir());
            }
            var BulunanFilm = Film_Agacı.ara(aranankelime);

            List<Film> Sonuç_Listesi = new List<Film>();

            if (BulunanFilm != null)
            {
                Sonuç_Listesi.Add(BulunanFilm);
            }
            else
            {
                ViewBag.Mesaj = "Aradığını film bulunamadı";

            }
            return View(Sonuç_Listesi);
        }
        [HttpPost]
        public IActionResult ekle(Film Yeni)
        {
            Film_Agacı.Ekle(Yeni);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Sil(string silinecekAd)
        {
            
            string sonucMesaji = Film_Agacı.FilmSil(silinecekAd);

            TempData["BilgiMesaji"] = sonucMesaji;

            return RedirectToAction("Index");
        }
    }

}

