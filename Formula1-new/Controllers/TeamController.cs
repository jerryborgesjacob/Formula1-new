﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Diagnostics;
using System.Security.Policy;
using Formula1_new.Models;
using Formula1_new.Migrations;


namespace Formula1_new.Controllers
{
    public class TeamController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TeamController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }

        public ActionResult List()
        {
            string url = "TeamData/ListTeam";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<TeamDTO> teams = response.Content.ReadAsAsync<IEnumerable<TeamDTO>>().Result;
            return View(teams);
        }

        // GET: Team/Details/5
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

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamDTO team) 
        {
            string url = "TeamData/AddTeam";
            string jsonpayload = jss.Serialize(team);
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

        // POST: Team/Create
        /*[HttpPost]
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

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            //the existing driver information
            string url = "TeamData/GetTeam/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeamDTO SelectedTeam = response.Content.ReadAsAsync<TeamDTO>().Result;

            return View(SelectedTeam);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Update(int id, TeamDTO team)
        {
            string url = "TeamData/UpdateTeam/" + id;
            string jsonpayload = jss.Serialize(team);
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

        // GET: Team/Delete/5

        public ActionResult DeleteConfirm(int id)
        {
            string url = "TeamData/GetTeam/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeamDTO selectedTeam = response.Content.ReadAsAsync<TeamDTO>().Result;
            return View(selectedTeam);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "TeamData/DeleteTeam/" + id;
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
