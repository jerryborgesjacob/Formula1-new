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
using Formula1_new.Models;

namespace Formula1_new.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Teams in the table in the database.
        /// </summary>
        /// <returns>
        /// List of teams and their details in the table.
        /// </returns>
        /// <example>
        /// // GET: api/TeamData/ListTeam => Data of teams in the table
        /// </example>
        [HttpGet]
        [Route("api/TeamData/ListTeam")]
        // GET: api/TeamData/ListTeam
        public IEnumerable<TeamDTO> ListTeam()
        {
            List<Team> Teams = db.Teams.ToList();
            List<TeamDTO> TeamDTOs = new List<TeamDTO>();

            Teams.ForEach(a => TeamDTOs.Add(new TeamDTO() 
            {
                TeamId = a.TeamId,
                TeamName = a.TeamName,
                TeamPoints  = a.TeamPoints,
                EngineSupplier = a.EngineSupplier,
                DriverId = a.Driver.DriverId
            
            }));

            return TeamDTOs;
        }

        /// <summary>
        /// Returns the team details with the specified TeamId
        /// </summary>
        /// <param name="id">TeamId of the Team</param>
        /// <returns>
        /// HEADER: 200 (Status Code for OK)
        /// </returns>
        /// <example>
        /// // GET: api/TeamData/5 => Data of Team with TeamId 5
        /// </example>

        [ResponseType(typeof(Team))]
        public IHttpActionResult GetTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        /// <summary>
        /// Updates the Team details of a particular team with the POST data input
        /// </summary>
        /// <param name="id">The TeamId in the table (primary key)</param>
        /// <param name="team">JSON Form Data of a Team</param>
        /// <returns>
        /// Status Code 
        /// HEADER: 200 = Success
        /// or
        /// HEADER: 400 = Bad Request
        /// or
        /// HEADER: 404 = Not Found
        /// </returns>
        /// <example>
        /// POST: api/TeamData/UpdateTeam/5
        /// FORM Data: Team JSON Object
        /// </example>

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.TeamId)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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
        /// Add New Team details onto the Team table in the Database
        /// </summary>
        /// <param name="team">JSON Form Data of the Team</param>
        /// Status Code 
        /// HEADER: 200 = Success
        /// or
        /// HEADER: 400 = Bad Request
        /// or
        /// HEADER: 404 = Not Found
        /// <example>
        /// POST: api/TeamData/AddTeam
        /// FORM Data: Team JSON Object
        /// </example>

        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult AddTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.TeamId }, team);
        }

        /// <summary>
        /// Deletes Team data from the database with TeamId.
        /// </summary>
        /// <param name="id">The primary key of the Team</param>
        /// <returns>
        /// HEADER: 200 = OK
        /// or
        /// HEADER: 404 = Not Found
        /// </returns>
        /// <example>
        /// POST: api/TeamData/DeleteTeam/5
        /// FORM DATA: (empty)
        /// </example>
        
        [ResponseType(typeof(Team))]
        [Route("api/TeamData/DeleteTeam/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.TeamId == id) > 0;
        }
    }
}