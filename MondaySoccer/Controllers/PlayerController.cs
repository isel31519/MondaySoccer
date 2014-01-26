using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MondaySoccer.Models.EF;

namespace MondaySoccer.Controllers
{
    public class PlayerController : Controller
    {
        private MondaySoccerEntities db = new MondaySoccerEntities();

        //
        // GET: /Player/

        public ActionResult Index()
        {
            var player = db.Player.Include(p => p.Team);
            return View(player.ToList());
        }

        //
        // GET: /Player/Details/5

        public ActionResult Details(long id = 0)
        {
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Name");
            return View();
        }

        //
        // POST: /Player/Create

        [HttpPost]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Player.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Name", player.TeamID);
            return View(player);
        }

        //
        // GET: /Player/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Name", player.TeamID);
            return View(player);
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Name", player.TeamID);
            return View(player);
        }

        //
        // GET: /Player/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // POST: /Player/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Player player = db.Player.Find(id);
            db.Player.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}