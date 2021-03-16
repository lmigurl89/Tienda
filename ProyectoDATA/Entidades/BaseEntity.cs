using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDATA.Entidades
{
    public class BaseEntity
    {
        #region Propiedades        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime FechaCreado { get; set; }

        #endregion
    }
}
