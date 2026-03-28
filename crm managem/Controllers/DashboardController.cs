using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using LeadManagementSystem.Data;

namespace LeadManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        DbHelper db = new DbHelper();

        // GET: /Dashboard/Dashboard
        public ActionResult Dashboard()
        {
            // Session check
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            // Lead counts
            ViewBag.TotalLeads = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM Leads"));
            ViewBag.New = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM Leads WHERE Status='New'") ?? 0);
            ViewBag.Contacted = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM Leads WHERE Status='Contacted'") ?? 0);
            ViewBag.Won = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM Leads WHERE Status='Won'") ?? 0);
            ViewBag.Lost = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(*) FROM Leads WHERE Status='Lost'") ?? 0);


            // ✅ FIXED: Simple status-wise summary (no AssignedTo)
            DataTable dt = db.ExecuteSelect(@"
                SELECT Status, COUNT(*) as Total
                FROM Leads
                GROUP BY Status
            ");

            ViewBag.StatusSummary = dt.AsEnumerable().Select(r => new
            {
                Status = r["Status"].ToString(),
                Total = (int)r["Total"]
            }).ToList();

            return View();
        }
    }
}