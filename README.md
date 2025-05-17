# 🧩 SliceQL

**SliceQL** is a lightweight SQL engine that lets you run SQL-like queries directly on structured text files (CSV, TSV, etc.).  
Built with .NET, it works both as a command-line tool and soon as a web interface.

> ⚠️ Excel (.xlsx) support coming soon!

---

## 🚀 Features

- 📂 Load structured `.txt` files as data sources
- 🧠 Run basic SQL queries (`SELECT`, `WHERE`, etc.)
- 💻 Command-line usage
- 🌐 ASP.NET Core web interface in development

---

## 🛠️ Tech Stack

- .NET 8 (C#)
- Modular architecture using .NET Class Libraries
- ASP.NET Core MVC for the web interface
- LINQ for query execution

---

## ⚙️ Getting Started

### 🔧 Command-Line Usage (Prototype)

```bash
git clone https://github.com/your-username/sliceql.git
cd sliceql
dotnet run --project SliceQL.Console -- --file data.txt --query "SELECT * FROM data WHERE age > 30"

