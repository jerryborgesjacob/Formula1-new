using Formula1_new.Migrations;
using Formula1_new.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Formula1_new.Controllers
{
    public class RacetrackController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RacetrackController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }

        // GET: Racetrack
        [Authorize]
        public ActionResult List()
        {
            //objective: communicate with our Racetrack data API to retrieve a list of Racetracks
            //curl: https://localhost:44341/api/RacetrackData/ListRacetracks

            string url = "RacetrackData/ListRacetracks";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<RaceTrackDTO> racetracks = response.Content.ReadAsAsync<IEnumerable<RaceTrackDTO>>().Result;
            return View(racetracks);
        }

        // GET: Racetrack/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        // POST: Racetrack/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(RaceTrackDTO racetrack)
        {

            //objective: Add a new Racetrack into the Database using the API
            //curl -H "Content-Type:application/json" -d @Driver.json https://localhost:44324/api/RacetrackData/AddRacetrack

            string url = "RacetrackData/AddRacetrack";
            string jsonpayload = jss.Serialize(racetrack);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        /*
         * // POST: Racetrack/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: Racetrack/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            //the existing racetrack information
            string url = "RacetrackData/GetRacetrack/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RaceTrackDTO SelectedRacetrack = response.Content.ReadAsAsync<RaceTrackDTO>().Result;

            return View(SelectedRacetrack);
        }

        // POST: Racetrack/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Update(int id, RaceTrackDTO racetrack)
        {
            string url = "RacetrackData/UpdateRacetrack/" + id;
            string jsonpayload = jss.Serialize(racetrack);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Racetrack/DeleteConfirm/5
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "RacetrackData/GetRacetrack/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RaceTrackDTO selectedRacetrack = response.Content.ReadAsAsync<RaceTrackDTO>().Result;
            return View(selectedRacetrack);
        }

        // POST: Racetrack/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "RacetrackData/DeleteRacetrack/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
