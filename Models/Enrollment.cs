namespace Task12_EraaSoft.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
