using System;
using System.Collections.Generic;
using System.Text;

// --- Veri Saklama ve Basit Yapılar ---

public class Koordinat
{
    public int Satir = 0;
    public int Sutun = 0;
}

public class HarfTasi
{
    public char Karakter = '\0';
    public int Puan = 0;

    public string Yazdir() { return $"[{Karakter}({Puan})]"; }
    public override string ToString() { return Yazdir(); }
}

public class TahtaHucresi
{
    public HarfTasi Tas = null;
    public string Bonus = null;

    public bool GetDoluMu() { return Tas != null; }

    // Yeni metotlar: Bonus etkilerini ve kullanımını yönetir
    public int GetHarfCarpani()
    {
        if (Bonus == "H2") return 2;
        if (Bonus == "H3") return 3;
        return 1;
    }

    public int GetKelimeCarpani()
    {
        if (Bonus == "K2") return 2;
        if (Bonus == "K3") return 2; // Mevcut kodunuzdaki K3 = Kelime x2 mantığı korunuyor
        return 1;
    }

    public void KullanBonus()
    {
        this.Bonus = null; // Bonus kullanıldıktan sonra temizlenir
    }
}

// --- Ana Oyun Mantığı Yapıları ---

public class Sozluk
{
    private List<string> kelimeler = new List<string>();
    private static readonly string[] VARSAYILAN_SOZLUK = {
        "AÇ", "AD", "AK", "AL", "AN", "AR", "AS", "AT", "AV", "AY", "AZ", "BAĞ", "BAL", "BEN", "BEŞ", "BİN", "BİR", "BOL", "BOŞ", "BOY",
        "CAM", "CAN", "CEM", "CİN", "CİP", "ÇAM", "ÇAN", "ÇAP", "ÇAY", "ÇEK", "ÇİĞ", "ÇİM", "ÇOK", "DAĞ", "DAL", "DAM", "DAR", "DEF", "DEM", "DIŞ", "DİL", "DİN", "DİŞ", "DON", "DUL", "DUŞ", "DUY",
        "EBE", "EDA", "EFE", "EGE", "EGO", "ELİF", "EMEK", "ESKİ", "EŞEK", "ETİK", "EVET", "FARE", "FARK", "FENA", "FİİL", "FİLE", "FİLM", "FREN",
        "GAF", "GAZ", "GECE", "GENÇ", "GÖL", "GÖZ", "GÜÇ", "GÜL", "GÜN", "HAK", "HAL", "HAM", "HAN", "HARÇ", "HAS", "HAT", "HAY", "HAZ", "HECE", "HIZ", "HİÇ", "HOŞ",
        "ISLAK", "İADE", "İCAT", "İÇİN", "İĞNE", "İKAZ", "İLAH", "İLAN", "İLGİ", "İLKE", "İLİM", "İNEK", "İREM", "İSİM", "İSYAN", "İTİNA", "İYİCE",
        "JANT", "JEST", "JİLE", "JÖLE", "JÜRİ", "KAFA", "KALE", "KALP", "KARA", "KART", "KASA", "KAŞE", "KEDİ", "KEME", "KENT", "KİLO", "KİRA", "KOCA", "KOLAY", "KÖMÜR", "KÖPEK", "KÖŞE", "KREM", "KULAÇ", "KUMAR", "KURAL", "KURUŞ", "KUYTU", "KUZEN",
        "LADES", "LALE", "LİDER", "LİMAN", "LİMON", "LİSTE", "LOKMA", "LÜKS", "MAAŞ", "MACUN", "MADEN", "MAKAS", "MALTA", "MARKA", "MASAL", "MAVİ", "MAYIN", "MEKAN", "MELEK", "MENÜ", "MERAK", "METAL", "METİN", "MEYVE", "MEZAR", "MİSAFİ", "MODEL", "MODEM", "MOTOR", "MUHAL", "MÜZİK",
        "NAKIŞ", "NALAN", "NAMLU", "NAZAR", "NEDEN", "NEHİR", "NEMLİ", "NEŞELİ", "NİYET", "NÖBET", "NUMARA", "OCAK", "ODACI", "OĞLAK", "OKTAV", "OKUMA", "OLASI", "OLAY", "ONAY", "ONUR", "OPERA", "ORMAN", "ORTAK", "OTEL", "OYACI", "OYNAK", "OYSAKİ",
        "ÖBEK", "ÖDEME", "ÖDEV", "ÖFKELİ", "ÖĞLEN", "ÖKSÜZ", "ÖLÇEK", "ÖLÇÜM", "ÖNCEL", "ÖNDER", "ÖRDEK", "ÖRNEK", "ÖRTÜ", "ÖVGÜ", "ÖYKÜ", "ÖZEL", "ÖZLEM", "ÖZVERİ",
        "PAKET", "PAMUK", "PANİK", "PARÇA", "PARKE", "PARTİ", "PATEN", "PAZAR", "PERDE", "PİLOT", "PİYON", "PLAKA", "POLİS", "POMPA", "POSTA", "PUDRA",
        "RACON", "RADAR", "RAFİNE", "RAHAT", "RAKET", "RAKİP", "REÇEL", "REHİN", "REJİM", "RENK", "RESİM", "RİAYET", "ROBOT", "ROMAN", "RÖPORT", "RÜŞT", "RÜTBE", "RÜYET",
        "SABAH", "SABIR", "SAÇAK", "SADECE", "SAHİP", "SAHNE", "SAKAL", "SAKIN", "SALÇA", "SALON", "SANAT", "SARI", "SATIR", "SAVAŞ", "SAYGI", "SEBEP", "SEÇİM", "SEFER", "SEKİZ", "SELAM", "SENET", "SERAP", "SERİN", "SEVAP", "SEVGİ", "SEYİR", "SICAK", "SIFIR", "SIĞNAK", "SİLAH", "SİLGİ", "SİNEK", "SİNİR", "SİPARİ", "SOFRA", "SOĞUK", "SOKAK", "SOLUK", "SONRA", "SORGU", "SORUN", "SÖZCÜK", "SÖYLEM", "SUBAY", "SULAMA", "SURET", "SÜREÇ", "SÜRPRİ",
        "ŞAHİN", "ŞAİBE", "ŞAKACI", "ŞANS", "ŞARAP", "ŞART", "ŞEHİR", "ŞEKER", "ŞEMSİ", "ŞERİT", "ŞİFRE", "ŞİKAYE", "ŞİMDİ", "ŞİRKET", "ŞÖLEN", "ŞÜKÜR",
        "TABAK", "TABAN", "TABİP", "TABLO", "TAHTA", "TAKIM", "TALEP", "TAMİR", "TANIK", "TARAF", "TARİH", "TARTI", "TASARI", "TAVIR", "TAVUK", "TEBRİK", "TEHLİ", "TEKİL", "TEKLİF", "TEMEL", "TEMİZ", "TEORİ", "TERAZİ", "TERCİH", "TESTİ", "TIRNAK", "TİCARET", "TOHUM", "TOPLAM", "TOPRAK", "TORUN", "TURİST", "TUTAM", "TUTKU", "TUZAK", "TÜCCAR", "TÜKETİ", "TÜNEL", "TÜRKÇE",
        "UÇAK", "UÇURT", "UĞRAŞ", "ULUSAL", "UMUT", "UNVAN", "UYARI", "UYGUN", "UYKU", "UZMAN", "ÜCRET", "ÜLKER", "ÜNİTE", "ÜRKME", "ÜSLUP", "ÜSTAT", "ÜTOPYA", "ÜVEY", "ÜZÜNTÜ",
        "VAGON", "VAHŞİ", "VAKİT", "VALİZ", "VARİS", "VATAN", "VEKİL", "VERGİ", "VİRAJ", "VİTES", "VİYANA", "VÜCUT",
        "YABAN", "YAĞIŞ", "YAKIN", "YAKIŞI", "YALAN", "YAMAÇ", "YAPIM", "YARAR", "YARIŞ", "YASAK", "YATAK", "YAVAŞ", "YAYIN", "YAZAR", "YEDEK", "YEMEK", "YENİ", "YEREL", "YEŞİL", "YETKİ", "YILAN", "YILDIZ", "YOKUŞ", "YORUM", "YÖNETİ", "YUVAR", "YÜKLEM", "YÜREK", "YÜZEY",
        "ZAMAN", "ZARAR", "ZARİF", "ZEKA", "ZEMİN", "ZEYTİN", "ZİHİN", "ZİNCİR", "ZİRVE", "ZİYAFE", "ZORLU"
    };

    public void Olustur()
    {
        kelimeler.Clear();
        foreach (string k in VARSAYILAN_SOZLUK) kelimeler.Add(k.ToUpperInvariant());
    }
    public bool KelimeVarMi(string kelime) { return kelimeler.Contains(kelime.ToUpperInvariant()); }
}

public class Torba
{
    private List<HarfTasi> taslar = new List<HarfTasi>();
    private Random rastgele = new Random();
    public static readonly char[] HARF_KARAKTERLERI = { 'A', 'B', 'C', 'Ç', 'D', 'E', 'F', 'G', 'Ğ', 'H', 'I', 'İ', 'J', 'K', 'L', 'M', 'N', 'O', 'Ö', 'P', 'R', 'S', 'Ş', 'T', 'U', 'Ü', 'V', 'Y', 'Z' };
    public static readonly int[] HARF_ADETLERI = { 12,  2,   2,   2,   2,   8,   1,   1,   1,   1,   4,   7,   1,   7,   7,   4,   5,   3,   1,   1,   6,   3,   2,   5,   3,   2,   1,   2,   2 };
    public static readonly int[] HARF_PUANLARI = {  1,   3,   4,   4,   3,   1,   7,   5,   8,   5,   2,   1,  10,   1,   1,   2,   1,   2,   7,   5,   1,   2,   4,   1,   2,   3,   7,   3,   4 };

    public void Doldur() { taslar.Clear(); for (int i = 0; i < HARF_KARAKTERLERI.Length; i++) { char karakter = HARF_KARAKTERLERI[i]; int adet = HARF_ADETLERI[i]; int puan = HARF_PUANLARI[i]; for (int j = 0; j < adet; j++) { HarfTasi yeniTas = new HarfTasi(); yeniTas.Karakter = karakter; yeniTas.Puan = puan; taslar.Add(yeniTas); } } }
    public void Karistir() { for (int i = 0; i < taslar.Count; i++) { int r = rastgele.Next(i, taslar.Count); HarfTasi temp = taslar[i]; taslar[i] = taslar[r]; taslar[r] = temp; } }
    public HarfTasi HarfCek() { if (!BosMu()) { HarfTasi t = taslar[0]; taslar.RemoveAt(0); return t; } return null; }
    public bool BosMu() { return taslar.Count == 0; }
    public int KalanTasSayisi() { return taslar.Count; }
    public static int HarfPuaniniBul(char karakter) { for(int i=0; i < Torba.HARF_KARAKTERLERI.Length; i++) if(Torba.HARF_KARAKTERLERI[i] == karakter) return Torba.HARF_PUANLARI[i]; return 0; }
}

public class Oyuncu
{
    public string Ad = ""; public int Skor = 0; public List<HarfTasi> Eli = new List<HarfTasi>();
    public void BilgileriniGir(string ad) { this.Ad = ad; }
    public void TasEkle(HarfTasi tas) { if (tas != null) Eli.Add(tas); }
    public bool TasCikar(char karakter) { for (int i = 0; i < Eli.Count; i++) if (Eli[i].Karakter == karakter) { Eli.RemoveAt(i); return true; } return false; }
    public bool GirilenHarflereSahipMi(List<char> istenenKarakterler) { List<char> geciciElKarakterleri = new List<char>(); for(int i=0; i < Eli.Count; i++) geciciElKarakterleri.Add(Eli[i].Karakter); for(int i=0; i < istenenKarakterler.Count; i++) { char istenen = istenenKarakterler[i]; if (geciciElKarakterleri.Contains(istenen)) geciciElKarakterleri.Remove(istenen); else return false; } return true; }
    public bool TasiBittiMi() { return Eli.Count == 0; }
    public void TaslariniYaz() { for(int i=0; i < Eli.Count; i++) Console.Write(Eli[i].Yazdir() + " "); }
    public void PuaniniGuncelle(int ekPuan) { Skor += ekPuan; }
}

public class Tahta
{
    private TahtaHucresi[,] hucreler;
    private const int BOYUT = 15;
    private static readonly int[,] k3_coords = { {0,2}, {0,12}, {2,0}, {2,14}, {12,0}, {12,14}, {14,2}, {14,12} };
    private static readonly int[,] k2_coords = { {2,7}, {3,3}, {3,11}, {7,2}, {7,12}, {11,3}, {11,11}, {12,7} };
    private static readonly int[,] h3_coords = { {1,1}, {1,13}, {4,4}, {4,10}, {10,4}, {10,10}, {13,1}, {13,13} };
    private static readonly int[,] h2_coords = { {0,5}, {0,9}, {1,6}, {1,8}, {5,0}, {5,5}, {5,9}, {5,14}, {6,1}, {6,6}, {6,8}, {6,13}, {8,1}, {8,6}, {8,8}, {8,13}, {9,0}, {9,5}, {9,9}, {9,14}, {13,6}, {13,8}, {14,5}, {14,9} };

    public void Olustur() { hucreler = new TahtaHucresi[BOYUT, BOYUT]; for (int i = 0; i < BOYUT; i++) for (int j = 0; j < BOYUT; j++) hucreler[i, j] = new TahtaHucresi(); YerlestirBonuslar(k3_coords, "K3"); YerlestirBonuslar(k2_coords, "K2"); YerlestirBonuslar(h3_coords, "H3"); YerlestirBonuslar(h2_coords, "H2"); }
    private void YerlestirBonuslar(int[,] coords, string bonusTipi) { for (int i = 0; i < coords.GetLength(0); i++) hucreler[coords[i, 0], coords[i, 1]].Bonus = bonusTipi; }
    public void Ciz() { Console.WriteLine(); Console.Write("    "); for (int j = 0; j < BOYUT; j++) Console.Write($"{j,-4}"); Console.WriteLine(); Console.Write("   +"); for (int j = 0; j < BOYUT; j++) Console.Write("---+"); Console.WriteLine(); for (int i = 0; i < BOYUT; i++) { Console.Write($"{i,-2} |"); for (int j = 0; j < BOYUT; j++) { TahtaHucresi h = hucreler[i,j]; if (h.GetDoluMu()) { Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.DarkGray; Console.Write($" {h.Tas.Karakter} "); Console.ResetColor(); } else if (h.Bonus != null) { if (h.Bonus == "K3") Console.ForegroundColor = ConsoleColor.Red; else if (h.Bonus == "K2") Console.ForegroundColor = ConsoleColor.Magenta; else if (h.Bonus == "H3") Console.ForegroundColor = ConsoleColor.DarkBlue; else if (h.Bonus == "H2") Console.ForegroundColor = ConsoleColor.Cyan; else Console.ForegroundColor = ConsoleColor.Gray; if (h.Bonus.Length == 2) Console.Write($" {h.Bonus}"); else if (h.Bonus.Length == 1) Console.Write($" {h.Bonus} "); else Console.Write(h.Bonus.Substring(0,3)); Console.ResetColor(); } else { Console.Write("   "); } Console.Write("|"); } Console.WriteLine(); Console.Write("   +"); for (int k = 0; k < BOYUT; k++) Console.Write("---+"); Console.WriteLine(); } }
    public string GecerlilikKontrol(string kelime, int satir, int sutun, char yon, Oyuncu oyuncu, bool ilkHamle, out List<char> eldenKullanilanKarakterler) { eldenKullanilanKarakterler = new List<char>(); List<char> oyuncununGeciciEli = new List<char>(); for(int k=0; k < oyuncu.Eli.Count; k++) oyuncununGeciciEli.Add(oyuncu.Eli[k].Karakter); bool enAzBirYeniHarfKullanildi = false; bool mevcutHarfeTemasOldu = false; for (int i = 0; i < kelime.Length; i++) { int r_hedef = satir, c_hedef = sutun; if (yon == 'H') c_hedef += i; else r_hedef += i; if (r_hedef < 0 || r_hedef >= BOYUT || c_hedef < 0 || c_hedef >= BOYUT) return "Kelime tahtadan taşıyor."; char kelimedekiHarf = kelime[i]; if (hucreler[r_hedef, c_hedef].Tas == null) { enAzBirYeniHarfKullanildi = true; if (!oyuncununGeciciEli.Contains(kelimedekiHarf)) return $"'{kelimedekiHarf}' elinizde yok."; eldenKullanilanKarakterler.Add(kelimedekiHarf); oyuncununGeciciEli.Remove(kelimedekiHarf); int[] dr = {-1, 1, 0, 0}; int[] dc = {0, 0, -1, 1}; for(int k_dr=0; k_dr<4; k_dr++) { int nr = r_hedef + dr[k_dr], nc = c_hedef + dc[k_dr]; if(nr >= 0 && nr < BOYUT && nc >= 0 && nc < BOYUT && hucreler[nr,nc].Tas != null) mevcutHarfeTemasOldu = true; } } else { if (hucreler[r_hedef, c_hedef].Tas.Karakter != kelimedekiHarf) return $"Uyuşmazlık: ({r_hedef},{c_hedef}) '{hucreler[r_hedef,c_hedef].Tas.Karakter}', kelimede '{kelimedekiHarf}'."; mevcutHarfeTemasOldu = true; } } if (!enAzBirYeniHarfKullanildi) return "En az bir yeni harf kullanmalısınız."; if (!ilkHamle && !mevcutHarfeTemasOldu) return "Yeni kelime mevcut bir harfe dokunmalı."; return "OK"; }
    public List<Koordinat> KelimeYerlestir(string kelime, int satir, int sutun, char yon, HarfTasi[] yerlestirilecekTaslar) { List<Koordinat> yeniHarfKoordinatlari = new List<Koordinat>(); int tasIndex = 0; for (int i = 0; i < kelime.Length; i++) { int r = satir, c = sutun; if (yon == 'H') c += i; else r += i; if (hucreler[r,c].Tas == null) { Koordinat yeniKoord = new Koordinat(); yeniKoord.Satir = r; yeniKoord.Sutun = c; yeniHarfKoordinatlari.Add(yeniKoord); if(tasIndex < yerlestirilecekTaslar.Length) hucreler[r,c].Tas = yerlestirilecekTaslar[tasIndex++]; } } return yeniHarfKoordinatlari; }
    // BonusTemizle metodu Tahta sınıfından kaldırıldı.

    public int KelimePuanla(string kelime, int satir, int sutun, char yon, bool anaKelimeMi, List<Koordinat> buHamledeYeniKonanKoordinatlar) {
        int toplamPuan = 0;
        int genelKelimeCarpani = 1; // Kelimenin tamamına uygulanacak çarpım

        for (int i = 0; i < kelime.Length; i++) {
            int r_harf = satir, c_harf = sutun;
            if (yon == 'H') c_harf += i; else r_harf += i;

            if (r_harf < 0 || r_harf >= BOYUT || c_harf < 0 || c_harf >= BOYUT || hucreler[r_harf, c_harf].Tas == null) continue;

            TahtaHucresi aktifHucre = hucreler[r_harf, c_harf];
            HarfTasi mevcutTas = aktifHucre.Tas;
            int harfPuani = mevcutTas.Puan;
            int anlikHarfCarpani = 1;

            bool buHarfYeniMi = false;
            for(int k=0; k < buHamledeYeniKonanKoordinatlar.Count; k++) {
                if(buHamledeYeniKonanKoordinatlar[k].Satir == r_harf && buHamledeYeniKonanKoordinatlar[k].Sutun == c_harf) {
                    buHarfYeniMi = true;
                    break;
                }
            }

            if (buHarfYeniMi && aktifHucre.Bonus != null) { // Sadece yeni konan taşların bonusları etkili olur
                anlikHarfCarpani = aktifHucre.GetHarfCarpani();
                genelKelimeCarpani *= aktifHucre.GetKelimeCarpani();
                aktifHucre.KullanBonus(); // Bonus kullanıldı olarak işaretle ve temizle
            }
            toplamPuan += harfPuani * anlikHarfCarpani;
        }
        return toplamPuan * genelKelimeCarpani;
    }
}

public class Oyun
{
    private Tahta tahta = new Tahta(); private Torba torba = new Torba(); private Sozluk sozluk = new Sozluk();
    private Oyuncu[] oyuncular = { new Oyuncu(), new Oyuncu() };
    private int siradakiOyuncuIndex = 0; private bool ilkHamle = true;

    public void Baslat() { Console.WriteLine("--- Scrabble Oyunu Başlıyor (Hücre Sorumlulukları) ---"); tahta.Olustur(); torba.Doldur(); torba.Karistir(); sozluk.Olustur(); OyuncuBilgileriniAl(); OyuncularaTasDagit(7); Oynat(); SkorlariYaz(true); }
    private void OyuncuBilgileriniAl() { Console.Write("1. Oyuncu Adı: "); oyuncular[0].BilgileriniGir(Console.ReadLine()); Console.Write("2. Oyuncu Adı: "); oyuncular[1].BilgileriniGir(Console.ReadLine()); }
    private void OyuncularaTasDagit(int adet) { for (int i = 0; i < oyuncular.Length; i++) for (int j = 0; j < adet; j++) oyuncular[i].TasEkle(torba.HarfCek()); }
    private void Oynat() {
        bool oyunDevam = true;
        while (oyunDevam) {
            Console.Clear(); tahta.Ciz(); Oyuncu mevcutOyuncu = oyuncular[siradakiOyuncuIndex];
            Console.WriteLine("\n--- Oyuncu Bilgileri ve Elleri ---"); for (int i = 0; i < oyuncular.Length; i++) OyuncularinTaslariniGoster(oyuncular[i]);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n>>> SIRA SİZDE: {mevcutOyuncu.Ad} <<<"); Console.ResetColor();
            Console.WriteLine($"Torbada Kalan Taş: {torba.KalanTasSayisi()}");
            Console.Write($"\n{mevcutOyuncu.Ad}, Hamleniz (KELIME SATIR SÜTUN YÖN): ");
            string giris = Console.ReadLine().ToUpper(); string kelime; int satir, sutun; char yon;
            if (!GirisParseEtVeTemelKontrol(giris, out kelime, out satir, out sutun, out yon)) { SiraDegistir(); continue; }
            if (!sozluk.KelimeVarMi(kelime)) { Console.WriteLine($"'{kelime}' sözlükte yok! (Enter)"); Console.ReadLine(); SiraDegistir(); continue; }
            List<char> kullanilanKarakterler;
            string gecerlilikSonucu = tahta.GecerlilikKontrol(kelime, satir, sutun, yon, mevcutOyuncu, ilkHamle, out kullanilanKarakterler);
            if (gecerlilikSonucu == "OK") {
                HarfTasi[] yerlestirilecekTaslar = new HarfTasi[kullanilanKarakterler.Count];
                for(int i=0; i < kullanilanKarakterler.Count; i++) { yerlestirilecekTaslar[i] = new HarfTasi(); yerlestirilecekTaslar[i].Karakter = kullanilanKarakterler[i]; yerlestirilecekTaslar[i].Puan = Torba.HarfPuaniniBul(kullanilanKarakterler[i]); }
                List<Koordinat> yeniHarfKoordinatlari = tahta.KelimeYerlestir(kelime, satir, sutun, yon, yerlestirilecekTaslar);
                int anaKelimePuani = tahta.KelimePuanla(kelime, satir, sutun, yon, true, yeniHarfKoordinatlari);
                mevcutOyuncu.PuaniniGuncelle(anaKelimePuani); Console.WriteLine($"'{kelime}' için {anaKelimePuani} puan.");
                // Bonus temizleme döngüsü Oyun.Oynat() içinden kaldırıldı.
                for (int i = 0; i < kullanilanKarakterler.Count; i++) mevcutOyuncu.TasCikar(kullanilanKarakterler[i]);
                for (int i = 0; i < kullanilanKarakterler.Count; i++) mevcutOyuncu.TasEkle(torba.HarfCek());
                ilkHamle = false;
                if (OyunBittiMi()) oyunDevam = false; else { Console.WriteLine("Devam etmek için Enter..."); Console.ReadLine(); }
            } else { Console.WriteLine($"Geçersiz Hamle: {gecerlilikSonucu} (Enter)"); Console.ReadLine(); }
            if (oyunDevam) SiraDegistir();
        }
    }
    private bool GirisParseEtVeTemelKontrol(string g, out string k, out int sa, out int su, out char y) { k = ""; sa = -1; su = -1; y = ' '; string[] p = g.Split(' '); if (p.Length != 4) { Console.WriteLine("Hatalı giriş! (Enter)"); Console.ReadLine(); return false; } k = p[0]; try { sa = int.Parse(p[1]); su = int.Parse(p[2]); y = p[3][0]; } catch { Console.WriteLine("Sayı/yön hatalı! (Enter)"); Console.ReadLine(); return false; } if ((y != 'H' && y != 'V') || k.Length == 0) { Console.WriteLine("Geçersiz kelime/yön! (Enter)"); Console.ReadLine(); return false; } return true; }
    private bool OyunBittiMi() { if (torba.BosMu()) for(int i=0; i < oyuncular.Length; i++) if (oyuncular[i].TasiBittiMi()) { Console.WriteLine($"\nTorba boş ve {oyuncular[i].Ad} taşı bitti. Oyun bitti!"); return true; } return false; }
    private void OyuncularinTaslariniGoster(Oyuncu o) { Console.WriteLine("-------------------------------------------------------------------------"); Console.Write($"Oyuncu: {o.Ad} | Puan: {o.Skor} | El: "); o.TaslariniYaz(); Console.WriteLine(); Console.WriteLine("-------------------------------------------------------------------------"); }
    private void SkorlariYaz(bool oyunSonu = false) { Console.Clear(); tahta.Ciz(); Console.WriteLine(oyunSonu ? "\nOYUN BİTTİ" : "\nSKORLAR"); for(int i=0; i<oyuncular.Length; i++) Console.WriteLine($"{oyuncular[i].Ad}: {oyuncular[i].Skor} Puan"); Console.WriteLine("============"); if (oyunSonu) { Oyuncu kazanan = null; if (oyuncular[0].Skor > oyuncular[1].Skor) kazanan = oyuncular[0]; else if (oyuncular[1].Skor > oyuncular[0].Skor) kazanan = oyuncular[1]; if (kazanan != null) Console.WriteLine($"KAZANAN: {kazanan.Ad}"); else Console.WriteLine("BERABERE!"); Console.WriteLine("============"); Console.WriteLine("\nÇıkmak için Enter..."); Console.ReadLine(); } }
    private void SiraDegistir() { siradakiOyuncuIndex = (siradakiOyuncuIndex + 1) % oyuncular.Length; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Oyun yeniOyun = new Oyun();
        yeniOyun.Baslat();
    }
}