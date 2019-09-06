using DataBaseHandler.Models;
using System.Data.Entity;

namespace DataBaseHandler.DbContexts
{
    internal class FileContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
    }
}
