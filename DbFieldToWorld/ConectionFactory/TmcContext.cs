using Microsoft.EntityFrameworkCore;


namespace DbFieldToWorld
{
    /// <summary>
    /// 
    /// </summary>
   public  class TmcContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseMySql(@"Data Source=10.0.0.32;Database=tmc;User ID=tmc;Password=tmc123;pooling=true;CharSet=utf8;port=3306;sslmode=none");
        }
    }
}
