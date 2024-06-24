using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
//using Formula1_new.Migrations;
using Formula1_new.Models;

namespace Formula1_new.Controllers
{
    public class RacetrackDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Racetracks in the table in the database.
        /// </summary>
        /// <returns>
        /// List of Racetracks and their details in the table.
        /// </returns>
        /// <example>
        /// // GET: api/RacetrackData/ListRacetracks => Data of Racetracks in the table
        /// </example>

        [HttpGet]
        [Route("api/RacetrackData/ListRaceTracks")]
        // GET: api/RacetrackData/ListRaceTracks
        public IEnumerable<RaceTrackDTO> ListRaceTracks()
        {
            List<Racetrack> Racetrack = db.RaceTracks.ToList();
            List<RaceTrackDTO> RaceTrackDTOs = new List<RaceTrackDTO>();

            Racetrack.ForEach(a => RaceTrackDTOs.Add(new RaceTrackDTO()
            {
                TrackId = a.TrackId,
                TrackName = a.TrackName,
                TrackLength = a.TrackLength,
                Country = a.Country
            }));

            return RaceTrackDTOs;
        }

        /// <summary>
        /// Returns the Racetrack details with the specified TrackId
        /// </summary>
        /// <param name="id">TrackId of the Racetrack</param>
        /// <returns>
        /// HEADER: 200 (Status Code for OK)
        /// </returns>
        /// <example>
        /// // GET: api/RacetrackData/5 => Data of Racetrack with TrackId 5
        /// </example>


        // GET: api/RacetrackData/5
        [ResponseType(typeof(Racetrack))]
        public IHttpActionResult GetRacetrack(int id)
        {
            Racetrack racetrack = db.RaceTracks.Find(id);
            if (racetrack == null)
            {
                return NotFound();
            }

            return Ok(racetrack);
        }

        /// <summary>
        /// Updates the Racetrack details of a particular Racetrack with the POST data input
        /// </summary>
        /// <param name="id">The TrackId in the table (primary key)</param>
        /// <param name="racetrack">JSON Form Data of a Racetrack</param>
        /// <returns>
        /// Status Code 
        /// HEADER: 200 = Success
        /// or
        /// HEADER: 400 = Bad Request
        /// or
        /// HEADER: 404 = Not Found
        /// </returns>
        /// <example>
        /// POST: api/RacetrackData/UpdateRacetrack/5
        /// FORM Data: Racetrack JSON Object
        /// </example>
        
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateRacetrack(int id, Racetrack racetrack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != racetrack.TrackId)
            {
                return BadRequest();
            }

            db.Entry(racetrack).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RacetrackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add New Racetrack details onto the Racetrack table in the Database
        /// </summary>
        /// <param name="racetrack">JSON Form Data of the Racetrack</param>
        /// Status Code 
        /// HEADER: 200 = Success
        /// or
        /// HEADER: 400 = Bad Request
        /// or
        /// HEADER: 404 = Not Found
        /// <example>
        /// POST: api/RacetrackData/AddRacetrack
        /// FORM Data: Racetrack JSON Object
        /// </example>

        [ResponseType(typeof(Racetrack))]
        public IHttpActionResult AddRacetrack(Racetrack racetrack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RaceTracks.Add(racetrack);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = racetrack.TrackId }, racetrack);
        }

        /// <summary>
        /// Deletes Racetrack data from the database with TrackId.
        /// </summary>
        /// <param name="id">The primary key of the Racetrack</param>
        /// <returns>
        /// HEADER: 200 = OK
        /// or
        /// HEADER: 404 = Not Found
        /// </returns>
        /// <example>
        /// POST: api/RacetrackData/DeleteRacetrack/5
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Racetrack))]
        [Route("api/RacetrackData/DeleteRacetrack/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteRacetrack(int id)
        {
            Racetrack racetrack = db.RaceTracks.Find(id);
            if (racetrack == null)
            {
                return NotFound();
            }

            db.RaceTracks.Remove(racetrack);
            db.SaveChanges();

            return Ok(racetrack);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RacetrackExists(int id)
        {
            return db.RaceTracks.Count(e => e.TrackId == id) > 0;
        }
    }
}