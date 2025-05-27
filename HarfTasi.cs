using System;
using System.Collections.Generic;
using System.Text;

public class HarfTasi
{
    public char Karakter = '\0';
    public int Puan = 0;

    public string Yazdir() { return $"[{Karakter}({Puan})]"; }
    public override string ToString() { return Yazdir(); }
}