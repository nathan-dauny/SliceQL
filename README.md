# 🧩 SliceQL

**SliceQL** is a Console App that lets you run SQL-like queries directly on structured text files.  
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
- SQLite query execution

---

## 📦 Packages Used

- 🧾 System.CommandLine — Modern API for building command-line apps with argument parsing, tab completion, and more  
- 🗃️ System.Data.SQLite — ADO.NET provider for SQLite databases  

---

## ⚙️ Getting Started

### 🔧 Command-Line Usage (Prototype)

```bash
git clone https://github.com/nathan-dauny/SliceQL.git
cd sliceql
dotnet run --project SliceQL/SliceQL.Console -- --data-file "SLiceQL\inputs\tableName.txt" -s "SELECT * FROM tableName;"


