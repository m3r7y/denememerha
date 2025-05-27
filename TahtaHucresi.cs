using System;
using System.Collections.Generic;
using System.Text;

public class TahtaHucresi
{
    public HarfTasi Tas = null;
    public string Bonus = null;

    public bool GetDoluMu() { return Tas != null; }

    public int GetHarfCarpani()
    {
        if (Bonus == "H2") return 2;
        if (Bonus == "H3") return 3;
        return 1;
    }

    public int GetKelimeCarpani()
    {
        if (Bonus == "K2") return 2;
        if (Bonus == "K3") return 2;
        return 1;
    }

    public void KullanBonus()
    {
        this.Bonus = null;
    }
}