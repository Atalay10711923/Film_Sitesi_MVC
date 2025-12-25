using Film_Sitesi_MVC.Models;
using System.Security.Cryptography.X509Certificates;

namespace Film_Sitesi_MVC.Data_Structures
{
    public class FilmBST
    {
        public List<Film> SıralıListeyiGetir()
        {
            List<Film> Film_Liste = new List<Film>();
            Dolaş(kök,Film_Liste);
            return Film_Liste;
        }
        private void Dolaş(FilmNode mevcut, List<Film> Film_Liste)
        {
            if (mevcut != null)
            {
                Dolaş(mevcut.sol, Film_Liste);

                Film_Liste.Add(mevcut.Data);

                Dolaş(mevcut.sağ, Film_Liste);
            }
        }
        public FilmNode kök { get; set; }
        public FilmBST()
        {
            kök = null;
        }
        public Film ara(string aranacak_isim)
        {


            return DugumAra(kök, aranacak_isim);
        }
        private Film DugumAra(FilmNode mevcut, string aranacak_isim)
        {
            if (mevcut == null) return null;

            int karsilastirma = string.Compare(mevcut.Data.Ad, aranacak_isim, StringComparison.OrdinalIgnoreCase);

            if (karsilastirma == 0)
            {
                return mevcut.Data;
            }
            else if (karsilastirma < 0)
            {
                return DugumAra(mevcut.sağ, aranacak_isim);
            }
            else
            {
                return DugumAra(mevcut.sol, aranacak_isim);
            }



        }
        public string FilmSil(string silinecek_isim)
        {
            
            Film bulunan = DugumAra(kök, silinecek_isim);
            if (bulunan == null)
            {
                return "Aradığınız eleman listede bulunmamaktadır.";
            }

           
            kök = SilmeIslemi(kök, silinecek_isim);

            return "Silinmesi istenen eleman listeden başarıyla çıkarılmıştır.";
        }
        private FilmNode SilmeIslemi(FilmNode kok, string silinecek_isim)
        {
            
            if (kok == null) return kok;

            int karsilastirma = string.Compare(silinecek_isim, kok.Data.Ad, StringComparison.CurrentCultureIgnoreCase);

            if (karsilastirma < 0)
            {
                kok.sol = SilmeIslemi(kok.sol, silinecek_isim);
            }
            else if (karsilastirma > 0)
            {
                kok.sağ = SilmeIslemi(kok.sağ, silinecek_isim);
            }
            else
            {
               
                if (kok.sol == null)
                    return kok.sağ;
                else if (kok.sağ == null)
                    return kok.sol;

                
                kok.Data = MinDegeriBul(kok.sağ);

                kok.sağ = SilmeIslemi(kok.sağ, kok.Data.Ad);
            }

            return kok;
        }

       
        private Film MinDegeriBul(FilmNode node)
        {
            Film minv = node.Data;
            while (node.sol != null)
            {
                minv = node.sol.Data;
                node = node.sol;
            }
            return minv;
        }
        public string Film_ekle(FilmNode mevcut , Film Eklenen)
        {
            
            int karsilastirma = string.Compare(mevcut.Data.Ad, Eklenen.Ad, StringComparison.OrdinalIgnoreCase);

            if (karsilastirma == 0)
            {
                return "Eklemek istediğiniz film halihazırda listede bulunuyor";
            }
            else if (karsilastirma < 0)
            {
                if (mevcut.sağ == null)
                {
                    mevcut.sağ = new FilmNode(Eklenen);
                    return "işlem başarıyla gerçekleşti";
                }
                else
                {
                    return Film_ekle(mevcut.sağ, Eklenen);
                }
                }
            else
            {

                if (mevcut.sol == null)
                {
                    mevcut.sol = new FilmNode(Eklenen);
                    return "işlem başarıyla gerçekleşti";
                }
                else
                {
                    return Film_ekle(mevcut.sol, Eklenen);
                }
            }




        }


        
        
        public void Ekle(Film Yenifilm)
        {
            FilmNode YeniDüğüm = new FilmNode(Yenifilm);
            if (kök == null)
            {
                kök = YeniDüğüm;
            }
            else
            {
                DugumeEkle(kök, YeniDüğüm);
            }
        }
        public void DugumeEkle(FilmNode mevcut,FilmNode Yeni)
        {
            int karsilastirma = string.Compare(mevcut.Data.Ad, Yeni.Data.Ad,StringComparison.OrdinalIgnoreCase);

            if (karsilastirma < 0)
            {
                if (mevcut.sağ == null) mevcut.sağ = Yeni;
                else
                {
                    DugumeEkle(mevcut.sağ, Yeni);
                }
           
            }
            else
            {
                if (mevcut.sol == null) mevcut.sol = Yeni;
                else
                {
                    DugumeEkle(mevcut.sol, Yeni);
                }

            }
        }



    }
}
