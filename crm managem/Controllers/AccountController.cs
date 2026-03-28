using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LeadManagementSystem.Data;
using LeadManagementSystem.Models;

namespace LeadManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        DbHelper db = new DbHelper();

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email already exists
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email=@Email";
            SqlParameter[] checkParams = { new SqlParameter("@Email", model.Email) };
            int exists = Convert.ToInt32(db.ExecuteScalar(checkQuery, checkParams));
            if (exists > 0)
            {
                ModelState.AddModelError("", "Email already registered.");
                return View(model);
            }

            // Hash password
            string hashed = HashPassword(model.Password);

            // Insert user
            string insertQuery = "INSERT INTO Users (FullName, Email, PasswordHash, Role) VALUES (@FullName, @Email, @PasswordHash, @Role)";
            SqlParameter[] insertParams = {
                new SqlParameter("@FullName", model.FullName),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@PasswordHash", hashed),
                new SqlParameter("@Role", model.Role)
            };
            db.ExecuteDML(insertQuery, insertParams);

            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string query = "SELECT Id, FullName, PasswordHash, Role FROM Users WHERE Email=@Email";
            SqlParameter[] parameters = { new SqlParameter("@Email", model.Email) };
            DataTable dt = db.ExecuteSelect(query, parameters);

            if (dt.Rows.Count == 0)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            string storedHash = dt.Rows[0]["PasswordHash"].ToString();
            if (!VerifyPassword(model.Password, storedHash))
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // Set session
            Session["UserId"] = dt.Rows[0]["Id"];
            Session["FullName"] = dt.Rows[0]["FullName"];
            Session["Role"] = dt.Rows[0]["Role"];

            return RedirectToAction("Dashboard", "Dashboard");
        }

        // GET: /Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // Hash password with SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // Verify hashed password
        private bool VerifyPassword(string password, string hash)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}