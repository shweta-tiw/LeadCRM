🚀 Lead Management System (CRM)

A full-featured web-based Lead Management System built with ASP.NET MVC and ADO.NET, designed to manage leads efficiently and track sales performance.  
This project demonstrates skills in backend, frontend, database integration, responsive UI design, and secure session management.



🌟 Features

- 🔐 User Authentication & Roles
  - Secure login and registration
  - Role-based access (Admin, Sales)
  - Session management for protected pages

- 📋 Lead Management (CRUD)
  - Add, Update, Delete, and View leads
  - Assign leads to Sales users
  - Filter leads by status, date, and user
  - Search leads by name, email, or phone

- 📊 Dashboard & Reports
  - Admin dashboard showing lead statistics
  - Charts for lead status (Won, Lost, Pending)
  - Top-performing sales users ranking

- 📱 Responsive Design
  - Built using Bootstrap 5
  - Works on desktop, tablet, and mobile

- 🛡 Security & Best Practices
  - `.gitignore` used to keep sensitive files safe
  - `Web.config` excluded from Git
  - Proper MVC architecture for maintainability

---

🛠 Tech Stack

- Backend: ASP.NET MVC, C#, ADO.NET  
- Frontend: HTML5, CSS3, Bootstrap 5, jQuery  
- Database: SQL Server  
- Version Control: Git & GitHub  

---

⚡ Installation & Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/shweta-tiw/LeadCRM.git

2. Open the solution in Visual Studio (.sln file).
3. Update the Web.config connection string to your local SQL Server database.
4. Build the solution and run (F5).
5. Use default roles to login (or register a new user).

📁 Project Structure
• /Controllers  → Handles user requests and actions
• /Models       → Data models for Users, Leads, Sales
• /Views        → UI pages for Dashboard, Leads, Account
• /Content      → CSS & Bootstrap files
• /Scripts      → JS & jQuery scripts
• App_Start     → Route, Bundle, and Filter configuration


👩‍💻 Key Highlights
• Fully functional Lead CRUD operations with real-time updates
• Admin dashboard with lead statistics and top users ranking
• Secure session-based authentication
• Polished, responsive, and user-friendly UI
• Uses Bootstrap and jQuery enhancements for smooth UX
