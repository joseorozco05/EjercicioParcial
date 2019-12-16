using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NgNetCore.Data;
using NgNetCore.Models;
using NgNetCore.ViewModels;



namespace NgNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiqueteController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiqueteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tiquete
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiquete>>> GetTiquete()
        {
            return await _context.Tiquetes.ToListAsync();
        }

        // GET: api/Tiquete/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tiquete>> GetTiquete(string id)
        {
            var tiquete = await _context.Tiquetes.FindAsync(id);

            if (tiquete == null)
            {
                return NotFound();
            }

            return tiquete;
        }

        // POST: api/Rutas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tiquete>> PostTiquete(TiqueteViewModel request)
        {
            var tiquete = new Tiquete()
            {
                 Id = request.Id,
                 RutaId = request.IdRuta,
                 ClienteId=request.ClienteId
                 
            };
            
            _context.Tiquetes.Add(tiquete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TiqueteExists(tiquete.Id))
                {
                    ModelState.AddModelError("Tiquete", "El codigo del tiquete ya se encuentra registrado");
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    return BadRequest(problemDetails);
                }
                else
                {
                    ModelState.AddModelError("Tiquete", "Existe un problema con la ruta");
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    return BadRequest(problemDetails);
                }
            }

            return CreatedAtAction("GetRuta", new { id = tiquete.Id }, tiquete);
        }

    }
}