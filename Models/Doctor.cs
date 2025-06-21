namespace Task12_EraaSoft.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public int SpecializationID { get; set; }
        public Specialization Specialization { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
    }
}
