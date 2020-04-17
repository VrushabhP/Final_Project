using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Data
{
    public class Summer
    {
        [Key]
        [Column(name: "ID", Order = 9, TypeName = "float")]
        public float ID { get; set; }

        [Column(name: "Year",Order = 0,TypeName ="float")]
        public float? Year { get; set; }
        
        #nullable enable
        [Column(name: "City", Order = 1, TypeName = "nvarchar(255)")]
        public string? City { get; set; }
        [Column(name: "Sport", Order = 2, TypeName = "nvarchar(255)")]
        public string? Sport { get; set; }
        [Column(name: "Discipline", Order = 3, TypeName = "nvarchar(255)")]
        public string? Discipline { get; set; }
        [Column(name: "Athlete", Order = 4, TypeName = "nvarchar(255)")]
        public string? Athlete { get; set; }
        [Column(name: "Country", Order = 5, TypeName = "nvarchar(255)")]
        public string? Country { get; set; }
        [Column(name: "Gender", Order = 6, TypeName = "nvarchar(255)")]
        public string? Gender { get; set; }
        [Column(name: "Event", Order = 7, TypeName = "nvarchar(255)")]
        public string? Event { get; set; }
        [Column(name: "Medal", Order = 8, TypeName = "nvarchar(255)")]
        public string? Medal { get; set; }
        
    }
}
