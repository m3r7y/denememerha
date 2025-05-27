using System;
using System.Collections.Generic;
using System.Text;

public class Torba
{
    private List<HarfTasi> taslar = new List<HarfTasi>();
    private Random rastgele = new Random();
    public static  char[] HARF_KARAKTERLERI = { 'A', 'B', 'C', 'Ç', 'D', 'E', 'F', 'G', 'Ğ', 'H', 'I', 'İ', 'J', 'K', 'L', 'M', 'N', 'O', 'Ö', 'P', 'R', 'S', 'Ş', 'T', 'U', 'Ü', 'V', 'Y', 'Z' };
    public static  int[] HARF_ADETLERI = { 12,  2,   2,   2,   2,   8,   1,   1,   1,   1,   4,   7,   1,   7,   7,   4,   5,   3,   1,   1,   6,   3,   2,   5,   3,   2,   1,   2,   2 };
    public static  int[] HARF_PUANLARI = {  1,   3,   4,   4,   3,   1,   7,   5,   8,   5,   2,   1,  10,   1,   1,   2,   1,   2,   7,   5,   1,   2,   4,   1,   2,   3,   7,   3,   4 };

    public void Doldur()
    {
        taslar.Clear();
        for (int i = 0; i < HARF_KARAKTERLERI.Length; i++)
        {
            char karakter = HARF_KARAKTERLERI[i];
            int adet = HARF_ADETLERI[i];
            int puan = HARF_PUANLARI[i];
            for (int j = 0; j < adet; j++)
            {
                HarfTasi yeniTas = new HarfTasi();
                yeniTas.Karakter = karakter;
                yeniTas.Puan = puan;
                taslar.Add(yeniTas);
            }
        }
    }

    public void Karistir()
    {
        for (int i = 0; i < taslar.Count; i++)
        {
            int r = rastgele.Next(i, taslar.Count);
            HarfTasi temp = taslar[i];
            taslar[i] = taslar[r];
            taslar[r] = temp;
        }
    }

    public HarfTasi HarfCek()
    {
        if (!BosMu())
        {
            HarfTasi t = taslar[0];
            taslar.RemoveAt(0);
            return t;
        }
        return null;
    }

    public bool BosMu() { return taslar.Count == 0; }
    public int KalanTasSayisi() { return taslar.Count; }

    public static int HarfPuaniniBul(char karakter)
    {
        for (int i = 0; i < Torba.HARF_KARAKTERLERI.Length; i++)
        {
            if (Torba.HARF_KARAKTERLERI[i] == karakter)
            {
                return Torba.HARF_PUANLARI[i];
            }
        }
        return 0;
    }
}