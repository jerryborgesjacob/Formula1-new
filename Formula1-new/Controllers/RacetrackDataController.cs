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

        // PUT: api/RacetrackData/5
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

        // POST: api/RacetrackData
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

        // DELETE: api/RacetrackData/5
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