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
    /// </summary>
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
