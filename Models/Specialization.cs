namespace Task12_EraaSoft.Models
{
    public class Specialization
    {
        public int SpecializationID { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Doctor> Doctors { get; } = new List<Doctor>();
    }
}
