using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTO_CLI
{
    /// <summary>
    /// Kontekst bazy danych.
    /// Suchary usuwane są z poziomu bazy podczas usuwania użytkownika: 
    /// PersonId REFERENCES Persons(Id) ON DELETE CASCADE 
    /// </summary>
    /// <example>
    /// CREATE TABLE Suchary (
    ///    SucharId INTEGER PRIMARY KEY AUTOINCREMENT,
    ///    PersonId REFERENCES Persons(Id) ON DELETE CASCADE,
    ///    categories TEXT,
    ///    created_at TEXT,
    ///    icon_url,
    ///    id TEXT,
    ///    updated_at TEXT,
    ///    url TEXT,
    ///    value      TEXT
    //);
    /// </example>
    public class DataContext : DbContext
    {

        /// <summary>
        /// Ścieżka do pliku db.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = C:\\Users\\Marek\\source\\repos\\ZTO_CLI\\ZTO_CLI\\Baza.db");
        }

        /// <summary>
        /// Właściwość Persons odpowiadająca tabeli Persons
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Właściwość Suchary odpowiadająca tabeli Suchary
        /// </summary>
        public DbSet<Suchar> Suchary { get; set; }
    }
}
