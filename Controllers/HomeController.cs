using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Belt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Belt.Controllers
{
    public class HomeController : Controller
    {
        StorageContext context;

        public HomeController(StorageContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id != 0) {
                return RedirectToAction("Main");
            }
            return View();
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register() {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id != 0) {
                return RedirectToAction("Main");
            }
            return View();
        }

        [HttpPost]
        [Route("users/new")]
        public IActionResult NewUser(ValidatedUser validatedUser) {
            if (ModelState.IsValid) {
                var users = context.users.Where(x => x.email == validatedUser.email).ToList();
                if (users.Count == 0) {
                    var user = new User {
                        first_name = validatedUser.first_name,
                        last_name = validatedUser.last_name,
                        email = validatedUser.email,
                    };
                    var hasher = new PasswordHasher<User>();
                    user.password = hasher.HashPassword(user, validatedUser.password);
                    context.Add(user);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("user", user.id);
                    return RedirectToAction("Main");
                }else{

                }
            }
            return View("Register");
        }

        [HttpPost("/")]
        public IActionResult Login(string email, string password) {
            var user = context.users.Where(x => x.email == email).FirstOrDefault();
            if (user != null && password != null) {
                var hasher = new PasswordHasher<User>();
                if (hasher.VerifyHashedPassword(user, user.password, password) != 0) {
                    HttpContext.Session.SetInt32("user", user.id);
                    return RedirectToAction("Main");
                }
            }
            TempData["error"] = "Email or Password are invalid.";
            return View("Index");
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Main() {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id == 0) {
                return RedirectToAction("Index");
            }
            var user = context.users.Include(x => x.participants).ThenInclude(x => x.activity).Where(x => x.id == id).Single();
            ViewBag.user = user;
            var user_activities = user.participants;
            var created_activities = context.activities.Where(x => x.creatorId == user.id);
            var list = new List<EventActivity>();
            foreach (var a in user_activities) {
                list.Add(a.activity);
            }
            foreach(var a in created_activities) {
                list.Add(a);
            }
            ViewBag.user_activities = list;
            ViewBag.activities = context.activities.Include(x => x.participants).Include(x => x.creator).Where(x => x.date > DateTime.Now).ToList().OrderBy(x => x.date.AddHours(x.time.Hour).AddMinutes(x.time.Minute));
            return View();
        }

        [HttpGet]
        [Route("activities/new")]
        public IActionResult NewActivity() {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id == 0) {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("activities/new")]
        public IActionResult CreateActivity(ValidatedEventActivity validatedActivity) {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id != 0) {
                if (ModelState.IsValid) {
                    var scaled_duration = validatedActivity.duration;
                    if (validatedActivity.scale == 0) {
                        scaled_duration /= 60;
                    }else if (validatedActivity.scale == 2) {
                        scaled_duration *= 24;
                    }
                    var activity = new EventActivity {
                        name = validatedActivity.name,
                        time = validatedActivity.time,
                        date = validatedActivity.date,
                        duration = scaled_duration,
                        description = validatedActivity.description,
                        creatorId = id
                    };
                    context.Add(activity);
                    context.SaveChanges();
                    return Redirect("/activities/" + activity.id);
                }
            }
            return View("NewActivity");
        }

        [HttpGet]
        [Route("activities/{activity_id}")]
        public IActionResult ShowActivity(int activity_id) {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id == 0) {
                return RedirectToAction("Index");
            }
            var activity = context.activities.Include(x => x.creator).Where(x => x.id == activity_id).FirstOrDefault();
            if (activity == null) {
                return RedirectToAction("Main");
            }
            var user = context.users.Include(x => x.participants).ThenInclude(x => x.activity).Where(x => x.id == id).Single();
            ViewBag.user = user;
            var user_activities = user.participants;
            var created_activities = context.activities.Where(x => x.creatorId == user.id);
            var list = new List<EventActivity>();
            foreach (var a in user_activities) {
                list.Add(a.activity);
            }
            foreach(var a in created_activities) {
                list.Add(a);
            }
            ViewBag.user_activities = list;
            ViewBag.activity = activity;
            ViewBag.participants = context.participants.Where(x => x.activity.id == activity_id).Include(x => x.user).ToList();
            return View("ShowActivity");
        }

        [HttpGet]
        [Route("activities/{activity_id}/participants/new")]
        public IActionResult NewParticipant(int activity_id) {
            var id = HttpContext.Session.GetInt32("user") ?? 0;
            if (id != 0) {
                var part = new Participant {
                    userId = id,
                    activityId = activity_id
                };
                context.Add(part);
                context.SaveChanges();
            }
            return RedirectToAction("Main");
        }

        [HttpGet]
        [Route("participants/{part_id}")]
        public IActionResult DeleteParticipant(int part_id) {
            var part = context.participants.Where(x => x.id == part_id).Single();
            context.Remove(part);
            context.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpGet]
        [Route("activities/{activity_id}/delete")]
        public IActionResult DeleteActivity(int activity_id) {
            var activity = context.activities.Where(x => x.id == activity_id).Single();
            context.Remove(activity);
            context.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
