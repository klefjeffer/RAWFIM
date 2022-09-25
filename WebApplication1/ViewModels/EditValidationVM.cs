namespace RAWFIM.ViewModels
{
    public class EditValidationVM
    {
        public int Id_validateur { get; set; }
        public bool Status_validation { get; set; }
        public DateTime Date_decision { get; set; }
        public string? Motif_refus { get; set; }
    }
}
