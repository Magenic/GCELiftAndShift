using CompanyX.Data;
using ExpenseReportApp.Service;
using System.Net;
using System.Web.Mvc;
using ExpenseReportApp.Models;
using ExpenseReportApp.Data;

namespace ExpenseReportApp.Controllers
{
    public class ExpenseReportController : Controller
    {
        #region PrivateVariables
        private ExpenseReportService _reportService;
        private LineItemService _lineItemService;
        private CategoryService _categoryService;
        #endregion

        #region Constructor
        public ExpenseReportController()
        {
            _reportService = new ExpenseReportService();
            _lineItemService = new LineItemService();
            _categoryService = new CategoryService();
        }
        #endregion

        #region GetExpenseReports
        public ActionResult ViewExpenseReport(int? expReportId)
        {
            if (expReportId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ExpenseReportViewModel vm = new ExpenseReportViewModel();
            vm.ExpenseReport = _reportService.GetByExpenseReportID(expReportId);
            vm.LineItems = _lineItemService.GetLineItems(expReportId);
            vm.LineItem = new LineItem();
            vm.LineItem.ExpenseReportID = expReportId;

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", null);
            return View(vm);
        }

        public ActionResult ViewExpenseReportList(string user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var expReports = _reportService.GetByUserID(user);
            return View(expReports);
        }
        #endregion

        #region ExpenseReportCRUD
        [HttpGet]
        public ActionResult CreateExpenseReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateExpenseReport(ExpenseReport expReport)
        {
            if (ModelState.IsValid)
            {
                expReport.UserID = User.Identity.Name;
                int expRepId = _reportService.Create(expReport);
                return RedirectToAction("ViewExpenseReport", "ExpenseReport", new { expReportId = expRepId });
            }
            return View();

        }

        [HttpGet]
        public ActionResult Update(int? expReportId)
        {
            if (expReportId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ExpenseReportViewModel vm = new ExpenseReportViewModel();
            vm.ExpenseReport = _reportService.GetByExpenseReportID(expReportId);
            vm.LineItems = _lineItemService.GetLineItems(expReportId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Update(ExpenseReport expenseReport)
        {
            if (ModelState.IsValid)
            {
                _reportService.Update(expenseReport);
                return RedirectToAction("ViewExpenseReport", "ExpenseReport", new { expReportId = expenseReport.ExpenseReportID });
            }

            ExpenseReportViewModel vm = new ExpenseReportViewModel();
            vm.ExpenseReport = _reportService.GetByExpenseReportID(expenseReport.ExpenseReportID);
            vm.LineItems = _lineItemService.GetLineItems(expenseReport.ExpenseReportID);
            return View(vm);
        }

        public ActionResult Delete(int? expReportId)
        {
            if (expReportId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _reportService.Delete(expReportId);
            return RedirectToAction("ViewExpenseReportList", new { user = User.Identity.Name });
        }
        #endregion

        #region LineItemCRUD
        [HttpPost]
        public ActionResult CreateLineItem(LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                _lineItemService.Create(lineItem);
                return RedirectToAction("ViewExpenseReport", "ExpenseReport", new { expReportId = lineItem.ExpenseReportID });
            }

            ExpenseReportViewModel vm = new ExpenseReportViewModel();
            vm.ExpenseReport = _reportService.GetByExpenseReportID(lineItem.ExpenseReportID);
            vm.LineItems = _lineItemService.GetLineItems(lineItem.ExpenseReportID);
            vm.LineItem = new LineItem();
            vm.LineItem.ExpenseReportID = lineItem.ExpenseReportID;

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", null);
            return View("ViewExpenseReport", vm);
        }

        [HttpGet]
        public ActionResult UpdateLineItem(int? lineItemId, int? categoryId)
        {

            if (lineItemId == null || categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _lineItemService.GetLineItem(lineItemId);
            item.CategoryID = categoryId;

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", null);

            return PartialView(item);
        }

        [HttpPost]
        public ActionResult UpdateLineItem(LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                _lineItemService.Update(lineItem);
                return RedirectToAction("ViewExpenseReport", "ExpenseReport", new { expReportId = lineItem.ExpenseReportID });
            }

            var item = _lineItemService.GetLineItem(lineItem.LineItemID);
            item.CategoryID = lineItem.CategoryID;

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", null);
            return View("ViewExpenseReport", new { expReportId = lineItem.ExpenseReportID });
        }

        public ActionResult DeleteLineItem(int? lineItemId, int? expReportId)
        {
            _lineItemService.Delete(lineItemId);
            return RedirectToAction("ViewExpenseReport", "ExpenseReport", new { expReportId = expReportId });
        }

        #endregion
    }
}