using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MondaySoccer.Models.EF;
using MondaySoccer.Auxiliary;

namespace MondaySoccer.Controllers
{
    public class GameController : Controller
    {
        private MondaySoccerEntities db = new MondaySoccerEntities();

        //
        // GET: /Game/

        public ActionResult Index(bool all=true)
        {
            if (!all)
                return RedirectToAction("Details",db.Game.FirstOrDefault(T=>T.StateCod==MondaySoccer.Auxiliary.State.Current).GameID);
            
            var games = db.Game.Include(g => g.Game_Player);
            
            return View(games.ToList());
        }

        //
        // GET: /Game/Details/5

        public ActionResult Details(long id = 0)
        {
            ViewBag.PlayerID = new SelectList(db.Player, "PlayerID", "Name");
            Game game = db.Game.FirstOrDefault(t=>t.GameID ==id);
            
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayersInvited = db.Game_Player.Where(t => t.GameID == game.GameID).ToList();
            return View(game);
        }

        //
        // GET: /Game/Create

        public ActionResult Create()
        {
            var current = db.Game.FirstOrDefault(T => T.StateCod == MondaySoccer.Auxiliary.State.Current);
            DateTime date = DateTime.UtcNow;
            if (current != null)
                date = current.Date.AddDays(7);
            else
            {
                DateTime today = new DateTime(date.Year,date.Month,date.Day);
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
                date = today.AddDays(daysUntilMonday);
            }
            //colocar por omissao as duas equipas.
           var game= new Game() { Date = date ,HomeTeam=1,AwayTeam=2};
           ViewBag.Teams = new SelectList(db.Team, "TeamID", "Name");
            return View(game);
        }

        //
        // POST: /Game/Create

        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (game.Date.DayOfWeek != DayOfWeek.Monday)
                ModelState.AddModelError("","Não é segunda!");
            if (ModelState.IsValid)
            {
                game.StateCod = MondaySoccer.Auxiliary.State.Inicial;
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Teams = new SelectList(db.Team, "TeamID", "Name");
            return View(game);
        }

        [HttpPost]
        public ActionResult StartGame(long id)
        {
            Game game = db.Game.FirstOrDefault(t => t.GameID == id);
            if (game == null)
            {
                return HttpNotFound();
            }



            //todo mandar erro
            if (db.Game.Any(T => T.StateCod == MondaySoccer.Auxiliary.State.Current))
                return RedirectToAction("Details", new { id = id });
 
            game.StateCod = MondaySoccer.Auxiliary.State.Current;
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id=id});
        }

        [HttpPost]
        public ActionResult CloseGame(long id)
        {
            Game game = db.Game.FirstOrDefault(t => t.GameID == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            if (DateTime.UtcNow.Date < game.Date.Date || DateTime.UtcNow.Day == game.Date.Day && DateTime.UtcNow.Hour < 22)
            {
                ModelState.AddModelError("", "O Jogo ainda não foi finalizado!");
                return PartialView("_CloseGame",game);
            }



            //faz cenas
            return RedirectToAction("Details", new { id = id });
        }


        [HttpPost]
        public ActionResult AddPlayer(Game_Player player)
        {
            if (ModelState.IsValid)
            {
                db.Game_Player.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        //
        // GET: /Game/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //
        // POST: /Game/Edit/5

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        //
        // GET: /Game/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //
        // POST: /Game/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
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