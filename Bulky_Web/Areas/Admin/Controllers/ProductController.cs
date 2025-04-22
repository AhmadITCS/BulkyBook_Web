using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.UintOfWork;
using BulkyBookModels1.View_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BulkyBook_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        
        
            private readonly IUintOfWork _uintOfWork;
        private readonly IWebHostEnvironment _environment;
            public ProductController(IUintOfWork uintOfWork ,IWebHostEnvironment environment)
            {
                _uintOfWork = uintOfWork;
            _environment = environment; 
            }

            public IActionResult Index()
            {
                List<Product> products = _uintOfWork.Product.GetAll(includeprop:"Category").ToList();

                return View(products);
            }
            public IActionResult UpSert(int? id)
            {

            //ViewBag.Categories = CatrgoryList;
            ProductVM productVM = new()
            {
                CategoryList = _uintOfWork.Category.GetAll().Select(n => new SelectListItem
                {

                    Text = n.Name,
                    Value = n.Id.ToString()

                }),

                product = new Product()
            };
            if (id == null || id == 0) {

                return View(productVM);
            }
            else
            {
               productVM.product=_uintOfWork.Product.Get(U => U.Id == id); 
                return View(productVM); 
            }
               
            }
        [HttpPost]
        public IActionResult UpSert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                String wwwRootPath = _environment.WebRootPath;
                if (file != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    String ProductPath = Path.Combine(wwwRootPath, "Image", "Product");

                    // تأكد من أن المجلد موجود، إذا لم يكن موجودًا، قم بإنشائه
                    if (!Directory.Exists(ProductPath))
                    {
                        Directory.CreateDirectory(ProductPath);
                    }

                    // ✅ حذف الصورة القديمة إذا وجدت
                    if (!string.IsNullOrEmpty(obj.product.ImageUrl))
                    {
                        string cleanedImageUrl = obj.product.ImageUrl.Replace("/", "\\").TrimStart('\\').Trim();
                        var oldimagepath = Path.Combine(wwwRootPath, cleanedImageUrl);

                        Console.WriteLine("📌 المسار الصحيح للصورة القديمة: " + oldimagepath);

                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                            Console.WriteLine("✅ تم حذف الصورة القديمة بنجاح!");
                        }
                        else
                        {
                            Console.WriteLine("❌ الصورة القديمة غير موجودة أو المسار غير صحيح!");
                        }
                    }

                    // ✅ حفظ الصورة الجديدة
                    using (var filestream = new FileStream(Path.Combine(ProductPath, FileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    // ✅ تحديث مسار الصورة في الكائن
                    obj.product.ImageUrl = @"Image\Product\" + FileName;
                }

                // ✅ تحديث قاعدة البيانات
                if (obj.product.Id == 0)
                {
                    _uintOfWork.Product.Add(obj.product);
                }
                else
                {
                    _uintOfWork.Product.UpDate(obj.product);
                }
                _uintOfWork.Save();

                TempData["Sucsess"] = "Success: This update was successful";
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _uintOfWork.Category.GetAll().Select(n => new SelectListItem
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                });

                return View(obj);
            }
        }


        //public IActionResult Delete(int id)
        //    {
        //        Product product= _uintOfWork.Product.Get(u => u.Id == id);
        //        _uintOfWork.Product.Delete(product);
        //        _uintOfWork.Save();
        //        TempData["Sucsess"] = "sucsess tis update";
        //        return RedirectToAction("Index");

        //    }
        #region API CLASS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ObjProductList = _uintOfWork.Product.GetAll(includeprop: "Category").ToList();
            return Json(new { data = ObjProductList });     
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

        var productdelete = _uintOfWork.Product.Get(u =>u.Id == id);
            if(productdelete == null)
            {
                return Json(new { success = false,message="Error while" });
            }
            string cleanedImageUrl = productdelete.ImageUrl.Replace("/", "\\").TrimStart('\\').Trim();
            var oldimagepath = Path.Combine(_environment.WebRootPath, cleanedImageUrl);

            if (System.IO.File.Exists(oldimagepath))
            {
                System.IO.File.Delete(oldimagepath);
            }
            _uintOfWork.Product.Delete(productdelete);
            _uintOfWork.Save();
            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion
    }

}
