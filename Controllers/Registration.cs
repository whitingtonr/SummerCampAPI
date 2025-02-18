using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SummerCampAPI.Controllers
{
  public class Registration : Controller
  {
    // GET: SummerCampRegistration
    public ActionResult Index()
    {
      return View();
    }

    // GET: SummerCampRegistration/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: SummerCampRegistration/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: SummerCampRegistration/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: SummerCampRegistration/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: SummerCampRegistration/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: SummerCampRegistration/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: SummerCampRegistration/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
