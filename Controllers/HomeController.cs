using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;



namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
         public HomeController(UserContext context)
    {
        _context = context;
    }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("processregistration")]
        public IActionResult processregistration(User bankuser){
            
             if (ModelState.IsValid){
                var ve = _context.eusers.Where(yuyu =>yuyu.email == bankuser.email).ToList();
                if(ve.Count==0){
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    bankuser.password = Hasher.HashPassword(bankuser, bankuser.password);
                    Eusers asd = new Eusers();
                    asd.firstname = bankuser.firstname;
                    asd.lastname = bankuser.lastname;
                    asd.email = bankuser.email;
                    asd.password =  bankuser.password;
                    asd.eusersid = bankuser.ideusers;
                    _context.eusers.Add(asd);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("UserName", asd.firstname);
                    HttpContext.Session.SetInt32("UserId", asd.eusersid);
                        return RedirectToAction("success");
                    }
                    else{
                        ModelState.AddModelError("Email", "Email already exists");
                        return View("Index");
                    }
                }
            else {
                    return View("Index");
                }   
        }
        [HttpPost]
        [Route("processlogin")]
        public IActionResult processlogin(Guy happyguy){
            if (happyguy.email != null && happyguy.password != null){
                var ve = _context.eusers.SingleOrDefault(yuyu =>yuyu.email == happyguy.email);
                if(ve != null){
                    var hasher = new PasswordHasher<Eusers>();
                    if(0 != hasher.VerifyHashedPassword(ve, ve.password, happyguy.password))
                    {
                       HttpContext.Session.SetString("UserName", ve.firstname);
                       HttpContext.Session.SetInt32("UserId", ve.eusersid);
                       return RedirectToAction("success");
                    }
                    else{
                        ModelState.AddModelError("Email", "You can't be logged in");
                        return View("About");
                    }  
                       
        }
        else{
            ModelState.AddModelError("Email", "You can't be logged in");
            return View("About");
        }
            }   
        ModelState.AddModelError("Email", "You can't be logged in");
        return View("About");
    }
        [HttpGet]
        [Route("success")]
        public IActionResult success(){
            var newlistt = _context.guestlist.Include(n=>n.activities).Include(m=>m.eusers).ToList();
            ViewBag.nl = newlistt;

            var eventcoord = _context.activities.Include(m=>m.eusers).ToList();
            ViewBag.ec = eventcoord;//joining table
            var abc = HttpContext.Session.GetInt32("UserId");//iduser
            ViewBag.abc = abc;// user id in session
            var bob = _context.activities.OrderByDescending(ghg=>ghg.date).ToList();//all activities
            ViewBag.bob = bob;//all activities
            var mary = _context.guestlist.ToList();
            var bhb = _context.guestlist.Where(ghuio=>ghuio.eusersid == abc).ToList();
            ViewBag.bnb = bhb;
            if(bhb.Count()<1){
                var count3 = 0;
                ViewBag.count3 = count3;
            }

            if(mary.Count()<1){
                var count = 0;
                ViewBag.count = count;// if list of guests-participants is empty
            }
            ViewBag.mary = mary;//guestlist
                if(abc == null)
                {
                    return View("Index");
     
                }
            string LocalVariable = HttpContext.Session.GetString("UserName");
            ViewBag.name = LocalVariable;
            return View("success");
        }


        public IActionResult About()
        {
            

            return View();
        }
        [HttpGet]
        [Route("contact")]
        public IActionResult Contact()
        {
    
            return RedirectToAction("reset");
        }

        [HttpGet]
        [Route("reset")]

        public IActionResult Reset(){
           HttpContext.Session.Clear();
           return View("Index");
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("createactivity")]
        public IActionResult createactivity(){
            return View("createactivity");
        }
        [HttpPost]
        [Route("processactivity")]
        public IActionResult processactivity(Activities newactivity){
                if (ModelState.IsValid){
                    var ghj = HttpContext.Session.GetInt32("UserId");
                    newactivity.eusersid =(int)ghj;
                    _context.activities.Add(newactivity);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("meetupid", newactivity.activitiesid);
                    return RedirectToAction("activity", new { activitiesid=newactivity.activitiesid } );
                }
                else{
                    return View("createactivity"); 
            }
        }
        
        [HttpGet]
        [Route("activity/{activitiesid}")]
        public IActionResult activity(int activitiesid){
            var eventcoord = _context.activities.Where(inif=>inif.activitiesid == activitiesid).Include(m=>m.eusers).ToList();
            ViewBag.ec = eventcoord;
            var thebob = _context.guestlist.ToList();
            ViewBag.thebob = thebob;
            var allact =  _context.activities.ToList();
            ViewBag.abc = allact;
            var minfo =  _context.activities.Where(inif=>inif.activitiesid == activitiesid).ToList();
            ViewBag.minfo = minfo;
            var bob = _context.guestlist.Where(b=>b.activitiesid == activitiesid).Include(u=>u.activities).Include(y=>y.eusers).ToList();
            ViewBag.bob = bob;
            if(bob.Count()<1){
                ViewBag.lolo = 5;
            }


            
            var abc = HttpContext.Session.GetInt32("UserId");//iduser
            ViewBag.abc = abc;// user id in session
            var secondbob = _context.activities.OrderByDescending(ghg=>ghg.date).ToList();//all activities
            ViewBag.secondbob = secondbob;//all activities
            var mary = _context.guestlist.ToList();
            if(mary.Count()<1){
                var count = 0;
                ViewBag.count = count;// if list of guests-participants is empty
            }
            ViewBag.mary = mary;//guestlist
            
            return View("activity");
        }
        [HttpGet]
        [Route("processdelete/{activitiesid}")]
        public IActionResult processdelete(int activitiesid){
            Activities RetrievedActivity = _context.activities.SingleOrDefault(wed => wed.activitiesid == activitiesid);
            _context.activities.Remove(RetrievedActivity);
            _context.SaveChanges();
            return RedirectToAction("success");
        }
        [HttpGet]
        [Route("addguest/{activitiesid}")]
        
        public IActionResult addguest(int activitiesid){
          
            var ghj = HttpContext.Session.GetInt32("UserId");
                    
                Guestlist newguest = new Guestlist();
                newguest.eusersid = (int)ghj;
                newguest.activitiesid = activitiesid;
                _context.guestlist.Add(newguest);
                _context.SaveChanges();
                return RedirectToAction("success");   
        }

        [HttpGet]
        [Route("deleteguest/{activitiesid}")]
        public IActionResult deleteguest(int activitiesid){
             var ghj = HttpContext.Session.GetInt32("UserId");
             Guestlist Retrievedguest = _context.guestlist.SingleOrDefault(wed => wed.activitiesid == activitiesid && wed.eusersid == (int)ghj);
            _context.guestlist.Remove(Retrievedguest);
            _context.SaveChanges();
            return RedirectToAction("success");
        }
        [HttpGet]
        [Route("activity/processdelete/{activitiesid}")]
        public IActionResult aprocessdelete(int activitiesid){
            Activities RetrievedActivity = _context.activities.SingleOrDefault(wed => wed.activitiesid == activitiesid);
            _context.activities.Remove(RetrievedActivity);
            _context.SaveChanges();
            return RedirectToAction("success");
        }
        [HttpGet]
        [Route("activity/addguest/{activitiesid}")]
        
        public IActionResult aaddguest(int activitiesid){
          
            var ghj = HttpContext.Session.GetInt32("UserId");
            
                Guestlist newguest = new Guestlist();
                newguest.eusersid = (int)ghj;
                newguest.activitiesid = activitiesid;
                _context.guestlist.Add(newguest);
                _context.SaveChanges();
                return RedirectToAction("success");   
        }
         [HttpGet]
        [Route("activity/deleteguest/{activitiesid}")]
        public IActionResult adeleteguest(int activitiesid){
             var ghj = HttpContext.Session.GetInt32("UserId");
             Guestlist Retrievedguest = _context.guestlist.SingleOrDefault(wed => wed.activitiesid == activitiesid && wed.eusersid == (int)ghj);
            _context.guestlist.Remove(Retrievedguest);
            _context.SaveChanges();
            return RedirectToAction("success");
        }
    }

}
