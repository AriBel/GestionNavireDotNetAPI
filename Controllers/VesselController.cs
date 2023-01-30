using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Vessel;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VesselController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VesselController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Vessels
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Vessel>>> GetAllVessels()
        {
            return Ok(await _context.Vessels.ToListAsync());
        }

        // GET: Vessels/Details/5
        [HttpGet]
        [Route("{vesselNumber}")]
        public async Task<ActionResult<Vessel>> GetVessel(string vesselNumber)
        {
            if (vesselNumber == null || _context.Vessels == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.VesselNumber == vesselNumber);
            if (vessel == null)
            {
                return NotFound();
            }

            return Ok(vessel);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> CreateVessel([FromBody] VesselCreateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var newVessel = new Vessel()
            {
                VesselNumber = request.VesselNumber,
                Name = request.Name,
                YearOfConstruction = request.YearOfConstruction,
                Length = request.Length,
                Width = request.Width,
                NetWeight = request.NetWeight,
                GrossWeight = request.GrossWeight,
                Info = request.Info

            };

            if (newVessel != null)
            {
                _context.Vessels.Add(newVessel);
                _context.SaveChanges();
            }

            return Ok("Vessel create!");
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateVessel([FromBody] VesselUpdateRequest request)
        {

            Vessel updateVessel = new Vessel
            {
                GrossWeight = request.GrossWeight,
                Info = request.Info,
                Length = request.Length,
                Name = request.Name,
                NetWeight = request.NetWeight,
                //VesselNumber = request.VesselNumber,
                Width = request.Width,
                YearOfConstruction = request.YearOfConstruction
            };

            _context.Entry(await _context.Vessels.FirstOrDefaultAsync(x => x.VesselNumber == request.VesselNumber)).CurrentValues.SetValues(updateVessel);
            return (await _context.SaveChangesAsync()).ToString();
       
        }

        // POST: Vessels/Delete/5
        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult<bool>> DeleteVessel([FromBody] VesselDeleteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.VesselNumber == null || _context.Vessels == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.VesselNumber == request.VesselNumber);
            if (vessel == null)
            {
                return NotFound();
            }

            _context.Vessels.Remove(vessel);
            _context.SaveChanges();

            return Ok(vessel);
        }
    }
}
