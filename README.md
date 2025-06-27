# 🧩 SliceQL

**SliceQL** is a versatile tool that lets you run SQL-like queries directly on multiple Csv files.  
Built with .NET, it supports both command-line usage and a modern web interface.

> ⚠️ Excel (.xlsx) support coming soon!

---

## 🚀 Features

- 📂 Load structured `.txt` or `.csv` files as data sources  
- 🧠 Run SQL queries (`SELECT`, `JOIN`, `WHERE`, etc.) on text files as if they were database tables  
- 💻 Command-line application for advanced users and automation  
- 🌐 ASP.NET Core MVC web interface for easy, interactive usage — **currently deployed on Render**  

---

## 🛠️ Tech Stack

- .NET 8 (C#)  
- Modular design with .NET Class Libraries  
- ASP.NET Core MVC for the web front-end  
- SQLite for query execution and in-memory relational operations 

---

## 📦 Packages Used

- 🧾 System.CommandLine — Modern API for building command-line apps with argument parsing, tab completion, and more  
- 🗃️ System.Data.SQLite — ADO.NET provider for SQLite databases
- 📊 DynamicCsvParser — Custom library to parse CSV files dynamically and map data types automatically  

---

## ⚙️ Getting Started

### 🔧 Command-Line Usage (Prototype)

```bash
git clone https://github.com/nathan-dauny/SliceQL.git
cd sliceql
dotnet run --project SliceQL/SliceQL.Console -- --data-file "SLiceQL\inputs\tableName.txt" -s "SELECT * FROM tableName;"

### 🌐 Web Interface

Access SliceQL through its web interface, hosted on Render:  
[https://sliceql.onrender.com/](https://sliceql.onrender.com/)

- Upload multiple CSV files  
- Write and execute SQL queries interactively  
- View results instantly in your browser

