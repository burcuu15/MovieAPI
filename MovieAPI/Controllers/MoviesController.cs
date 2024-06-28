using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using System.Security.Claims;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")] //denetleyicinin rota şablonunu belirler
    [ApiController] //web api denetleyicisi olduğunu belirler
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]  //tüm filmleri döndürme isteği
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync(); // tüm filmleri asenkron olarak listeye alır
        }

        // GET: api/Movies/{id}
        [HttpGet("{id}")]  //belirli bir filmi id ile döndürme
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id); //veritabanında belirtilen idye sahip filmi asenkron olarak bulur.

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // POST: api/Movies/{id}/ratings
        [HttpPost("{id}/ratings")] //belli filme derecelendirme ekleme isteği
        public async Task<ActionResult<MovieRating>> AddRating(int id, [FromBody] MovieRating rating)
        {
            if (id != rating.MovieId)
            {
                return BadRequest();
            }

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetMovieRatings), new { id = rating.Id }, rating); 
            //yeni oluşturulan kaynağın urlsi döner
        }

        // GET: api/Movies/{id}/ratings
        [HttpGet("{id}/ratings")] //belli film için tüm dereceleri döndürme isteği
        public async Task<ActionResult<IEnumerable<MovieRating>>> GetMovieRatings(int id)
        {
            var ratings = await _context.Ratings.Where(r => r.MovieId == id).ToListAsync();
            return ratings;
        }

    }
}
