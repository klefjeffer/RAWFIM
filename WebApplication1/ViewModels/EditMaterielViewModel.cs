namespace RAWFIM.ViewModels
{
    public class EditMaterielViewModel
    {
        public int Id_machine { get; set; }
        public int Id_type_machine { get; set; }
        public string? Num_marché { get; set; }
        public int Id_marque { get; set; }
        public DateTime Date_reception { get; set; }
        public DateTime Date_fin_garantie { get; set; }

        public string? Description_machine { get; set; }

        public int Quantite_stock { get; set; }
        public string? Nom_machine { get; set; }
    }
}
