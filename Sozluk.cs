using System;
using System.Collections.Generic;
using System.Text;

public class Sozluk
{
    private List<string> kelimeler = new List<string>();
    private static  string[] VARSAYILAN_SOZLUK = {
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