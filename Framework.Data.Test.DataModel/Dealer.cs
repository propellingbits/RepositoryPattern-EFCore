using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Data.Test.DataModel
{
    public class Dealer
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
