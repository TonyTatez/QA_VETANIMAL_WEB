namespace VET_ANIMAL.WEB.Helpers
{
    public class ReportingAttributes
    {
        /// <summary>
        /// Sirve para indicar el nombre de la columna a mostrar en un archivo de reporte. Setear "columnWidth" indica el ancho personalizado que tendra la columna.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class ReportingOutputAttribute : Attribute
        {
            public ReportingOutputAttribute(string name, int? columnWidth = null)
            {
                Name = name;
                ColumnWidth = columnWidth ?? 400;
            }

            public string Name { get; set; }
            public int ColumnWidth { get; set; }
        }

        /// <summary>
        /// Sirve para indicar que esta propiedad se va a excluir del reporte a generar.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class ReportExcludedAttribute : Attribute
        {
            public ReportExcludedAttribute()
            {
            }
        }
    }
}