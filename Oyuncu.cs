using System;
using System.Collections.Generic;
using System.Text;

public class Oyuncu
{
    public string Ad = "";
    public int Skor = 0;
    public List<HarfTasi> Eli = new List<HarfTasi>();

    public void BilgileriniGir(string ad) { this.Ad = ad; }
    public void TasEkle(HarfTasi tas) { if (tas != null) Eli.Add(tas); }

    public bool TasCikar(char karakter)
    {
        for (int i = 0; i < Eli.Count; i++)
        {
            if (Eli[i].Karakter == karakter)
            {
                Eli.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool GirilenHarflereSahipMi(List<char> istenenKarakterler)
    {
        List<char> geciciElKarakterleri = new List<char>();
        for (int i = 0; i < Eli.Count; i++) geciciElKarakterleri.Add(Eli[i].Karakter);

        for (int i = 0; i < istenenKarakterler.Count; i++)
        {
            char istenen = istenenKarakterler[i];
            if (geciciElKarakterleri.Contains(istenen))
            {
                geciciElKarakterleri.Remove(istenen);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public bool TasiBittiMi() { return Eli.Count == 0; }
    public void TaslariniYaz() { for (int i = 0; i < Eli.Count; i++) Console.Write(Eli[i].Yazdir() + " "); }
    public void PuaniniGuncelle(int ekPuan) { Skor += ekPuan; }
}