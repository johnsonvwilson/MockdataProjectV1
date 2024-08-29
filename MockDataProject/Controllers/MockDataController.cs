using System.Linq;
using System.Net;
using System.Web.Mvc;
using MockDataProject.Models;

namespace MockDataProject.Controllers
{
    public class MockDataController : Controller
    {
        private MockDataContext db = new MockDataContext();

        // GET: MockData
        public ActionResult Index()
        {
            return View(db.MockDatas.ToList());
        }

        // GET: MockData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MockData/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email")] MockData mockData)
        {
            if (ModelState.IsValid)
            {
                db.MockDatas.Add(mockData);
                db.SaveChanges();

                // Return the JSON data for the new entry
                return Json(new { success = true, data = mockData });
            }

            return Json(new { success = false });
        }

        // GET: MockData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MockData mockData = db.MockDatas.Find(id);
            if (mockData == null)
            {
                return HttpNotFound();
            }
            return View(mockData);
        }

        // POST: MockData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email")] MockData mockData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mockData).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mockData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mockData = db.MockDatas.Find(id);
            if (mockData == null)
            {
                return Json(new { success = false });
            }

            db.MockDatas.Remove(mockData);
            db.SaveChanges();

            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
