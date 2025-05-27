using System;
using System.Collections.Generic;
using System.Text;

public class Oyun
{
    private Tahta tahta = new Tahta();
    private Torba torba = new Torba();
    private Sozluk sozluk = new Sozluk();
    private Oyuncu[] oyuncular = { new Oyuncu(), new Oyuncu() };
    private int siradakiOyuncuIndex = 0;
    private bool ilkHamle = true;

    public void Baslat()
    {
        Console.WriteLine("--- Oyun Başlıyor ---");
        tahta.Olustur();
        torba.Doldur();
        torba.Karistir();
        sozluk.Olustur();
        OyuncuBilgileriniAl();
        OyuncularaTasDagit(7);
        Oynat();
        SkorlariYaz(true);
    }

    private void OyuncuBilgileriniAl()
    {
        Console.Write("1. Oyuncu Adı: ");
        oyuncular[0].BilgileriniGir(Console.ReadLine());
        Console.Write("2. Oyuncu Adı: ");
        oyuncular[1].BilgileriniGir(Console.ReadLine());
    }

    private void OyuncularaTasDagit(int adet)
    {
        for (int i = 0; i < oyuncular.Length; i++)
        {
            for (int j = 0; j < adet; j++)
            {
                oyuncular[i].TasEkle(torba.HarfCek());
            }
        }
    }

    private void Oynat()
    {
        bool oyunDevam = true;
        while (oyunDevam)
        {
            Console.Clear();
            tahta.Ciz();
            Oyuncu mevcutOyuncu = oyuncular[siradakiOyuncuIndex];

            Console.WriteLine("\n--- Oyuncu Bilgileri ve Elleri ---");
            for (int i = 0; i < oyuncular.Length; i++) OyuncularinTaslariniGoster(oyuncular[i]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n>>> SIRA SİZDE: {mevcutOyuncu.Ad} <<<");
            Console.ResetColor();

            Console.WriteLine($"Torbada Kalan Taş: {torba.KalanTasSayisi()}");
            Console.Write($"\n{mevcutOyuncu.Ad}, Hamleniz (KELIME SATIR SÜTUN YÖN): ");
            string giris = Console.ReadLine().ToUpperInvariant();
            string kelime;
            int satir, sutun;
            char yon;

            if (!GirisParseEtVeTemelKontrol(giris, out kelime, out satir, out sutun, out yon))
            {
                SiraDegistir();
                continue;
            }

            if (!sozluk.KelimeVarMi(kelime))
            {
                Console.WriteLine($"'{kelime}' sözlükte yok! (Enter)");
                Console.ReadLine();
                SiraDegistir();
                continue;
            }

            List<char> kullanilanKarakterler;
            string gecerlilikSonucu = tahta.GecerlilikKontrol(kelime, satir, sutun, yon, mevcutOyuncu, ilkHamle, out kullanilanKarakterler);

            if (gecerlilikSonucu == "OK")
            {
                HarfTasi[] yerlestirilecekTaslar = new HarfTasi[kullanilanKarakterler.Count];
                for (int i = 0; i < kullanilanKarakterler.Count; i++)
                {
                    yerlestirilecekTaslar[i] = new HarfTasi();
                    yerlestirilecekTaslar[i].Karakter = kullanilanKarakterler[i];
                    yerlestirilecekTaslar[i].Puan = Torba.HarfPuaniniBul(kullanilanKarakterler[i]);
                }

                List<Koordinat> yeniHarfKoordinatlari = tahta.KelimeYerlestir(kelime, satir, sutun, yon, yerlestirilecekTaslar);
                int anaKelimePuani = tahta.KelimePuanla(kelime, satir, sutun, yon, true, yeniHarfKoordinatlari);
                mevcutOyuncu.PuaniniGuncelle(anaKelimePuani);
                Console.WriteLine($"'{kelime}' için {anaKelimePuani} puan.");

                for (int i = 0; i < kullanilanKarakterler.Count; i++) mevcutOyuncu.TasCikar(kullanilanKarakterler[i]);
                for (int i = 0; i < kullanilanKarakterler.Count; i++) mevcutOyuncu.TasEkle(torba.HarfCek());

                ilkHamle = false;
                if (OyunBittiMi()) oyunDevam = false;
                else
                {
                    Console.WriteLine("Devam etmek için Enter...");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"Geçersiz Hamle: {gecerlilikSonucu} (Enter)");
                Console.ReadLine();
            }

            if (oyunDevam) SiraDegistir();
        }
    }

    private bool GirisParseEtVeTemelKontrol(string g, out string k, out int sa, out int su, out char y)
    {
        k = ""; sa = -1; su = -1; y = ' ';
        string[] p = g.Split(' ');
        if (p.Length != 4)
        {
            Console.WriteLine("Hatalı giriş! (KELIME SATIR SÜTUN YÖN(Y/D) formatında girin) (Enter)");
            Console.ReadLine();
            return false;
        }
        k = p[0];
        try
        {
            sa = int.Parse(p[1]);
            su = int.Parse(p[2]);
            y = p[3][0];
        }
        catch
        {
            Console.WriteLine("Satır, sütun veya yön hatalı! (Enter)");
            Console.ReadLine();
            return false;
        }
        if ((y != 'Y' && y != 'D') || k.Length == 0)
        {
            Console.WriteLine("Geçersiz kelime veya yön! (Yön: Y veya D) (Enter)");
            Console.ReadLine();
            return false;
        }
        return true;
    }

    private bool OyunBittiMi()
    {
        if (torba.BosMu())
        {
            for (int i = 0; i < oyuncular.Length; i++)
            {
                if (oyuncular[i].TasiBittiMi())
                {
                    Console.WriteLine($"\nTorba boş ve {oyuncular[i].Ad} adlı oyuncunun taşı bitti. Oyun bitti!");
                    return true;
                }
            }
        }
        return false;
    }

    private void OyuncularinTaslariniGoster(Oyuncu o)
    {
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.Write($"Oyuncu: {o.Ad} | Puan: {o.Skor} | El: ");
        o.TaslariniYaz();
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------------");
    }

    private void SkorlariYaz(bool oyunSonu = false)
    {
        Console.Clear();
        tahta.Ciz();
        Console.WriteLine(oyunSonu ? "\nOYUN BİTTİ" : "\nSKORLAR");
        for (int i = 0; i < oyuncular.Length; i++)
        {
            Console.WriteLine($"{oyuncular[i].Ad}: {oyuncular[i].Skor} Puan");
        }
        Console.WriteLine("============");
        if (oyunSonu)
        {
            Oyuncu kazanan = null;
            if (oyuncular[0].Skor > oyuncular[1].Skor) kazanan = oyuncular[0];
            else if (oyuncular[1].Skor > oyuncular[0].Skor) kazanan = oyuncular[1];

            if (kazanan != null) Console.WriteLine($"KAZANAN: {kazanan.Ad}");
            else Console.WriteLine("BERABERE!");
            Console.WriteLine("============");
            Console.WriteLine("\nÇıkmak için Enter...");
            Console.ReadLine();
        }
    }

    private void SiraDegistir()
    {
        siradakiOyuncuIndex = (siradakiOyuncuIndex + 1) % oyuncular.Length;
    }
}