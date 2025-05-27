using System;
using System.Collections.Generic;
using System.Text;

public class Tahta
{
    private TahtaHucresi[,] hucreler;
    private const int BOYUT = 15;
    private static int[,] k3_coords = { {0,2}, {0,12}, {2,0}, {2,14}, {12,0}, {12,14}, {14,2}, {14,12} };
    private static int[,] k2_coords = { {2,7}, {3,3}, {3,11}, {7,2}, {7,12}, {11,3}, {11,11}, {12,7} };
    private static int[,] h3_coords = { {1,1}, {1,13}, {4,4}, {4,10}, {10,4}, {10,10}, {13,1}, {13,13} };
    private static int[,] h2_coords = { {0,5}, {0,9}, {1,6}, {1,8}, {5,0}, {5,5}, {5,9}, {5,14}, {6,1}, {6,6}, {6,8}, {6,13}, {8,1}, {8,6}, {8,8}, {8,13}, {9,0}, {9,5}, {9,9}, {9,14}, {13,6}, {13,8}, {14,5}, {14,9} };

    public void Olustur()
    {
        hucreler = new TahtaHucresi[BOYUT, BOYUT];
        for (int i = 0; i < BOYUT; i++)
        {
            for (int j = 0; j < BOYUT; j++)
            {
                hucreler[i, j] = new TahtaHucresi();
            }
        }
        YerlestirBonuslar(k3_coords, "K3");
        YerlestirBonuslar(k2_coords, "K2");
        YerlestirBonuslar(h3_coords, "H3");
        YerlestirBonuslar(h2_coords, "H2");
    }

    private void YerlestirBonuslar(int[,] coords, string bonusTipi)
    {
        for (int i = 0; i < coords.GetLength(0); i++)
        {
            hucreler[coords[i, 0], coords[i, 1]].Bonus = bonusTipi;
        }
    }

    public void Ciz()
    {
        Console.WriteLine();
        Console.Write("    ");
        for (int j = 0; j < BOYUT; j++) Console.Write($"{j,-4}");
        Console.WriteLine();

        Console.Write("   +");
        for (int j = 0; j < BOYUT; j++) Console.Write("---+");
        Console.WriteLine();

        for (int i = 0; i < BOYUT; i++)
        {
            Console.Write($"{i,-2} |");
            for (int j = 0; j < BOYUT; j++)
            {
                TahtaHucresi h = hucreler[i, j];
                if (h.GetDoluMu())
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write($" {h.Tas.Karakter} ");
                    Console.ResetColor();
                }
                else if (h.Bonus != null)
                {
                    if (h.Bonus == "K3") Console.ForegroundColor = ConsoleColor.Red;
                    else if (h.Bonus == "K2") Console.ForegroundColor = ConsoleColor.Magenta;
                    else if (h.Bonus == "H3") Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (h.Bonus == "H2") Console.ForegroundColor = ConsoleColor.Cyan;
                    else Console.ForegroundColor = ConsoleColor.Gray;

                    if (h.Bonus.Length == 2) Console.Write($" {h.Bonus}");
                    else if (h.Bonus.Length == 1) Console.Write($" {h.Bonus} ");
                    else Console.Write(h.Bonus.Substring(0, 3));
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("   ");
                }
                Console.Write("|");
            }
            Console.WriteLine();
            Console.Write("   +");
            for (int k = 0; k < BOYUT; k++) Console.Write("---+");
            Console.WriteLine();
        }
    }

    public string GecerlilikKontrol(string kelime, int satir, int sutun, char yon, Oyuncu oyuncu, bool ilkHamle, out List<char> eldenKullanilanKarakterler)
    {
        eldenKullanilanKarakterler = new List<char>();
        List<char> oyuncununGeciciEli = new List<char>();
        for (int k = 0; k < oyuncu.Eli.Count; k++) oyuncununGeciciEli.Add(oyuncu.Eli[k].Karakter);

        bool enAzBirYeniHarfKullanildi = false;
        bool mevcutHarfeTemasOldu = false;

        for (int i = 0; i < kelime.Length; i++)
        {
            int r_hedef = satir, c_hedef = sutun;
            if (yon == 'H') c_hedef += i; else r_hedef += i;

            if (r_hedef < 0 || r_hedef >= BOYUT || c_hedef < 0 || c_hedef >= BOYUT) return "Kelime tahtadan taşıyor.";

            char kelimedekiHarf = kelime[i];
            if (hucreler[r_hedef, c_hedef].Tas == null) // Hücre boşsa
            {
                enAzBirYeniHarfKullanildi = true;
                if (!oyuncununGeciciEli.Contains(kelimedekiHarf)) return $"'{kelimedekiHarf}' elinizde yok.";
                eldenKullanilanKarakterler.Add(kelimedekiHarf);
                oyuncununGeciciEli.Remove(kelimedekiHarf);

                // Komşu hücrelerde taş var mı diye bak (temas için)
                int[] dr = { -1, 1, 0, 0 };
                int[] dc = { 0, 0, -1, 1 };
                for (int k_dr = 0; k_dr < 4; k_dr++)
                {
                    int nr = r_hedef + dr[k_dr], nc = c_hedef + dc[k_dr];
                    if (nr >= 0 && nr < BOYUT && nc >= 0 && nc < BOYUT && hucreler[nr, nc].Tas != null) mevcutHarfeTemasOldu = true;
                }
            }
            else // Hücre doluysa
            {
                if (hucreler[r_hedef, c_hedef].Tas.Karakter != kelimedekiHarf) return $"Uyuşmazlık: ({r_hedef},{c_hedef}) '{hucreler[r_hedef, c_hedef].Tas.Karakter}', kelimede '{kelimedekiHarf}'.";
                mevcutHarfeTemasOldu = true; // Dolu hücreye denk gelirse.
            }
        }

        if (!enAzBirYeniHarfKullanildi) return "En az bir yeni harf kullanmalısınız.";
        if (!ilkHamle && !mevcutHarfeTemasOldu) return "Yeni kelime mevcut bir harfe bitisik olmalı.";
        return "OK";
    }

    public List<Koordinat> KelimeYerlestir(string kelime, int satir, int sutun, char yon, HarfTasi[] yerlestirilecekTaslar)
    {
        List<Koordinat> yeniHarfKoordinatlari = new List<Koordinat>();
        int tasIndex = 0;
        for (int i = 0; i < kelime.Length; i++)
        {
            int r = satir, c = sutun;
            if (yon == 'H') c += i; else r += i;

            if (hucreler[r, c].Tas == null) // Sadece boş hücrelere yerleştir
            {
                Koordinat yeniKoord = new Koordinat();
                yeniKoord.Satir = r;
                yeniKoord.Sutun = c;
                yeniHarfKoordinatlari.Add(yeniKoord);
                if (tasIndex < yerlestirilecekTaslar.Length)
                {
                    hucreler[r, c].Tas = yerlestirilecekTaslar[tasIndex++];
                }
            }
        }
        return yeniHarfKoordinatlari;
    }

    public int KelimePuanla(string kelime, int satir, int sutun, char yon, bool anaKelimeMi, List<Koordinat> buHamledeYeniKonanKoordinatlar)
    {
        int toplamPuan = 0;
        int genelKelimeCarpani = 1;

        for (int i = 0; i < kelime.Length; i++)
        {
            int r_harf = satir, c_harf = sutun;
            if (yon == 'H') c_harf += i; else r_harf += i;

            if (r_harf < 0 || r_harf >= BOYUT || c_harf < 0 || c_harf >= BOYUT || hucreler[r_harf, c_harf].Tas == null) continue;

            TahtaHucresi aktifHucre = hucreler[r_harf, c_harf];
            HarfTasi mevcutTas = aktifHucre.Tas;
            int harfPuani = mevcutTas.Puan;
            int anlikHarfCarpani = 1;

            bool buHarfYeniMi = false;
            for (int k = 0; k < buHamledeYeniKonanKoordinatlar.Count; k++)
            {
                if (buHamledeYeniKonanKoordinatlar[k].Satir == r_harf && buHamledeYeniKonanKoordinatlar[k].Sutun == c_harf)
                {
                    buHarfYeniMi = true;
                    break;
                }
            }

            if (buHarfYeniMi && aktifHucre.Bonus != null)
            {
                anlikHarfCarpani = aktifHucre.GetHarfCarpani();
                genelKelimeCarpani *= aktifHucre.GetKelimeCarpani();
                aktifHucre.KullanBonus();
            }
            toplamPuan += harfPuani * anlikHarfCarpani;
        }
        return toplamPuan * genelKelimeCarpani;
    }
}