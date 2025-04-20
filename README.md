# ERPConnect

**ERPConnect** is a lightweight ERP simulation built with ASP.NET Core and SQL Server.  
It covers core modules—Products, Inventory, Customers, and Billing—with role‑based access, reporting, and PDF export.

## Features

- **Products CRUD**: Manage your product catalog (name, description, price).  
- **Inventory Management**: Track stock levels by product, filter/search by name, and view recent additions.  
- **Customer Management**: Add and list customers (name, email).  
- **Billing/Invoices**: Create invoices by selecting a customer, date, and amount; search/filter invoices.  
- **Dashboard**: Home‑page summary of totals and recent activity.  
- **PDF Export**: Download invoice as branded PDF using QuestPDF Community license.

## Tech Stack

- **ASP.NET Core 7** (MVC + Razor Pages)  
- **Entity Framework Core** (Code‑First Migrations)  
- **SQL Server Express** (or LocalDB)  
- **QuestPDF** for PDF generation  
- **Bootstrap 5** + Font Awesome for UI  

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)  
- SQL Server Express (or LocalDB)  
- (Optional) [SQL Server Management Studio](https://aka.ms/ssms)  

### Setup

1. **Clone** the repo:
   ```bash
   git clone https://github.com/your-username/ERPConnect.git
   cd ERPConnect
  ```
2. Configure your database in appsettings.json:
   ```bash
   "ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ERPConnectDB;Trusted_Connection=True;TrustServerCertificate=True"
    }
  ```
3. Apply migrations:
  ```bash
  dotnet ef database update
  ```
4. Run the application:
  ```bash
  dotnet run
  ```
Open https://localhost:<PORT> in your browser.

**Usage**
**Dashboard**: Home screen with key metrics.

**Products**: Add/edit/delete products.

**Inventory**: Stock items, filter by product name.

**Customers**: Manage customer records.

**Billing**: Create/search invoices; download PDF.

**License**
This project is MIT‑licensed. QuestPDF is used under its Community license (no key required).