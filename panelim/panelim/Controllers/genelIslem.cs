using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using panelim.Models;
namespace System.Web.Mvc
{
    public static class genelIslem
    {
        static SiteDbEntities db = new SiteDbEntities();


        public static string sayfaIddenAd(int? sayfaid)
        {

            int id = Convert.ToInt32(sayfaid);

            return db.Sayfalar.Where(m => m.Id == id).Single().SayfaAdi;

            //return "11";


        }

        public static int loginOlmusmu()
        {

            try
            {
                if (HttpContext.Current.Request.Cookies["giris"] != null)
                {

                    string uyeid = HttpContext.Current.Request.Cookies["giris"].Values[0];
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            catch
            {
                return 0;
            }
        }
        //public static List<Kullanici> kullanici(string kullaniciadi, string parola)
        //{
        //    SiteDbEntities kk = new SiteDbEntities();
        //    List<Kullanici> kullanicilar = db.Kullanici.Where(m => m.KullaniciAdi == kullaniciadi && m.Parola == parola).ToList();
        //    if(kullanicilar==null)
        //    {
        //        return null;
        //    }
        //    return kullanicilar;

        //}
        public static string logo(int? tempId)
        {
            SiteDbEntities MB = new SiteDbEntities();

            var tblLogo = MB.tblLogo.FirstOrDefault();
            if (tblLogo == null)
            {
                return "resim yok";
            }
            return tblLogo.LogoIcerik;


        }
        public static List<tblAltMenu> altmenuler(int id)
        {

            List<tblAltMenu> menuler = db.tblAltMenu.Where(m => m.UstId == id).AsEnumerable().ToList();

            return menuler;

        }
        public static string menuIddenAd(int? menuid)
        {
            int id = Convert.ToInt32(menuid);

            return db.tblMenu.Where(m => m.Id == id).Single().MenuAd; ;
        }
        public static List<tblAltMenu> tblAltMenu(int TempId)
        {


            var menu = db.tblMenu.FirstOrDefault();
            if (menu != null)
            {
                List<tblAltMenu> altmenu = db.tblAltMenu.Where(m => m.MenuId == menu.Id && m.UstId == 0).ToList();
                return altmenu;
            }

            return null;

        }
        public static List<tblGaleriResim> FullWidth(int TempId)
        {

            SiteDbEntities ml = new SiteDbEntities();
            var menu = ml.tblGaleri.Where(m => m.g_tipi == "Full Width" && m.aktif == true).FirstOrDefault();
            if (menu != null)
            {
                List<tblGaleriResim> sliders = ml.tblGaleriResim.Where(m => m.Galeri_Id == menu.Id).OrderBy(m => m.sira).ToList();
                return sliders;
            }
            else {
                return null;
            }

           
        }
        public static List<tblGaleriResim> Content (int TempId)
        {


            var menu = db.tblGaleri.Where(m => m.g_tipi == "Content Galeri" && m.aktif == true).FirstOrDefault();
            if (menu != null)
            {
                List<tblGaleriResim> sliders = db.tblGaleriResim.Where(m => m.Galeri_Id == menu.Id).OrderBy(m => m.sira).ToList();
                return sliders;
            }
            else
            {
                return null;
            }


        }
        public static List<Sayfalar> sayfa(int TempId)
        {
            SiteDbEntities cc = new SiteDbEntities();
            List<Sayfalar> sayfalar = cc.Sayfalar.Where(m =>  m.SayfaAdi != "Anasayfa").OrderBy(m => m.sirano).ToList();

            return sayfalar;

        }
        public static string galeriiddentip(int? GaleriId)
        {
            int galeriid = Convert.ToInt32(GaleriId);
            var galeri = db.tblGaleri.Find(galeriid);
            return galeri.g_tipi;

        }
        public static List<tblGaleriResim> galeriResim(int? galeriid)
        {
            SiteDbEntities sb = new SiteDbEntities();
            List<tblGaleriResim> galeriresim = sb.tblGaleriResim.Where(x => x.Galeri_Id == galeriid).OrderBy(m => m.sira).ToList();
            return galeriresim;

        }
        public static List<tblFormIcerik> form(int? id)
        {
            List<tblFormIcerik> sayfaforms = db.tblFormIcerik.Where(X => X.FormId == id).OrderBy(x => x.sira).ToList();

            return sayfaforms;

        }
        public static List<sayfaform> sayfaform(int? id)
        {
            List<sayfaform> sayfaforms = db.sayfaform.Where(X => X.SayfaId == id).OrderBy(x=>x.FormId).ToList();
            
            return sayfaforms;

        }
        public static string formiddenad(int? formid)
        {
            int id = Convert.ToInt32(formid);

            return db.tblForm.Where(m => m.id == id).Single().FormAdi; ;
        }

    }
}