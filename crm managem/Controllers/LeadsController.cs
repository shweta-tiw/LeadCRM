using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using LeadManagementSystem.Data;

namespace LeadManagementSystem.Controllers
{
    public class LeadsController : Controller
    {
        DbHelper db = new DbHelper();

        // GET: Leads
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            string query = @"SELECT Id, Name, Email, Phone, Status FROM Leads";
            DataTable dt = db.ExecuteSelect(query);

            return View(dt);
        }

        // GET: Leads/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Leads/Create
        [HttpPost]
        public ActionResult Add(string Name, string Email, string Phone)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            string query = @"INSERT INTO Leads (Name, Email, Phone, Status, CreatedAt)
                             VALUES (@Name, @Email, @Phone, 'New', GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", Name),
                new SqlParameter("@Email", Email),
                new SqlParameter("@Phone", Phone)
            };

            db.ExecuteDML(query, parameters);

            return RedirectToAction("Index");
        }

        // GET: Leads/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            string query = "SELECT * FROM Leads WHERE Id=@Id";
            SqlParameter[] param = { new SqlParameter("@Id", id) };

            DataTable dt = db.ExecuteSelect(query, param);

            if (dt.Rows.Count == 0)
                return HttpNotFound();

            return View(dt.Rows[0]);
        }

        // POST: Leads/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, string Name, string Email, string Phone, string Status)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            string query = @"UPDATE Leads 
                             SET Name=@Name, Email=@Email, Phone=@Phone, Status=@Status 
                             WHERE Id=@Id";

            SqlParameter[] param = {
                new SqlParameter("@Name", Name),
                new SqlParameter("@Email", Email),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Id", Id)
            };

            db.ExecuteDML(query, param);

            return RedirectToAction("Index");
        }

        // GET: Leads/Delete/5
        [HttpPost]
        public JsonResult DeleteLead(int id)
        {
            try
            {
                string query = "DELETE FROM Leads WHERE Id=@Id";
                SqlParameter[] param = { new SqlParameter("@Id", id) };

                int rows = db.ExecuteDML(query, param);

                if (rows > 0)
                    return Json(new { success = true });
                else
                    return Json(new { success = false, message = "Lead not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, string status)
        {
            try
            {
                string query = "UPDATE Leads SET Status=@Status WHERE Id=@Id";

                SqlParameter[] param = {
            new SqlParameter("@Status", status),
            new SqlParameter("@Id", id)
        };

                int rows = db.ExecuteDML(query, param);

                return Json(new { success = true, rows = rows });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
       
    }
}