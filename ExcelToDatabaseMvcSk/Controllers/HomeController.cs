using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelToDatabaseMvcSk.Models;
using System.Data.Entity;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelToDatabaseMvcSk.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Excel Dosyası Yüklemek İçin Butona Tıklayabililirsiniz";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Veriler";

            return View();
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var usersList = new List<DosyaVerileri>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 4; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new DosyaVerileri();
                            user.Bilanco = workSheet.Cells[rowIterator, 1].Value.ToString();
                            user.OncekiYil = Convert.ToDecimal(workSheet.Cells[rowIterator, 2].Value);
                            user.CariYil = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value);
                            usersList.Add(user);
                        }

                    }
                }
            }
            using (ExcelImportDBEntities db = new ExcelImportDBEntities())
            {
                foreach (var item in usersList)
                {
                    db.DosyaVerileri.Add(item);
                }
                db.SaveChanges();
            }
            ViewBag.ListDetay = usersList;
            return View("Contact");
        }

        public ActionResult Listele()
        {
            ExcelImportDBEntities dt = new ExcelImportDBEntities();

            var degerler = dt.DosyaVerileri.ToList();
            return View(degerler);

        }

        public ActionResult Hesapla()
        {
            var DosyaList = new List<DosyaVerileri>();

            ExcelImportDBEntities db = new ExcelImportDBEntities();

            var hesapla = new List<HesaplananVeri>();
            var item = new DosyaVerileri();
            var degerler = db.DosyaVerileri.ToList();
            int j = 0;
            foreach (var i in degerler)
            {
                j = j + 1;
                var veri = new HesaplananVeri();
                veri.Bilanco = i.Bilanco;
                veri.OncekiYil = i.OncekiYil;
                veri.CariYil = i.CariYil;
                var sonuc1 = i.OncekiYil;
                var sonuc2 = i.CariYil;
                if (j < 65)
                {
                    var sonuc = sonuc1 - sonuc2;
                    veri.Sonuc = Convert.ToDecimal(sonuc);
                }
                else
                {
                    var sonuc = sonuc2 - sonuc1;
                    veri.Sonuc = Convert.ToDecimal(sonuc);


                }

                hesapla.Add(veri);

            }
            using (ExcelImportDBEntities dt = new ExcelImportDBEntities())
            {

                foreach (var veri in hesapla)
                {
                    dt.HesaplananVeri.Add(veri);
                }
                dt.SaveChanges();
            }
            ViewBag.ListDetay = hesapla;
            return View("Hesapla");



        }
        public ActionResult Hesapla2()
        {
            ExcelImportDBEntities dt = new ExcelImportDBEntities();

            var degerler = dt.HesaplananVeri.ToList();
            return View(degerler);

        }






    }
}