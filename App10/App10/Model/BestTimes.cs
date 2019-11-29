using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using App10.Model;
namespace App10.Model
{
    public class BestTimes : ISqliteModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Times { get; set; }

        public TimeSpan TimeSp { get; set; }

       

    }
}
