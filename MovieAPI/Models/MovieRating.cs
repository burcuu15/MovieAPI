namespace MovieAPI.Models
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; } // Kullanıcı kimliği veya e-posta adresi
        public string Note { get; set; }
        public int Rating { get; set; } // 1-10 arası puan

       
    }
}
